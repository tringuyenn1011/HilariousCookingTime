using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClientBuilder
{
    private Client _client;
    public ClientBuilder()
    {
        _client = new Client();
    }

    public ClientBuilder setSprite(Sprite[] sprites)
    {
        int randomSprite = Random.Range(0, sprites.Length);
        _client.sprite = sprites[randomSprite];
        return this;
    }
    public ClientBuilder setDesire(Dish[] dishes)
    {
        int firstDish = Random.Range(0, dishes.Length);
        int secondDish = Random.Range(0, dishes.Length);
        _client.dishDesire = new Dish[] { dishes[firstDish], dishes[secondDish] };
        return this;
    }

    public ClientBuilder setSpeed(int speed)
    {
        _client.ClientSpeed = speed;
        return this;
    }

    public ClientBuilder setTargetPosition(Transform targetPosition)
    {
        _client.targetPosition = targetPosition;
        return this;
    }

    public Client build()
    {
        return _client;
    }
}
