using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SlingSlot : MonoBehaviour, IDropHandler
{
    [HideInInspector]
    public GameObject firstDrag;
    private Slingshot slingshot;
    void Awake() 
    {
           firstDrag = GameObject.Find("firstDrag");
    }
    void Start() 
    {
        
    }
    public void OnDrop(PointerEventData eventData)
    {
        DraggableItem draggableItem = eventData.pointerDrag.gameObject.GetComponent<DraggableItem>();
        

        if(draggableItem.itemData != null && draggableItem.itemData.slotType == SlotType.Food && draggableItem.itemData.isCompleted)
        {   
            Debug.Log("prepare food On Sling");
            GameObject.Find("DragDrop").GetComponent<UpDownCamera>().CallSlingshot();
            slingshot = GameObject.Find("Slingshot").GetComponent<Slingshot>(); 
            firstDrag.SetActive(false);
            slingshot.itemData = eventData.pointerDrag.gameObject.GetComponent<Food>().itemData;
            slingshot.SetFood();

            GameObject prefab = Resources.Load<GameObject>("Prefabs/FoodSlot");
            prefab.GetComponent<Food>().originPosition = eventData.pointerDrag.GetComponent<Food>().originPosition;
            Instantiate(prefab, GameObject.Find("Slot").transform);

            // prefab.GetComponent<RectTransform>().anchoredPosition = eventData.pointerDrag.GetComponent<Food>().originPosition;
            // prefab.GetComponent<Food>().originPosition = prefab.GetComponent<RectTransform>().anchoredPosition;
            Destroy(draggableItem.gameObject);
            this.gameObject.SetActive(false);
        }else
        {
            
            Debug.Log("Do An chua hoan thanh");
        }
    }
}
