using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Slot : MonoBehaviour, IPointerClickHandler
{
    private Stack<Item> items;
    public Stack<Item> Items { get => items; set => items = value; }

    public Text stackText;

    public Sprite slotEmpty;

    public bool IsEmpty
    {
        get { return Items.Count == 0; }
    }

    void Start()
    {
        Items = new Stack<Item>();

        RectTransform slotRect = GetComponent<RectTransform>();
        RectTransform textRect = stackText.GetComponent<RectTransform>();

        int textScaleFactor = (int)(slotRect.sizeDelta.x * 0.35);

        stackText.resizeTextMaxSize = textScaleFactor;
        stackText.resizeTextMinSize = textScaleFactor;

        textRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, slotRect.sizeDelta.x);
        textRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, slotRect.sizeDelta.y);
    }

    void Update()
    {

    }

    public bool IsAvailable
    {
        get { return CurrentItem.MaxSize > Items.Count; }
    }

    public Item CurrentItem
    {
        get { return Items.Peek(); }
    }


    public void AddItem(Item item)
    {
        Items.Push(item);

        if (Items.Count > 1)
        {
            stackText.text = Items.Count.ToString();
        }

        ChangeSprite(item.InventorySprite);
    }

    public void AddItems(Stack<Item> items)
    {
        this.Items = new Stack<Item>(items);

        stackText.text = items.Count > 1 ? items.Count.ToString() : string.Empty;

        ChangeSprite(CurrentItem.InventorySprite);
    }

    private void ChangeSprite(Sprite neutralSprite)
    {
        GetComponent<Image>().sprite = neutralSprite;

        SpriteState st = new SpriteState();

        st.pressedSprite = neutralSprite;

        GetComponent<Button>().spriteState = st;
    }

    public void UseItem()
    {
        if (!IsEmpty)
        {
            Items.Pop().Use();

            stackText.text = Items.Count > 1 ? Items.Count.ToString() : string.Empty;

            if (IsEmpty)
            {
                ChangeSprite(slotEmpty);
                Inventory.EmptySlots++;
            }
        }
    }

    public void ClearSlot()
    {
        items.Clear();
        ChangeSprite(slotEmpty);
        stackText.text = string.Empty;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Right && !GameObject.Find("Hover") && Inventory.CanvasGroup.alpha > 0)
        {
            UseItem();
        }
        if (eventData.button == PointerEventData.InputButton.Right && GameObject.Find("Hover") && Inventory.CanvasGroup.alpha > 0)
        {
            if (Inventory.from.Items.Count > 1)
                AddItem(Inventory.from.Items.Pop());
        }
    }
}