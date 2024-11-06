using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public abstract class DraggableItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    // Tham chiếu đến Scriptable Object chứa dữ liệu
    public ItemData itemData;
    [HideInInspector]
    public RectTransform rectTransform;
    [HideInInspector]
    public Canvas canvas;
    [HideInInspector]
    public CanvasGroup canvasGroup;
    [HideInInspector]
    public GameObject clone;
    [HideInInspector]
    public Vector2 originalPosition;

    private UpDownCamera dragDrop;

    public virtual void Awake() {
        canvas = GetComponentInParent<Canvas>();  
        canvasGroup = GetComponent<CanvasGroup>();
        rectTransform = GetComponent<RectTransform>();
        dragDrop = GameObject.Find("DragDrop").GetComponent<UpDownCamera>();
        //originalPosition = rectTransform.anchoredPosition; // Lưu vị trí ban đầu
    }

    public virtual void OnBeginDrag(PointerEventData eventData)
    {
        //
        PlaySound();
        clone = Instantiate(gameObject, canvas.transform);
        rectTransform = clone.GetComponent<RectTransform>();

        if(dragDrop.isCamMove)
        {
            rectTransform.anchoredPosition += new Vector2(0,285);
        }

        
        clone.GetComponent<Image>().sprite = itemData.icon;
        clone.GetComponent<CanvasGroup>().blocksRaycasts = false;

        
    }

    public virtual void OnDrag(PointerEventData eventData)
    {
        if(clone != null)
        {
            Color color = clone.GetComponent<Image>().color;
            color.a = 255;
            clone.GetComponent<Image>().color = color;

            clone.GetComponent<CanvasGroup>().alpha = 0.9f;
            rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
        }
        
    }

    public virtual void OnEndDrag(PointerEventData eventData)
    {
        clone.GetComponent<CanvasGroup>().alpha = 1f;

        if(clone != null && !IsDroppedOnValidSlot())
        {
            Destroy(clone.gameObject);
        }else if(IsDroppedOnValidSlot())
        {
            //'canvasGroup.blocksRaycasts = true;
        }
        

        // if (eventData.pointerDrag != null && 
        //         rectTransform.anchoredPosition != originalPosition)
        // {
        //     if(transform.parent == canvas.transform)
        //     {
        //         transform.SetParent(GameObject.Find(this.itemData.slotType.ToString()).transform);
        //         rectTransform.anchoredPosition = originalPosition;
        //     }
                
            
        //      // Khôi phục vị trí
        // }
    }

    private bool IsDroppedOnValidSlot()
    {
        // Sử dụng raycast để kiểm tra đối tượng UI mà con trỏ chuột đang nằm trên
        PointerEventData pointerEventData = new PointerEventData(EventSystem.current)
        {
            position = Input.mousePosition // Lấy vị trí hiện tại của con trỏ chuột
        };

        // Tạo danh sách chứa các đối tượng mà raycast chạm vào
        List<RaycastResult> raycastResults = new List<RaycastResult>();

        // Thực hiện raycast từ vị trí chuột
        EventSystem.current.RaycastAll(pointerEventData, raycastResults);

        // Kiểm tra xem có đối tượng nào là Slot không
        foreach (RaycastResult result in raycastResults)
        {
            Food food = result.gameObject.GetComponent<Food>();
            if (food != null)
            {
                // Nếu đúng là Slot, trả về true
                return true;
            }
        }

        // Nếu không tìm thấy slot, trả về false
        return false;
    }

    public SlotType GetSlotType()
    {
        return itemData.slotType; // Trả về loại slot của món
    }

    private void PlaySound()
    {
        AudioManager audio = AudioManager.instance;
        switch (itemData.slotType)
        {
            case SlotType.Kitchenware: 
                audio.PlaySound("TakePlate");
                break;
            case SlotType.Spice:
            case SlotType.Kimchi:
            case SlotType.Meat:
            case SlotType.Ingredient: 
            case SlotType.Water:
                audio.PlaySound("TakeIngredient");
                break;
            

            default:
                break;
        }
    }
}
