using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewClientSeat", menuName = "ClientSeat/New Client Seat")]
public class ClientSeatData : ScriptableObject
{
    public string seatName;
    public bool isUsing;
    public Vector2 seatPosition;
}
