using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Hotbar : Inventory
{
    public GameObject selectedSlot = null;
    private RectTransform hotbarRect;

    private float hotbarWidth, hotbarHeight;

    void Start()
    {
        selectedSlot = allSlots[0];
        CreateLayout();
    }

    void Update()
    {
        SelectSlot();
    }

    private void SelectSlot()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            selectedSlot = allSlots[0];
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            selectedSlot = allSlots[1];
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            selectedSlot = allSlots[2];
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            selectedSlot = allSlots[3];
        }
        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            selectedSlot = allSlots[4];
        }
        if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            selectedSlot = allSlots[5];
        }
        if (Input.GetKeyDown(KeyCode.Alpha7))
        {
            selectedSlot = allSlots[6];
        }
        if (Input.GetKeyDown(KeyCode.Alpha8))
        {
            selectedSlot = allSlots[7];
        }
        if (Input.GetKeyDown(KeyCode.Alpha9))
        {
            selectedSlot = allSlots[8];
        }
        if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            selectedSlot = allSlots[9];
        }
    }

    void CreateLayout()
    {

        hotbarWidth = 10 * (SlotSize + slotPaddingLeft) + slotPaddingLeft;
        hotbarHeight = 1 * (SlotSize + slotPaddingTop) + slotPaddingTop;

        hotbarRect = GetComponent<RectTransform>();

        hotbarRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, hotbarWidth);
        hotbarRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, hotbarHeight);

        for (int x = 0; x < 10; x++)
        {
            GameObject newSlot = slotPrefab;

            RectTransform slotRect = newSlot.GetComponent<RectTransform>();

            newSlot.name = "Slot";
            //newSlot.transform.SetParent(this.transform);

            slotRect.localPosition = new Vector3(slotPaddingLeft * (x + 1) + (SlotSize * x), -slotPaddingTop);

            slotRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, SlotSize);
            slotRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, SlotSize);

            allSlots.Add(newSlot);
        }

        EmptySlots = allSlots.Count;
    }
}