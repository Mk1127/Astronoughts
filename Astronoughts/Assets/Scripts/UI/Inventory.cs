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
    public static Inventory inventory;

    // A reference to the inventorys RectTransform
    protected RectTransform inventoryRect;

    // The width and height of the inventory
    protected float inventoryWidth, inventoryHeight;

    // This indicates if the inventory is open or closed
    private bool isOpen;

    // The number of slots & rows, their size, and offset to top & left 
    public int slots;
    public int rows;
    public float slotSize;
    public float slotPaddingLeft, slotPaddingTop;

    // background of inventory, slots, and slots qualities
    public Image invImage;
    private readonly Image slotImage;
    private Image[] slotImages;

    // Offset used to move the hovering object away from the mouse 
    protected float hoverYOffset;

    // variables for moving items to and from slots
    private Slot from;
    private Slot to;
    private Slot movingSlot;
    private GameObject clicked;
    private GameObject hoverObject;
    [SerializeField]
    private int emptySlots;

    // structural objects
    public EventSystem eventSystem;
    public Canvas UI;
    public CanvasGroup canvasGroup;

    // gameObjects (inventory prefabs to generate)
    public GameObject iconPrefab;
    public Button inventoryButton;

    public Button invPanel;
    public Button invSolar1;
    public Button invSolar2;
    public Button invEngine;
    public Button invCockpit;

    private GameObject gm;
    private GameManager gmScript;

    // function-related variables
    private bool fadingIn;
    private bool fadingOut;
    private bool hidden;

    public float fadeTime;
    public float iA;
    #endregion

    #region Collections
    // a list for all of the inventory's slots
    public List<GameObject> allSlots;
    #endregion

    #region Properties
    // The slot we are moving from
    public Slot From
    {
        get
        {
            return from;
        }
        set
        {
            from = value;
        }
    }


    // The slot we are moving to
    public Slot To
    {
        get
        {
            return to;
        }
        set
        {
            to = value;
        }
    }

    // The clicked item
    public GameObject Clicked
    {
        get
        {
            return clicked;
        }
        set
        {
            clicked = value;
        }
    }

    // The slot that contains the items being moved
    public Slot MovingSlot
    {
        get
        {
            return movingSlot;
        }
        set
        {
            movingSlot = value;
        }
    }

    // The hover object
    public GameObject HoverObject
    {
        get
        {
            return hoverObject;
        }
        set
        {
            hoverObject = value;
        }
    }

    // Indicates if the inventory is open
    public bool IsOpen
    {
        get
        {
            return isOpen;
        }
        set
        {
            isOpen = value;
        }
    }

    // Property for accessing the number of empty slots
    public int EmptySlots
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

    public bool FadingOut
    {
        get
        {
            return fadingOut;
        }
    }

    #endregion

    // Use this for initialization
    private void Start()
    {
        gm = GameObject.FindGameObjectWithTag("GameController");
        gmScript = gm.GetComponent<GameManager>();

        CheckSlots();

        isOpen = true;

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
                RectTransformUtility.ScreenPointToLocalPointInRectangle(UI.transform as RectTransform,Input.mousePosition,UI.worldCamera,out Vector2 position);
                hoverObject.transform.position = UI.transform.TransformPoint(position);
                position.Set(position.x,position.y - hoverYOffset);
                hoverObject.transform.position = UI.transform.TransformPoint(position);
            }
        }
    }

    #region Functions
    public void CheckSlots()
    {
        if(gmScript.panelEnabled == true)
        {
            invPanel.interactable = true;
        }
        if(gmScript.panelEnabled == false)
        {
            invPanel.interactable = false;
        }

        if(gmScript.solar1Enabled == true)
        {
            invSolar1.interactable = true;
        }
        if(gmScript.solar1Enabled == false)
        {
            invSolar1.interactable = false;
        }

        if(gmScript.solar2Enabled == true)
        {
            invSolar2.interactable = true;
        }
        if(gmScript.solar2Enabled == false)
        {
            invSolar2.interactable = false;
        }

        if(gmScript.engineEnabled == true)
        {
            invEngine.interactable = true;
        }
        if(gmScript.engineEnabled == false)
        {
            invEngine.interactable = false;
        }

        if(gmScript.cockpitEnabled == true)
        {
            invCockpit.interactable = true;
        }
        if(gmScript.cockpitEnabled == false)
        {
            invCockpit.interactable = false;
        }
    }

     public Image[] GetSlots()
    {
        // Get the slot objects
        GameObject[] slotObjects = GameObject.FindGameObjectsWithTag("Slot");

        // Declare an array of slots
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

    public void AddItem(Item item)
    {
        if(item.name == "Panel")
        {
            gmScript.panelButton.interactable = true;
        }
        if(item.name == "Solar1")
        {
            gmScript.solar1Button.interactable = true;
        }
        if(item.name == "Solar2")
        {
            gmScript.solar2Button.interactable = true;
        }
        if(item.name == "Engine")
        {
            gmScript.engineButton.interactable = true;
        }
        if(item.name == "cockpit")
        {
            gmScript.cockpitButton.interactable = true;
        }
    }

    public void MoveItem(GameObject clicked)
    {
        if(from == null)
        {
            if(!clicked.GetComponent<Slot>().IsEmpty) // The slot we clicked isn't empty
            {
                from = clicked.GetComponent<Slot>(); // The slot we're moving from
                from.GetComponent<Image>().color = Color.gray; // Set the "from" slot's color to gray (its the "from" slot)

                hoverObject = (GameObject)Instantiate(iconPrefab);
                hoverObject.GetComponent<Image>().sprite = clicked.GetComponent<Image>().sprite; // Sets the sprite on the hover object so that it reflects the object we are moving
                hoverObject.name = "Hover"; // Sets the name of the hover object

                // Creates references to the transforms
                RectTransform hoverTransform = hoverObject.GetComponent<RectTransform>();
                RectTransform clickedTransform = clicked.GetComponent<RectTransform>();

                // Sets the size of the hoverobject so that it has the same size as the clicked object
                hoverTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal,clickedTransform.sizeDelta.x);
                hoverTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical,clickedTransform.sizeDelta.y);

                // Sets the hoverobject's parent as the canvas, so that it is visible in the game
                hoverObject.transform.SetParent(GameObject.Find("UI").transform,true);

                // Sets the local scale to make usre that it has the correct size
                hoverObject.transform.localScale = clicked.transform.localScale;

                // hoverObject.transform.GetChild(0).GetComponent<Text>().text = movingSlot.Items.Count > 1 ? movingSlot.Items.Count.ToString() : string.Empty;
            }
        }
        else if(to == null) // Select the slot we are moving to
        {
            to = clicked.GetComponent<Slot>(); // Set the "to" object
            Destroy(GameObject.Find("Hover"));
        }
        
        if(to != null && from != null) // If both "to" and "from" are null we're done moving items. 
        {
            Stack<Item> tmpTo = new(to.Items);
            to.AddItems(from.Items);

            if(tmpTo.Count == 0)
            {
                from.ClearSlot();
            }
            else
            {
                from.AddItems(tmpTo);
            }
            // Reset all values
            from.GetComponent<Image>().color = Color.white;
            to = null;
            from = null;

        }
    }
    
    private IEnumerator FadeOut()
    {
        if(!fadingOut) // Checks if we are already fading out
        {
            // Sets the current state
            fadingOut = true;
            fadingIn = false;

            // Makes sure that we are not fading out the at same time
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

            // Sets the end condition to make sure we are 100% invisible
            // slotImage.alpha = 0;
            invImage.color = new Color(1,1,1,0);

            // Sets the status
            fadingOut = false;
            // instantClose = false;

        }
    }

    private IEnumerator FadeIn()
    {
        if(!fadingIn) // Is it already fading out?
        {
            // Set the current state
            fadingOut = false;
            fadingIn = true;

            // Make sure that we are not fading out
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

                yield return null;
            }

            // Set the end condition to make sure we are 100% visible
            // slotImage.alpha = 1;
            invImage.color = new Color(1,1,1,1);

            // Set the status
            fadingIn = false;
        }
    }

    #endregion
}
