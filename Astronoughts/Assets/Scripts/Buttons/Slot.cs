using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Events;
using UnityEngine.EventSystems;


public class Slot : MonoBehaviour, IPointerClickHandler, IPointerDownHandler, IPointerUpHandler, IPointerExitHandler
{
    private Stack<Item> items;

    // The amount of items to pickup (this is the text on the UI element we use for splitting)
    public Text stackText;

    // The slot's empty sprite
    public Sprite slotEmpty;

    // The slot's highlighted sprite
    public Sprite slotHighlight;

    [SerializeField]
    [Tooltip("How long must pointer be down on this object to trigger a long press")]
    private float holdTime = 1f;

    private bool held = false;
    public UnityEvent onClick = new UnityEvent();
    public UnityEvent onLongPress = new UnityEvent();

    // Indicates if the slot is empty
    public bool IsEmpty
    {
        get
        {
            return Items.Count == 0;
        }
    }

    // Indicates if the slot is avaialble for stacking more items
    public bool IsAvailable
    {
        get
        {
            return CurrentItem.maxSize > Items.Count;
        }
    }

    // Returns the current item in the stack
    public Item CurrentItem
    {
        get
        {
            return Items.Peek();
        }
    }

    public Stack<Item> Items
    {
        get => items;
        set => items = value;
    }

    void Start()
    {
        //Instantiates the items stack
        Items = new Stack<Item>();

        //Creates a reference to the slot slot's recttransform
        RectTransform slotRect = GetComponent<RectTransform>();

        //Creates a reference to the stackText's recttransform
        RectTransform textRect = stackText.GetComponent<RectTransform>();

        //Calculates the scalefactor of the text by taking 60% of the slots width
        int textScaleFactor = (int)(slotRect.sizeDelta.x * 0.20);

        //Sets the min and max textSize of the stackText
        stackText.resizeTextMaxSize = textScaleFactor;
        stackText.resizeTextMinSize = textScaleFactor;

        //Sets the actual size of the textRect
        textRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal,slotRect.sizeDelta.x);
        textRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical,slotRect.sizeDelta.y);
    }

    public void Update()
    {
    }

    /// Adds a single item to the inventory
    /// <param name="item">The item to add</param>
    public void AddItem(Item item)
    {
        if(IsEmpty) //if the slot is empty
        {
           Inventory.EmptySlots--; //Reduce the number of empty slots
        }
        Items.Push(item); //Adds the item to the stack
        if(Items.Count > 1) //Checks if we have a stacked item
        {
            stackText.text = Items.Count.ToString(); //If the item is stacked then we need to write the stack number on top of the icon
        }
        ChangeSprite(item.spriteNeutral,item.spriteHighlighted); //Changes the sprite so that it reflects the item the slot is occupied by
    }

    /// Adds a stack of items to the slot
    /// <param name="items">The stack of items to add</param>
    public void AddItems(Stack<Item> items)
    {
        if(IsEmpty) //if the slot is empty
        {
            Inventory.EmptySlots--; //Reduce the number of empty slots
        }
        this.Items = new Stack<Item>(items); //Adds the stack of items to the slot
        stackText.text = items.Count > 1 ? items.Count.ToString() : string.Empty; //Writes the correct stack number on the icon
        ChangeSprite(CurrentItem.spriteNeutral,CurrentItem.spriteHighlighted); //Changes the sprite so that it reflects the item the slot is occupied by
    }

    private void ChangeSprite(Sprite neutral,Sprite highlight)
    {
        //Sets the neutralsprite
        GetComponent<Image>().sprite = neutral;

        //Creates a spriteState, so that we can change the sprites of the different states
        SpriteState st = new SpriteState();
        st.highlightedSprite = highlight;
        st.pressedSprite = neutral;

        //Sets the sprite state
        GetComponent<Button>().spriteState = st;
    }

    // Uses an item on the slot.
    public void UseItem()
    {
        if(!IsEmpty)
        {
            Items.Pop().Use();
            stackText.text = Items.Count > 1 ? Items.Count.ToString() : string.Empty; //Writes the correct stack number 

            if(IsEmpty) //Checks if we just removed the last item from the inventory
            {
                ChangeSprite(slotEmpty,slotHighlight); //Changes the sprite to empty if the slot is empty
                Inventory.EmptySlots++; //Adds 1 to the amount of empty slots
            }

        }

    }

    // Clears the slot
    public void ClearSlot()
    {
        //Clears all items on the slot
        items.Clear();

        //Changes the sprite to empty
        ChangeSprite(slotEmpty,slotHighlight);

        //Clears the text
        stackText.text = string.Empty;

        if(transform.parent != null)
        {
            Inventory.EmptySlots++;
        }
    }

    /// Handles OnPointer events
    /// <param name="eventData"></param>
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

}
