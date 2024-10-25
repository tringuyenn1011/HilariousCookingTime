using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewListSeat", menuName = "ClientSeat/New List Seat")]
public class SeatList : ScriptableObject
{
    public List<ClientSeatData> seats;
}
