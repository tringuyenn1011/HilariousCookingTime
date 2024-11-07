using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewClient", menuName = "Client/New Client")]
public class ClientData : ScriptableObject
{
    public string clientName;
    public Sprite clientSprite;

    public bool isPleasure;

    public List<ItemData> listOfFoods;
}
