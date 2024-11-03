using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClientMoving : MonoBehaviour
{
    
    private ClientSeatData seatStoredPosition;
    public bool isCompleted = false;

    private bool isMove = false;
    // Nếu còn chỗ trống và chưa đi qua chỗ trống thì đến chỗ trống đó
    // Nếu ngồi vào chỗ thì order
    // Nếu hết chỗ thì đi khỏi quầy
    public void SelectSeat(Client client)
    {
        if (!client.isChoosingSeat)
        {
            foreach (ClientSeatData csd in client.seatList.seats)
            {
                if (client.clientRectTransform.anchoredPosition.x >= csd.seatPosition.x && !csd.isUsing)
                {
                    client.isChoosingSeat = true;
                    client.destination = csd.seatPosition;
                    seatStoredPosition = csd;
                    csd.isUsing = true;
                    break;
                }
            }
        }
        else
        {
            if (!client.isOrdering && client.clientRectTransform.anchoredPosition.x == seatStoredPosition.seatPosition.x)
            {
                client.isOrdering = true;
            }
        }
    }

    public void Move(Client client)
    {
        SelectSeat(client);
        LeaveSeat(client);
        client.clientRectTransform.anchoredPosition = Vector2.MoveTowards(client.clientRectTransform.anchoredPosition, client.destination, client.speed * Time.deltaTime);
    }

    // Nếu thời gian phục vụ quá lâu thì sẽ nổi giận và rời khỏi chỗ ngồi
    // Nếu được phục vụ những món yêu cầu sẽ rời khỏi chỗ ngồi
    public void LeaveSeat(Client client)
    {
        if (!client.isChoosingSeat)
        {
            client.destination = client.endPoint.seatPosition;
        }

        if (client.isOrdering)
        {
            client.waitingTime -= Time.deltaTime;
            client.timerBar.value = client.waitingTime;
            // if(client.isPleasuring)
            // {
            //     GameData.instance.AddPoints(300);
            //     GameData.instance.AddMoney(Random.Range(5,16));
            // }
                
            // else if(client.waitingTime < 0)
            // {
            //     GameData.instance.AddPoints(-500);
            //     Debug.LogWarning(client.clientRectTransform.anchoredPosition);
            //     GameData.instance.LoseLife();
            // }
                

            if (client.isPleasuring || client.waitingTime < 0)
            {
                if(client.isPleasuring)
                {
                    GameData.instance.AddPoints(300);
                    GameData.instance.AddMoney(Random.Range(5,16));
                }
                    
                else if(client.waitingTime < 0)
                {
                    GameData.instance.AddPoints(-500);
                    
                    GameData.instance.LoseLife();
                    AudioManager.instance.PlaySound("AngryClient");


                }
                seatStoredPosition.isUsing = false;
                client.destination = client.endPoint.seatPosition;
                client.isOrdering = false;
                client.thinking.SetActive(false);
                client.waitingTime = 0;
                
            }
        }
    }
}
