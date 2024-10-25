using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEditor.PackageManager;
using UnityEngine;
using UnityEngine.UI;

public class ClientOrdering : MonoBehaviour
{
    //Nếu như khách hàng chưa chọn chỗ ngồi hoặc chưa đến chỗ ngồi thì chưa order
    public void Order(Client client)
    {
        client.foodOrders = ChooseRandomFoods(client);
        DisplayOrder(client);
    }

    public List<ItemData> ChooseRandomFoods(Client client)
    {
        List<ItemData> orderedFoods = new List<ItemData>();
        // Chọn số lượng món ăn ngẫu nhiên từ 1 đến 3
        int foodCount = Random.Range(1, 4);

        // Tạo một danh sách các món ăn còn lại
        List<ItemData> availableFoods = new List<ItemData>(client.clientData.listOfFoods);
        
        // Đảm bảo danh sách có đủ món để chọn
        for (int i = 0; i < foodCount && availableFoods.Count > 0; i++)
        {
            // Chọn ngẫu nhiên một món ăn
            ItemData selectedFood = availableFoods[Random.Range(0, availableFoods.Count)];

            orderedFoods.Add(selectedFood);
            // Loại bỏ món ăn đã chọn để không bị trùng
            availableFoods.Remove(selectedFood);
        }

        return orderedFoods;
    }

    public void DisplayOrder(Client client)
    {
        if (client.foods.transform.childCount > 0)
        {
            foreach (Transform child in client.foods.transform)
            {
                Destroy(child.gameObject);
            }
        }

        Vector3 distanceBetweenFoods = new Vector3(0, 0, 0);
        foreach (var item in client.foodOrders)
        {
            GameObject clone = Instantiate(Resources.Load<GameObject>("Prefabs/EmptyFood"), client.foods.transform);
            clone.transform.localPosition += distanceBetweenFoods;
            distanceBetweenFoods += new Vector3(0, 120, 0);
            clone.name = item.name;
            clone.GetComponent<Image>().sprite = item.icon;
        }      
    }

}
