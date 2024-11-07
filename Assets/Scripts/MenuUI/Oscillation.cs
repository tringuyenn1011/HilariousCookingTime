using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oscillation : MonoBehaviour
{
    public float amplitude = 1f; 
    public float frequency = 1f; 
    public Vector3 direction = Vector3.up;

    private Vector3 startPosition;

    void Start()
    {
        startPosition = transform.position;
    }

    void Update()
    {
        float oscillation = Mathf.Sin(Time.time * frequency) * amplitude;
        transform.position = startPosition + direction * oscillation;
    }
}
