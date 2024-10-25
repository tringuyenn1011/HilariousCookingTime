using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ClientSpawner : MonoBehaviour
{
    public ClientList clientList;
    public RectTransform spawnPoint;
    public ClientSeatData endPoint;
    public SeatList seatList;

    public float spawnTime;
    public float waitingTime;


    public void Start()
    {
        foreach (ClientSeatData seat in seatList.seats)
        {
            seat.isUsing = false;
        }
    }

    public void Update()
    {
        if (waitingTime < 0)
        {
            int randomIndex = Random.Range(0, clientList.listOfClients.Count);
            ClientData randomClient = clientList.listOfClients[randomIndex];
            spawnClient(randomClient);
            waitingTime = spawnTime;
        }
        else
        {
            waitingTime -= Time.deltaTime;
        }
    }
    public void spawnClient(ClientData clientData)
    {
        GameObject clone = Instantiate(Resources.Load<GameObject>("Prefabs/EmptyClient"), spawnPoint);
        clone.name = clientData.clientName;
        clone.GetComponent<Image>().sprite = clientData.clientSprite;

        Client clientComponent = clone.GetComponent<Client>();
        clientComponent.clientData = clientData;
        clientComponent.seatList = seatList;
        clientComponent.endPoint = endPoint;

    }
}
