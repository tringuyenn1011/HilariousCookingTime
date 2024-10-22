using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewClientList", menuName = "Client/New Client List")]
public class ClientList : ScriptableObject
{
    public List<ClientData> listOfClients;
}
