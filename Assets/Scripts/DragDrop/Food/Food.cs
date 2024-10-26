using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Image = UnityEngine.UI.Image;

public class Food : DraggableItem, IDropHandler
{
    public List<ItemData> itemsInSlot = new List<ItemData>();
    public SlotType slotType;
    private Transform parentTransform;



    public override void Awake() 
    {
        base.Awake();
        parentTransform = this.transform.parent;  
    }



    public override void OnBeginDrag(PointerEventData eventData)
    {
        originalPosition = rectTransform.anchoredPosition;
        this.gameObject.transform.SetParent(canvas.transform);
        canvasGroup.blocksRaycasts = false;
    }

    public override void OnDrag(PointerEventData eventData)
    {
        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }

    public void OnDrop(PointerEventData eventData)
    {
        Debug.Log("Oke?");
        if (eventData.pointerDrag != null)
        {
            var draggableItem = eventData.pointerDrag.GetComponent<DraggableItem>();
            
            if(itemsInSlot.Count == 0 && draggableItem.GetSlotType() == SlotType.Kitchenware)
            {
                Debug.Log(1);
                    AddIngredient(draggableItem);
            }else if (draggableItem != null && CheckCanDropInto(draggableItem) && !IsHaveBefore(draggableItem)
                        && itemsInSlot.Count != 0)
            {
                Debug.Log(2);
                AddIngredient(draggableItem);
                if(itemsInSlot.Count >= 2)
                    CreateFoodItem();
            }else
            {
                Debug.Log(3);
                Destroy(draggableItem.clone.gameObject);
            }
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
    
    private bool IsHaveBefore(DraggableItem draggableItem)
    {
        foreach(Transform child in this.transform)
        {
            if(draggableItem.itemData == child.GetComponent<DraggableItem>().itemData)
            {
                Debug.LogError("Đã có nguyên liệu: " + draggableItem.itemData.itemName);
                return true;
            }
                
        }

        return false;
    }

    private void CreateFoodItem()
    {
        foreach(var recipe in GameRecipe.Menu.recipes)
        {
            if(DoesRecipeMatch(itemsInSlot, recipe))
            {
                foreach (Transform child in this.transform)
                {
                    Destroy(child.gameObject);
                }
                Debug.Log("Tạo thành món: " + recipe.foodName);
                CreateNewFood(recipe);
                itemsInSlot.Clear();
            }
        }
    }

    

    private void CreateNewFood(Recipe recipe)
    {
        itemData = recipe.foodSO;
        //Tạo thành đồ ăn rồi sinh ra Object Clone chứa đồ ăn
        clone = Instantiate(Resources.Load<GameObject>("Prefabs/EmptyFood"), this.transform);
        clone.name = itemData.itemName;
        clone.GetComponent<Image>().sprite = itemData.icon;
    }

    private bool DoesRecipeMatch(List<ItemData> itemsInSlot, Recipe recipe)
    {
        if (itemsInSlot.Count != recipe.ingredient.Count)
        {
            return false;
        }

        HashSet<ItemData> slotIngredients = new HashSet<ItemData>(itemsInSlot);
        HashSet<ItemData> recipeIngredients = new HashSet<ItemData>(recipe.ingredient);

        return slotIngredients.SetEquals(recipeIngredients);

    }

    private void AddIngredient(DraggableItem draggableItem)
    {
        
        if(IsSpiceFirstIngredient(draggableItem))
        {
            return;
        }else
        {
            ItemData draggedItem = draggableItem.itemData;

            // Di chuyển đối tượng clone vào vị trí của slot
            RectTransform itemTransform = draggableItem.clone.GetComponent<RectTransform>();
            itemTransform.anchoredPosition = GetComponent<RectTransform>().anchoredPosition;
            itemTransform.transform.SetParent(this.gameObject.transform);

            itemsInSlot.Add(draggedItem);

            // Kiểm tra nếu nguyên liệu đã tồn tại trong slot, tắt raycast để không cản trở thao tác kéo thả tiếp theo
            draggableItem.clone.GetComponent<CanvasGroup>().blocksRaycasts = false;

            Debug.Log("Đã thêm nguyên liệu vào slot."); 
        }
    }

    private bool IsSpiceFirstIngredient(DraggableItem draggableItem)
    {
        // Kiểm tra xem có nguyên liệu trong slot trước khi cho phép thêm gia vị
        if (IsSpice(draggableItem) && itemsInSlot.Count == 1)
        {
            Debug.Log("Không thể thêm gia vị khi không có nguyên liệu nào!");
            Destroy(draggableItem.clone.gameObject);
            return true;
        }
        return false;
    }

    private bool IsFirstKitchenware(DraggableItem draggableItem)
    {
        if(itemsInSlot.Count == 0 || itemsInSlot[0].slotType == SlotType.Kitchenware)
        {
            return true;
        }
        
        return false;
    }
    
    public override void OnEndDrag(PointerEventData eventData)
    {
            transform.SetParent(parentTransform);

            canvasGroup.blocksRaycasts = true;
            rectTransform.anchoredPosition = originalPosition;
        
    }


    private bool IsSpice(DraggableItem item)
    {
        return item.GetSlotType() == SlotType.Spice; // Hoặc sử dụng SlotType.Spice nếu đã thiết lập
    }

    private bool CheckCanDropInto(DraggableItem item)
    {
        if(item.itemData != null)
        {
            SlotType type = item.GetSlotType();
            if(slotType == SlotType.Food)
                return type == SlotType.Ingredient || 
                    type == SlotType.Spice;

            else if (slotType == SlotType.Drink)
                return type == SlotType.Drink;
        }
        
        return false;
    }

    


}
