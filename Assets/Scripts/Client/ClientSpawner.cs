using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ClientSpawner2 : MonoBehaviour
{
    public ScriptableObjectCustomers customers;
    private int customerQuantities = 0;
    public RectTransform spawnPoint;
    public RectTransform endPoint;
    public ClientSlot[] slotPoints;
    public float spawnInterval = 2f;
    private float timer;

    private void Awake()
    {
        customerQuantities = customers.listOfCustomers.Count();
    }

    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= spawnInterval)
        {
            SpawnObject();
            timer = 0f;
        }
    }

    Transform IsSlotEmpty()
    {
        foreach (ClientSlot s in slotPoints)
        {
            if (!s.isUsed)
            {
                return s.slotPosition;
            }
        }
        return null;
    }

    void SpawnObject()
    {
        int random = Random.Range(0, customerQuantities);
        GameObject customerObject = customers.listOfCustomers[random];
        customerObject.GetComponent<Customer>().destinationPoint = endPoint;
        
        Instantiate(customerObject, spawnPoint.transform.position, spawnPoint.transform.rotation);
    }
}
