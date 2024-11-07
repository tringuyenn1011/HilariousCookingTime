using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Slot : MonoBehaviour, IDropHandler
{
    public Menu foodMenu;
    public List<ItemData> itemsInSlot = new List<ItemData>();
    public SlotType slotType;
    private Food foodToDrag;

    void Awake() 
    {
        foodToDrag = this.transform.GetChild(0).GetComponent<Food>();    
    }

    public void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag != null)
        {
            var draggableItem = eventData.pointerDrag.GetComponent<DraggableItem>();

            if (draggableItem != null)
            {
                if (IsSpice(draggableItem) && foodToDrag.itemsInSlot.Count == 0)
                {
                    Debug.Log("Không thể thêm gia vị khi không có nguyên liệu nào!");
                    return;
                }

                ItemData draggedItem = draggableItem.itemData;

                RectTransform itemTransform = draggableItem.clone.GetComponent<RectTransform>();
                itemTransform.anchoredPosition = GetComponent<RectTransform>().anchoredPosition;

                itemTransform.transform.SetParent(this.gameObject.transform.GetChild(0));

                foodToDrag.itemsInSlot.Add(draggedItem);

                draggableItem.clone.GetComponent<CanvasGroup>().blocksRaycasts = false;

                Debug.Log("Đã thêm nguyên liệu vào slot.");
            }
        }
    }


    private bool IsSpice(DraggableItem item)
    {
        return item.GetSlotType() == SlotType.Spice;
    }
}


