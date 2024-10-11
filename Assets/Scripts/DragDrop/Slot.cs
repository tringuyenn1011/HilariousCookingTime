using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Slot : MonoBehaviour, IDropHandler
{
    public SlotType slotType; // Kiểu slot: Đồ ăn hoặc Đồ uống
    private PointerEventData temp = null;
    public void OnDrop(PointerEventData eventData)
    {
        Debug.Log("OnDrop");
        if(eventData.pointerDrag != null)
        {
            eventData.pointerDrag.GetComponent<RectTransform>().anchoredPosition = 
                GetComponent<RectTransform>().anchoredPosition;
            eventData.pointerDrag.GetComponent<MouseController>().originalPosition = GetComponent<RectTransform>().anchoredPosition;
        }
    }
}

public enum SlotType
{
    Food,
    Drink
}