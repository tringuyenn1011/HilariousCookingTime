using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SlingSlot : MonoBehaviour, IDropHandler
{
    private Slingshot slingshot;
    void Awake() 
    {
           
    }
    void Start() 
    {
        
    }
    public void OnDrop(PointerEventData eventData)
    {
        slingshot = GameObject.Find("Slingshot").GetComponent<Slingshot>(); 
        Debug.Log("prepare food On Sling");
        if(eventData.pointerDrag.gameObject.GetComponent<DraggableItem>().itemData.slotType == SlotType.Food)
        {
            slingshot.itemData = eventData.pointerDrag.gameObject.GetComponent<Food>().itemData;
            slingshot.SetFood();
            this.gameObject.SetActive(false);
        }
        // else if(eventData.pointerDrag.gameObject.GetComponent<Food>().slotType == SlotType.Drink)
        // {
        //     GameObject prefab = Resources.Load<GameObject>("Prefabs/DrinkSlot");
        //     Instantiate(prefab, GameObject.Find("Slot").transform);
        //     Destroy(eventData.pointerDrag.gameObject);
        // }
    }
}
