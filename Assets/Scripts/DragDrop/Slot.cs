using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Slot : MonoBehaviour, IDropHandler
{
    public Menu foodMenu;
    public List<ItemData> itemsInSlot = new List<ItemData>();
    public SlotType slotType; // Kiểu slot: Đồ ăn hoặc Đồ uống
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
                // Kiểm tra xem có nguyên liệu trong slot trước khi cho phép thêm gia vị
                if (IsSpice(draggableItem) && foodToDrag.itemsInSlot.Count == 0)
                {
                    Debug.Log("Không thể thêm gia vị khi không có nguyên liệu nào!");
                    return;
                }

                // Lấy ItemData từ clone được kéo thả
                ItemData draggedItem = draggableItem.itemData;

                // Di chuyển đối tượng clone vào vị trí của slot
                RectTransform itemTransform = draggableItem.clone.GetComponent<RectTransform>();
                itemTransform.anchoredPosition = GetComponent<RectTransform>().anchoredPosition;

                itemTransform.transform.SetParent(this.gameObject.transform.GetChild(0));

                // Thêm nguyên liệu vào slot
                foodToDrag.itemsInSlot.Add(draggedItem);

                // Kiểm tra nếu nguyên liệu đã tồn tại trong slot, tắt raycast để không cản trở thao tác kéo thả tiếp theo
                draggableItem.clone.GetComponent<CanvasGroup>().blocksRaycasts = false;

                Debug.Log("Đã thêm nguyên liệu vào slot.");
            }

















            // // Kiểm tra xem đối tượng có triển khai IDraggableItem không
            // var draggableItem = eventData.pointerDrag.GetComponent<DraggableItem>();

            // if (draggableItem != null && draggableItem.GetSlotType() == slotType)
            // {
            //     // Di chuyển đối tượng vào vị trí của slot
            //     RectTransform itemTransform = eventData.pointerDrag.GetComponent<RectTransform>();
            //     itemTransform.anchoredPosition = GetComponent<RectTransform>().anchoredPosition;

            //     // Cập nhật vị trí ban đầu cho đối tượng
            //     eventData.pointerDrag.GetComponent<DraggableItem>().originalPosition = 
            //         GetComponent<RectTransform>().anchoredPosition;
            // }
        }
    }


    private bool IsSpice(DraggableItem item)
    {
        return item.GetSlotType() == SlotType.Spice; // Hoặc sử dụng SlotType.Spice nếu đã thiết lập
    }
}


