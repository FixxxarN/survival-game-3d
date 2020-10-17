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
        selectedSlot = _allSlots[0];
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
            selectedSlot = _allSlots[0];
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            selectedSlot = _allSlots[1];
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            selectedSlot = _allSlots[2];
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            selectedSlot = _allSlots[3];
        }
        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            selectedSlot = _allSlots[4];
        }
        if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            selectedSlot = _allSlots[5];
        }
        if (Input.GetKeyDown(KeyCode.Alpha7))
        {
            selectedSlot = _allSlots[6];
        }
        if (Input.GetKeyDown(KeyCode.Alpha8))
        {
            selectedSlot = _allSlots[7];
        }
        if (Input.GetKeyDown(KeyCode.Alpha9))
        {
            selectedSlot = _allSlots[8];
        }
        if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            selectedSlot = _allSlots[9];
        }
    }

    void CreateLayout()
    {

        hotbarWidth = 10 * (_slotSize + _slotPaddingLeft) + _slotPaddingLeft;
        hotbarHeight = 1 * (_slotSize + _slotPaddingTop) + _slotPaddingTop;

        hotbarRect = GetComponent<RectTransform>();

        hotbarRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, hotbarWidth);
        hotbarRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, hotbarHeight);

        for (int x = 0; x < 10; x++)
        {
            GameObject newSlot = _slotPrefab;

            RectTransform slotRect = newSlot.GetComponent<RectTransform>();

            newSlot.name = "Slot";
            //newSlot.transform.SetParent(this.transform);

            slotRect.localPosition = new Vector3(_slotPaddingLeft * (x + 1) + (_slotSize * x), -_slotPaddingTop);

            slotRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, _slotSize);
            slotRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, _slotSize);

            _allSlots.Add(newSlot);
        }

        EmptySlots = _allSlots.Count;
    }
}