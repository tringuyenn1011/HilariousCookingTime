using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oscillation : MonoBehaviour
{
    public float amplitude = 1f; // Biên độ dao động
    public float frequency = 1f; // Tần số dao động
    public Vector3 direction = Vector3.up; // Hướng dao động

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
