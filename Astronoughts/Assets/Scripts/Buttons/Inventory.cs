using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;
using TMPro;
using UnityEngine.EventSystems;

public class Inventory : MonoBehaviour
{
    #region Variables
    // A reference to the inventorys RectTransform
    protected RectTransform inventoryRect;

    // The width and height of the inventory
    protected float inventoryWidth, inventoryHeight;

    // The number of slots
    public int slots;

    // The number of rows
    public int rows;

    // The left and top slots padding
    public float slotPaddingLeft, slotPaddingTop;

    // The size of each slot
    public float slotSize;

    // slots prefab
    public GameObject slotPrefab;

    // background of inventory
    public Image invImage;
    private Image slotImage;
    private Image[] slotImages;

    // Offset used to move the hovering object away from the mouse 
    protected float hoverYOffset;

    // variables for moving items to and from slots
    private static Slot from, to;
    public GameObject iconPrefab;
    private static GameObject hoverObject;
    private static int emptySlots;
    public EventSystem eventSystem;
    public Canvas canvas;
    public CanvasGroup canvasGroup;
    private bool fadingIn;
    private bool fadingOut;
    private bool hidden;
    public float fadeTime;
    public float iA;
    public Button inventoryButton;
    #endregion

    #region Collections

    // A list of all the slots in the inventory
    private List<GameObject> allSlots;

    #endregion

    #region Properties
    // Property for accessing the number of empty slots
    public static int EmptySlots
    {
        get
        {
            return emptySlots;
        }
        set
        {
            emptySlots = value;
        }
    }

    #endregion


    // Use this for initialization
    private void Start()
    {
        //Creates the inventory layout
        CreateLayout();

        Image[] slotImages = GetSlots();
        invImage.color = new Color(1,1,1,0);

        foreach(Image slotImage in slotImages)
        {
            slotImage.color = new Color(1,1,1,0); // hidden at first
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonUp(0)) //Check if the user lifted the first mousebutton
        {
            if(!eventSystem.IsPointerOverGameObject(-1) && from != null)
            {
                from.GetComponent<Image>().color = Color.white;
                from.ClearSlot();
                Destroy(GameObject.Find("Hover"));
                to = null;
                from = null;
                hoverObject = null;
                emptySlots++;
            }
            if(hoverObject != null)
            {
                Vector2 position;
                RectTransformUtility.ScreenPointToLocalPointInRectangle(canvas.transform as RectTransform,Input.mousePosition,canvas.worldCamera,out position);
                hoverObject.transform.position = canvas.transform.TransformPoint(position);
                position.Set(position.x,position.y - hoverYOffset);
                hoverObject.transform.position = canvas.transform.TransformPoint(position);
            }
        }
    }

    public Image[] GetSlots()
    {
        // Get the slot objects
        GameObject[] slotObjects = GameObject.FindGameObjectsWithTag("Slot");

        // Declare an array of slots with the amount of hearts present
        slotImages = new Image[slotObjects.Length];

        // Loop through the slots, adding each image component to the array
        for(int i = 0;i < slotImages.Length;i++)
        {
            slotImages[i] = slotObjects[i].GetComponent<Image>();
        }
        // Return the slots array
        return slotImages;
    }

    public void InventoryClick()
    {
        if(iA > 0)
        {
            StartCoroutine("FadeOut"); //Close the inventory (Start Coroutines with a string so they can be manually stopped)
        }
        else
        {
            StartCoroutine("FadeIn"); // Open the inventory
        }
    }

    public void CreateLayout()
    {
        //Instantiate "allSlot" list
        allSlots = new List<GameObject>();

        //Calculate hoverYOffset: 1% of slot size
        hoverYOffset = slotSize * 0.1f;

        //Store the number of empty slots
        emptySlots = slots;

        //Calculate inventory width
        inventoryWidth = (slots / rows) * (slotSize + slotPaddingLeft);

        //Calculate inventory height
        inventoryHeight = rows * (slotSize + slotPaddingTop);

        //Create a reference to the inventory's RectTransform
        inventoryRect = GetComponent<RectTransform>();

        //Set the inventory's width and height
        inventoryRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal,inventoryWidth + slotPaddingLeft);
        inventoryRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical,inventoryHeight + slotPaddingTop);

        //Calculate the number of columns
        int columns = slots / rows;

        for(int y = 0;y < rows;y++) //Runs through the rows
        {
            for(int x = 0;x < columns;x++) //Runs through the columns
            {
                //Instantiates the slot and creates a reference to it
                GameObject newSlot = (GameObject)Instantiate(slotPrefab);

                //Makes a reference to the rect transform
                RectTransform slotRect = newSlot.GetComponent<RectTransform>();

                //Sets the slots name
                newSlot.name = "Slot";

                //Sets the canvas as the parent of the slots, so that it will be visible on the screen
                newSlot.transform.SetParent(this.transform.parent);

                //Sets the slots position
                slotRect.localPosition = inventoryRect.localPosition + new Vector3(slotPaddingLeft * (x + 1) + (slotSize * x),-slotPaddingTop * (y + 1) - (slotSize * y));

                //Sets the size of the slot
                slotRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal,slotSize * canvas.scaleFactor);
                slotRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical,slotSize * canvas.scaleFactor);
                newSlot.transform.SetParent(this.transform);

                //Adds the new slots to the slot list
                allSlots.Add(newSlot);
            }
        }
    }

    /// Add an item to the inventory
    /// <param name="item">The item to add</param>
    /// <returns></returns>
    public bool AddItem(Item item)
    {
        if(item.maxSize == 1) //If the item isn't stackable
        {
            //Places the item at an empty slot
            PlaceEmpty(item);
            return true;
        }
        else //If the item is stackable 
        {
            foreach(GameObject slot in allSlots) //Runs through all slots in the inventory
            {
                Slot tmp = slot.GetComponent<Slot>(); //Creates a reference to the slot

                if(!tmp.IsEmpty) //If the item isn't empty
                {
                    //Checks if the om the slot is the same type as the item we want to pick up
                    if(tmp.CurrentItem.type == item.type && tmp.IsAvailable)
                    {
                        tmp.AddItem(item); //Adds the item to the inventory
                        return true;
                    }
                }
            }
            if(emptySlots > 0) //Places the item on an empty slots
            {
                return PlaceEmpty(item);
            }
        }

        return false;
    }

    /// Put item in empty slot
    /// <param name="item"></param>
    /// <returns></returns>
    private bool PlaceEmpty(Item item)
    {
        if(emptySlots > 0) //If we have at least one empty slot
        {
            foreach(GameObject slot in allSlots) //Run through all of the slots
            {
                Slot tmp = slot.GetComponent<Slot>(); //Create a reference to the slot 
                if(tmp.IsEmpty) //If the slot is empty
                {
                    tmp.AddItem(item); //Add item
                    return true;
                }
            }
        }
        return false;
    }

    /// Move an item to another slot in the inventory
    /// <param name="clicked"></param>
    public void MoveItem(GameObject clicked)
    {
        if(from == null)
        {
            if(!clicked.GetComponent<Slot>().IsEmpty) //The slot we clicked isn't empty
            {
                from = clicked.GetComponent<Slot>(); //The slot we're emoving from
                from.GetComponent<Image>().color = Color.gray; //Set the "from" slot's color to gray (its the "from" slot)

                hoverObject = (GameObject)Instantiate(iconPrefab);
                hoverObject.GetComponent<Image>().sprite = clicked.GetComponent<Image>().sprite; //Sets the sprite on the hover object so that it reflects the object we are moving
                hoverObject.name = "Hover"; //Sets the name of the hover object

                //Creates references to the transforms
                RectTransform hoverTransform = hoverObject.GetComponent<RectTransform>();
                RectTransform clickedTransform = clicked.GetComponent<RectTransform>();

                //Sets the size of the hoverobject so that it has the same size as the clicked object
                hoverTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal,clickedTransform.sizeDelta.x);
                hoverTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical,clickedTransform.sizeDelta.y);

                //Sets the hoverobject's parent as the canvas, so that it is visible in the game
                hoverObject.transform.SetParent(GameObject.Find("Canvas").transform,true);

                //Sets the local scale to make usre that it has the correct size
                hoverObject.transform.localScale = clicked.gameObject.transform.localScale;

                //hoverObject.transform.GetChild(0).GetComponent<Text>().text = movingSlot.Items.Count > 1 ? movingSlot.Items.Count.ToString() : string.Empty;
            }
        }
        else if(to == null) //Select the slot we are moving to
        {
            to = clicked.GetComponent<Slot>(); //Set the "to" object
            Destroy(GameObject.Find("Hover"));
        }
        
        if(to != null && from != null) //If both "to" and "from" are null we're done moving items. 
        {
            Stack<Item> tmpTo = new Stack<Item>(to.Items);
            to.AddItems(from.Items);

            if(tmpTo.Count == 0)
            {
                from.ClearSlot();
            }
            else
            {
                from.AddItems(tmpTo);
            }
            //Reset all values
            from.GetComponent<Image>().color = Color.white;
            to = null;
            from = null;

        }
    }
    private IEnumerator FadeOut()
    {
        if(!fadingOut) //Checks if we are already fading out
        {
            //Sets the current state
            fadingOut = true;
            fadingIn = false;

            //Makes sure that we are not fading out the at same time
            StopCoroutine("FadeIn");


            // loop over 1 second backwards
            for(iA = 1;iA >= 0;iA -= Time.deltaTime)
            {
                invImage.color = new Color(1,1,1,iA);
                // set color with iA as alpha
                foreach(Image slotImage in slotImages)
                {
                    slotImage.color = new Color(1,1,1,iA);
                }
                yield return null;
            }

            //Sets the values for fading
            // float startAlpha = canvasGroup.alpha;

            //float rate = 1.0f / fadeTime; //Calculates the rate, so that we can fade over x number of seconds

            // float progress = 0.0f; //Progresses over the set time

            //while(progress < 1.0) //Progresses over the set time
            //{
            //    slotImage.color = Mathf.Lerp(startAlpha,0,progress);  //Lerps from the start alpha to 0 to make the inventory invisible
            //
            //    progress += rate * Time.deltaTime; //Adds to the progress so that we will get close to out goal
            //
            //     yield return null;
            // }

            //Sets the end condition to make sure we are 100% invisible
            //slotImage.alpha = 0;
            invImage.color = new Color(1,1,1,0);

            //Sets the status
            fadingOut = false;
            //instantClose = false;

        }
    }

    private IEnumerator FadeIn()
    {
        if(!fadingIn) //Is it already fading out?
        {
            //Set the current state
            fadingOut = false;
            fadingIn = true;

            //Make sure that we are not fading out
            StopCoroutine("FadeOut");

            // loop over 1 second
            for(iA = 0; iA <= 1; iA += Time.deltaTime)
            {
                invImage.color = new Color(1,1,1,iA);
                // set color with i as alpha
                foreach(Image slotImage in slotImages)
                {
                    slotImage.color = new Color(1,1,1,iA);
                }

                //if(instantClose)
                // {
                //     break;
                // }
                yield return null;
            }

            //float startAlpha = slotImage.alpha; //Set the start alpha value

            //float rate = 1.0f / fadeTime; //Calculate the rate, so that we can fade over x amount of seconds

            //float progress = 0.0f; //Reset the progress

            //while(progress < 1.0) //Progress over the set time
            //{
            //    slotImage.alpha = Mathf.Lerp(startAlpha,1,progress); //Lerp from the start alpha to one to make the inventory visible
            //
            //    progress += rate * Time.deltaTime; //Add to the progress so that we will get close to out goal
            //
            //    yield return null;
            //}

            //Set the end condition to make sure we are 100% visible
            //slotImage.alpha = 1;
            invImage.color = new Color(1,1,1,1);

            //Set the status
            fadingIn = false;
        }
    }


}
