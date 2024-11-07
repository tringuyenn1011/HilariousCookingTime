using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using Unity.VisualScripting;
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

    public bool isChoosingSeat = false;

    public bool isPleasuring = false;

    public bool isOrdering = false;
    public bool isChoosingFoods = false;

    public float waitingTime = 20f;
    [HideInInspector]
    public RectTransform clientRectTransform;

    public GameObject shoot;
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
        this.GetComponent<CanvasGroup>().alpha = 0.5f;
    }

    private void Update()
    {
        clientMoving.Move(this);
        if (isOrdering && !isChoosingFoods)
        {
            this.gameObject.GetComponent<BoxCollider>().enabled = true;
            Debug.Log("Khách hàng Order");
            AudioManager.instance.PlaySound("ClientOrder");
            thinking.SetActive(true);
            clientOrdering.Order(this);

            timerBar.maxValue = SetTimeToWait();
            timerBar.value = timerBar.maxValue;
            waitingTime = timerBar.maxValue;
            
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
            this.gameObject.GetComponent<BoxCollider>().enabled = false;
        }
    }

    private float SetTimeToWait()
    {
        float time = 0;
        if(foodOrders.Count == 2)
            time = Random.Range(40f, 50f);
        else if(foodOrders.Count == 3)
            time = Random.Range(50f, 55f);
        else
            time = Random.Range(39f, 40f);
        
        return time;
    }

    private void OnTriggerEnter(Collider collision)
    {
        Bullet bullet = collision.GetComponent<Bullet>();
        
        if (!isPleasuring && bullet.itemData.slotType == SlotType.Food)
        {
            string foodName = collision.gameObject.name;
            var itemToRemove = foodOrders.FirstOrDefault(item => item.name == foodName);
            if (itemToRemove != null)
            {
                Debug.Log("giaothanhcong");
                GameData.instance.AddPoints(200);
                FindObjectOfType<GameFunction>().ShowPointPopup(this.GetComponent<RectTransform>().anchoredPosition, 200, this.transform);
                GameData.instance.AddMoney(50);
                FindObjectOfType<GameFunction>().ShowMoneyPopup(this.GetComponent<RectTransform>().anchoredPosition, 50, this.transform);
                foodOrders.Remove(itemToRemove);
            }
            else
            {
                //Vứt cái bánh nơi mặt
                GameData.instance.AddPoints(-100);
                FindObjectOfType<GameFunction>().ShowPointPopup(this.GetComponent<RectTransform>().anchoredPosition, -100, this.transform);

                if(GameObject.Find("GameManager").GetComponent<GameManager>().IsEventRandom(50f))
                {
                    InstantiateBullet(bullet);
                }
                
            }

            clientOrdering.DisplayOrder(this);

        }

        

        Destroy(collision.gameObject);

    }

    public void InstantiateBullet(Bullet bullet)
    {
        
        GameObject prefab = Resources.Load<GameObject>("Prefabs/EmptyProjectile");
        prefab.GetComponent<Bullet>().isSlingshot = false;
        GameObject foodPrefab = Instantiate(prefab, shoot.transform.position, Quaternion.identity,GameObject.Find("Canvas").transform.GetChild(0));
        foodPrefab.name = bullet.itemData.name;
        foodPrefab.GetComponent<Image>().sprite = bullet.itemData.icon;
        foodPrefab.GetComponent<Image>().SetNativeSize();
        foodPrefab.GetComponent<Bullet>().itemData = bullet.itemData;
    }

}
