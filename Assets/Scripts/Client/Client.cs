using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Client : MonoBehaviour
{
    public Dish[] dishDesire;
    public int ClientSpeed = 0;
    public Transform targetPosition;
    private Rigidbody2D rigidbody;
    public Sprite sprite;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        rigidbody.velocity = Vector2.left * ClientSpeed;
    }

    private void Update()
    {
        var step = ClientSpeed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, targetPosition.position, step);
    }
}
