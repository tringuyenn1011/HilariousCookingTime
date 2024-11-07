using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public abstract class DraggableItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
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
                
            
        // }
    }

    private bool IsDroppedOnValidSlot()
    {
        PointerEventData pointerEventData = new PointerEventData(EventSystem.current)
        {
            position = Input.mousePosition
        };

        List<RaycastResult> raycastResults = new List<RaycastResult>();

        EventSystem.current.RaycastAll(pointerEventData, raycastResults);

        foreach (RaycastResult result in raycastResults)
        {
            Food food = result.gameObject.GetComponent<Food>();
            if (food != null)
            {
                return true;
            }
        }

        return false;
    }

    public SlotType GetSlotType()
    {
        return itemData.slotType;
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
