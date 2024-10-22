using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewClient", menuName = "Client/New Client")]
public class ClientData : ScriptableObject
{
    public string clientName;
    public Sprite clientSprite;

    //true nếu khách hàng được phục vụ đúng và đủ món ăn trong thời gian quy định
    public bool isPleasure;

    //List chứa các món ăn mà khách hàng có thể gọi
    public List<ItemData> listOfFoods;
}
