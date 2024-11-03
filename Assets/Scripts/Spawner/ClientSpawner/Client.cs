using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using Unity.VisualScripting;
using UnityEditor.PackageManager;
using UnityEngine;
using UnityEngine.UI;

public class Client : MonoBehaviour
{
    public ClientData clientData;
    public SeatList seatList;

    public ClientSeatData endPoint;
    public Vector2 destination;
    public int speed = 500;

    private ClientMoving clientMoving;
    private ClientOrdering clientOrdering;

    public GameObject thinking;
    public GameObject foods;
    public Slider timerBar;

    public List<ItemData> foodOrders;

    // Kiểm tra xem khách hàng đã chọn chỗ ngồi chưa
    public bool isChoosingSeat = false;

    public bool isPleasuring = false;

    public bool isOrdering = false;
    public bool isChoosingFoods = false;

    public float waitingTime = 20f;
    [HideInInspector]
    public RectTransform clientRectTransform;
    private void Awake()
    {
        clientRectTransform = GetComponent<RectTransform>();
        clientMoving = GetComponent<ClientMoving>();
        clientOrdering = GetComponent<ClientOrdering>();

        thinking = this.transform.Find("Thinking").gameObject;
        foods = thinking.transform.Find("Foods").gameObject;
    }

    public void Start()
    {
        thinking.SetActive(false);
        timerBar.maxValue = waitingTime;
        timerBar.value = waitingTime;
    }

    private void Update()
    {
        clientMoving.Move(this);
        if (isOrdering && !isChoosingFoods)
        {
            Debug.Log("Khách hàng Order");
            AudioManager.instance.PlaySound("ClientOrder");
            thinking.SetActive(true);
            clientOrdering.Order(this);
            isChoosingFoods = true;
        }

        if (clientRectTransform.anchoredPosition.x == endPoint.seatPosition.x)
        {
            Destroy(this.gameObject);
        }

        if (isChoosingFoods && foodOrders.Count == 0)
        {
            //Khách hàng rời đi
            isPleasuring = true;
            thinking.SetActive(false);
        }
    }

    //Nếu tag là food
    //  tên món ăn trùng với foodOrder
    //      thì foodOrders remove món ăn đó (foodOrders = 0 thì khách hàng thỏa mãn)
    //      nếu không phải thì khách hàng tức giận
    private void OnTriggerEnter(Collider collision)
    {
        
        if (!isPleasuring && collision.GetComponent<Bullet>().itemData.slotType == SlotType.Food)
        {
            string foodName = collision.gameObject.name;
            var itemToRemove = foodOrders.FirstOrDefault(item => item.name == foodName);
            if (itemToRemove != null)
            {
                Debug.LogWarning("giaothanhcong");
                GameData.instance.AddPoints(200);
                GameData.instance.AddMoney(50);
                foodOrders.Remove(itemToRemove);
            }
            else
            {
                //Vứt cái bánh nơi mặt
                GameData.instance.AddPoints(-100);
            }

            clientOrdering.DisplayOrder(this);

            // GameObject prefab = Resources.Load<GameObject>("Prefabs/FoodSlot");
            // Instantiate(prefab, GameObject.Find("Slot").transform);
            // //prefab.transform.position = collision.GetComponent<Food>().originalPosition;
        }

        

        Destroy(collision.gameObject);

    }

}
