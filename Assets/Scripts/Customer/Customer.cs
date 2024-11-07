using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Customer : MonoBehaviour
{
    [SerializeField]
    private float speed = 1.0f;
    private Menu[] desire;
    public Transform destinationPoint { get; set; }
    public ClientSlot[] clientSlots;
    public Transform endPoint;

    public float waitingTime = 20f;

    private void Start()
    {
        GameObject[] slots = GameObject.FindGameObjectsWithTag("Slot");
        endPoint = GameObject.Find("EndPoint").GetComponent<Transform>();
        clientSlots = new ClientSlot[slots.Length];
        for (int i = 0; i < slots.Length; i++)
        {
            clientSlots[i] = slots[i].GetComponent<ClientSlot>();
        }

        Transform newDestination = ChooseSlot();
        if (newDestination != null)
        {
             
            destinationPoint = newDestination;
        }
        else
        {
            destinationPoint = endPoint;
        }

        waitingTime = Random.Range(100, 200);
    }

    private void Update()
    {
        Moving();
        Leave();
    }

    private void SetMenuDesire()
    {

    }
    private void Moving()
    {
        transform.position = Vector3.MoveTowards(this.transform.position, destinationPoint.position, speed * Time.deltaTime);
    }

    
    private Transform ChooseSlot()
    {
        foreach (ClientSlot s in clientSlots)
        {
            if (!s.isUsed)
            {
                s.isUsed = true;
                return s.slotPosition;
            }
        }
        return null;
    }

    //Khách hàng sẽ rời đi nếu thời gian chờ đợi lâu hoặc đồ ăn mong muốn được đáp ứng
    private void Leave()
    {
        if (waitingTime > 0)
        {
            waitingTime -= Time.deltaTime;
        }
        else
        {
            destinationPoint = endPoint;
        }
    }
}
