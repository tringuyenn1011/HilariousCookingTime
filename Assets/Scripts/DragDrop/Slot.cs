using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Slot : MonoBehaviour, IDropHandler
{
    public SlotType slotType; // Kiểu slot: Đồ ăn hoặc Đồ uống

    public void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag != null)
        {
            // Kiểm tra xem đối tượng có triển khai IDraggableItem không
            var draggableItem = eventData.pointerDrag.GetComponent<DraggableItem>();

            if (draggableItem != null && draggableItem.GetSlotType() == slotType)
            {
                // Di chuyển đối tượng vào vị trí của slot
                RectTransform itemTransform = eventData.pointerDrag.GetComponent<RectTransform>();
                itemTransform.anchoredPosition = GetComponent<RectTransform>().anchoredPosition;

                // Cập nhật vị trí ban đầu cho đối tượng
                eventData.pointerDrag.GetComponent<DraggableItem>().originalPosition = 
                    GetComponent<RectTransform>().anchoredPosition;
            }
        }
    }
}

public enum SlotType
{
    Food,
    Drink
}