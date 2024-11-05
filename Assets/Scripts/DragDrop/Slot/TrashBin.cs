using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TrashBin : MonoBehaviour, IDropHandler
{
    public void OnDrop(PointerEventData eventData)
    {
        Debug.Log("trásh");
        
        if(eventData.pointerDrag.gameObject.GetComponent<Food>().slotType == SlotType.Food)
        {
            GameObject prefab = Resources.Load<GameObject>("Prefabs/FoodSlot");
            Instantiate(prefab, GameObject.Find("Slot").transform);
            prefab.transform.position = eventData.pointerDrag.GetComponent<Food>().originalPosition;
            Destroy(eventData.pointerDrag.gameObject);
        }
            
    }
}
