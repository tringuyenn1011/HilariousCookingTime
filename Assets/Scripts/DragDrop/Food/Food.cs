using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Image = UnityEngine.UI.Image;

public class Food : DraggableItem, IDropHandler
{
    public List<ItemData> itemsInSlot = new List<ItemData>();
    public SlotType slotType;
    private Transform parentTransform;

    private bool isTo = true;

    public Vector2 originPosition;

    private Dictionary<string, List<SlotType>> foodOrders = new Dictionary<string, List<SlotType>>()
    {
        { "Tô", new List<SlotType> { SlotType.Kitchenware, SlotType.Ingredient, SlotType.Water, SlotType.Meat, SlotType.Spice } },
        { "Dĩa", new List<SlotType> { SlotType.Kitchenware, SlotType.Ingredient, SlotType.Spice, SlotType.Spice } }
    };

    private List<SlotType> toList = new List<SlotType> { SlotType.Kitchenware, SlotType.Ingredient, SlotType.Water, SlotType.Meat, SlotType.Spice };
    private List<SlotType> diaList = new List<SlotType> { SlotType.Kitchenware, SlotType.Kimchi, SlotType.Spice, SlotType.Spice};



    public override void Awake() 
    {
        base.Awake();
        rectTransform.anchoredPosition = originPosition; 
        parentTransform = this.transform.parent;  
    }



    public override void OnBeginDrag(PointerEventData eventData)
    {
        //originalPosition = rectTransform.anchoredPosition;
        this.gameObject.transform.SetParent(canvas.transform);
        canvasGroup.blocksRaycasts = false;
    }

    public override void OnDrag(PointerEventData eventData)
    {
        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }

    public void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag != null)
        {
            var draggableItem = eventData.pointerDrag.GetComponent<DraggableItem>();
            if(eventData.pointerDrag.GetComponent<Food>() == null)
            {
                DropTo(draggableItem);
            }
            else
            {
                
            }
        }   
    }
    
    private bool IsHaveBefore(DraggableItem draggableItem)
    {
        foreach(ItemData item in itemsInSlot)
        {
            if(draggableItem.itemData == item)
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
                if(recipe.foodSO.isCompleted)
                {
                    int scoreGained = 100;
                    GameData.instance.AddPoints(scoreGained);
                    FindObjectOfType<GameFunction>().ShowPointPopup(this.GetComponent<RectTransform>().anchoredPosition, scoreGained, this.transform);
                }
                    
                //itemsInSlot.Clear();
            }
        }
    }

    

    private void CreateNewFood(Recipe recipe)
    {
        itemData = recipe.foodSO;
        if(isTo)
            clone = Instantiate(Resources.Load<GameObject>("Prefabs/EmptyTo"), this.transform);
        else
            clone = Instantiate(Resources.Load<GameObject>("Prefabs/EmptyDia"), this.transform);
        clone.name = itemData.itemName;
        clone.GetComponent<Image>().sprite = itemData.icon;
    }

    private bool DoesRecipeMatch(List<ItemData> itemsInSlot, Recipe recipe)
    {
        // if (itemsInSlot.Count != recipe.ingredient.Count)
        // {
        //     return false;
        // }

        HashSet<ItemData> slotIngredients = new HashSet<ItemData>(itemsInSlot);
        HashSet<ItemData> recipeIngredients = new HashSet<ItemData>(recipe.ingredient);

        return slotIngredients.SetEquals(recipeIngredients);

    }

    private void AddIngredient(DraggableItem draggableItem)
    {
            if(draggableItem.GetSlotType() == SlotType.Water)
            {
                AudioManager.instance.PlaySound("WaterPour");
            }
            ItemData draggedItem = draggableItem.itemData;

            RectTransform itemTransform = draggableItem.clone.GetComponent<RectTransform>();
            itemTransform.anchoredPosition = GetComponent<RectTransform>().anchoredPosition;
            itemTransform.transform.SetParent(this.gameObject.transform);

            itemsInSlot.Add(draggedItem);

            draggableItem.clone.GetComponent<CanvasGroup>().blocksRaycasts = false;
            Debug.Log("Đã thêm nguyên liệu vào slot."); 
    }

    private bool IsSpiceFirstIngredient(DraggableItem draggableItem)
    {
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
            rectTransform.anchoredPosition = originPosition;
        
    }


    private bool IsSpice(DraggableItem item)
    {
        return item.GetSlotType() == SlotType.Spice;
    }

    private bool CheckCanDropInto(DraggableItem item)
    {
        if(item.itemData != null)
        {
            SlotType type = item.GetSlotType();
            if(slotType == SlotType.Food)
                return type == SlotType.Ingredient || 
                    type == SlotType.Spice;

        }
        
        return false;
    }

    

    private void DropTo(DraggableItem draggableItem)
    {
        if(itemsInSlot.Count == 0)
        {
            if(draggableItem.GetSlotType() == SlotType.Kitchenware)
            {
                
                if(draggableItem.itemData.name == "To")
                    isTo = true;
                else if(draggableItem.itemData.name == "Dia")
                    isTo = false;
                DoCheckAndAddIngredient(draggableItem, isTo);
            }
            else
            {
                
                Destroy(draggableItem.clone.gameObject);
            }
        }else
        {
            DoCheckAndAddIngredient(draggableItem, isTo);
        }
    }

    private void DoCheckAndAddIngredient(DraggableItem draggableItem, bool isTo)
    {
        if(draggableItem.GetSlotType() == SlotType.Water)
        {
            AudioManager.instance.PlaySound("WaterPour");
        }else
        {
            AudioManager.instance.PlaySound("CraftFood");
        }
        
        if(isTo == true)
        {
            
            DoAddIngredient(draggableItem, toList);
        }else
        {
            
            DoAddIngredient(draggableItem, diaList);
        }
    }

    private void DoAddIngredient(DraggableItem draggableItem, List<SlotType> list)
    {
        if(draggableItem != null && itemsInSlot.Count < list.Count && draggableItem.GetSlotType() == list[itemsInSlot.Count] && 
            !IsHaveBefore(draggableItem))
        {
            AddIngredient(draggableItem);
            CreateFoodItem();
        }
        else
        {
            GameObject prefab = Resources.Load<GameObject>("Prefabs/FoodSlot");
            prefab.GetComponent<Food>().originPosition = originPosition;
            Instantiate(prefab, GameObject.Find("Slot").transform);

            GameObject explose = Resources.Load<GameObject>("Prefabs/Explosion");
            explose.GetComponent<RectTransform>().anchoredPosition = originPosition;
            GameObject obj = Instantiate(explose, GameObject.Find("DragDrop").transform);

            AudioManager.instance.PlaySound("Explosion");

            StartCoroutine(WaitToDestroyAnim(obj));
            Destroy(draggableItem.clone.gameObject);
        }
    }

    private IEnumerator WaitToDestroyAnim(GameObject gameObject)
    {
        
        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
        Destroy(this.gameObject);
            
    }
    


}
