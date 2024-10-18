using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TrashBin : MonoBehaviour, IDropHandler
{
    public void OnDrop(PointerEventData eventData)
    {
        
        if(eventData.pointerDrag.gameObject.GetComponent<Food>().slotType == SlotType.Food)
        {
            GameObject prefab = Resources.Load<GameObject>("Prefabs/FoodSlot");
            Instantiate(prefab, GameObject.Find("Slot").transform);
            Destroy(eventData.pointerDrag.gameObject);
        }else if(eventData.pointerDrag.gameObject.GetComponent<Food>().slotType == SlotType.Drink)
        {
            GameObject prefab = Resources.Load<GameObject>("Prefabs/DrinkSlot");
            Instantiate(prefab, GameObject.Find("Slot").transform);
            Destroy(eventData.pointerDrag.gameObject);
        }
            
    }
}
