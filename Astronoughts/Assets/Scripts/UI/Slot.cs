using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Slot : MonoBehaviour, IPointerClickHandler, IPointerDownHandler, IPointerUpHandler, IPointerExitHandler
{
    #region Variables
    private Stack<Item> items;

    // The number of items to pickup (stack text)
    public Text stackText;

    // The slot's various sprite images
    public Sprite slotEmpty;
    public Sprite slotHighlight;

    [SerializeField]
    private float holdTime = 1f;
    private bool held = false;
    public UnityEvent onClick = new UnityEvent();
    public UnityEvent onLongPress = new UnityEvent();
    #endregion

    #region Properties
    // Indicates if the slot is empty
    public bool IsEmpty
    {
        get
        {
            return items.Count == 0;
        }
    }

    // Indicates if the slot is available for stacking more items
    public bool IsAvailable
    {
        get
        {
            return CurrentItem.maxSize > items.Count;
        }
    }

    // Returns the current item in the stack
    public Item CurrentItem
    {
        get
        {
            return items.Peek();
        }
    }

    public Stack<Item> Items
    {
        get
        {
            return items;
        }
        set
        {
            items = value;
        }
    }
    #endregion

    void Awake()
    {
        // Instantiates the items stack
        Items = new Stack<Item>();
    }

    void Start()
    {
        // Creates a reference to the slot slot's recttransform
        RectTransform slotRect = GetComponent<RectTransform>();

        // Creates a reference to the stackText's recttransform
        RectTransform textRect = stackText.GetComponent<RectTransform>();

        // Calculates the scalefactor of the text by taking 60% of the slots width
        int textScaleFactor = (int)(slotRect.sizeDelta.x * 0.20);

        // Sets the min and max textSize of the stackText
        stackText.resizeTextMaxSize = textScaleFactor;
        stackText.resizeTextMinSize = textScaleFactor;

        // Sets the actual size of the textRect
        textRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal,slotRect.sizeDelta.x);
        textRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical,slotRect.sizeDelta.y);
    }

    #region Functions
    public void AddItem(Item item)
    {
        if(IsEmpty) // if the slot is empty
        {
            transform.parent.GetComponent<Inventory>().EmptySlots--; // Reduce the number of empty slots
        }
        Items.Push(item); // Adds the item to the stack
        if(Items.Count > 1) // Checks if we have a stacked item
        {
            stackText.text = Items.Count.ToString(); // If the item is stacked then we need to write the stack number on top of the icon
        }
        ChangeSprite(item.spriteNeutral,item.spriteHighlighted); // Changes the sprite so that it reflects the item the slot is occupied by
    }

    public void AddItems(Stack<Item> items)
    {
        if(IsEmpty) // if the slot is empty
        {
            transform.parent.GetComponent<Inventory>().EmptySlots--; // Reduce the number of empty slots
        }
        this.Items = new Stack<Item>(items); // Adds the stack of items to the slot
        stackText.text = items.Count > 1 ? items.Count.ToString() : string.Empty; // Writes the correct stack number on the icon
        ChangeSprite(CurrentItem.spriteNeutral,CurrentItem.spriteHighlighted); //Changes the sprite so that it reflects the item the slot is occupied by
    }

    private void ChangeSprite(Sprite neutral, Sprite highlight)
    {
        // Sets the generic sprite
        GetComponent<Image>().sprite = neutral;

        // creating and setting dfferent states for the Sprites
        SpriteState st = new SpriteState();
        st.highlightedSprite = highlight;
        st.pressedSprite = neutral;
        GetComponent<Button>().spriteState = st;
    }

    // Uses an item on the slot.
    public void UseItem()
    {
        if(!IsEmpty)
        {
            Items.Pop().Use();
            stackText.text = Items.Count > 1 ? Items.Count.ToString() : string.Empty; // edit stack number 

            if(IsEmpty) // Is the item removed the last one?
            {
                ChangeSprite(slotEmpty,slotHighlight); // if it is, change icon
                transform.parent.GetComponent<Inventory>().EmptySlots++; // Add 1 to the number of empty slots
            }

        }

    }

    // Removes the top item from the slot and returns it
    /// <returns>The removed item</returns>
    public Item RemoveItem()
    {
        if(!IsEmpty)
        {
            //Remove the item from the stack and stores it in a tmp variable
            Item tmp = items.Pop();
            //Makes sure that the correct number is shown on the slot
            stackText.text = items.Count > 1 ? items.Count.ToString() : string.Empty;
            if(IsEmpty)
            {
                ClearSlot();
            }
            //Returns the removed item
            return tmp;
        }
        return null;
    }

    // Clears the slot
    public void ClearSlot()
    {
        // Clears slot of all items, changes sprite & text
        items.Clear();
        ChangeSprite(slotEmpty,slotHighlight);
        stackText.text = string.Empty;

        if(transform.parent != null)
        {
            transform.parent.GetComponent<Inventory>().EmptySlots++;
        }
    }

    // Functions to control mouse button use
    public void OnPointerClick(PointerEventData eventData)
    {
        if(eventData.button == PointerEventData.InputButton.Right && !GameObject.Find("Hover"))
        {
            UseItem();
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        held = false;
        Invoke("OnLongPress",holdTime);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        CancelInvoke("OnLongPress");
        if (!held)
        onClick.Invoke();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        CancelInvoke("OnLongPress");
    }

    private void OnLongPress()
    {
        held = true;
        onLongPress.Invoke();
    }
    #endregion

}
