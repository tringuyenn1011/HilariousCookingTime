using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClientSlot : MonoBehaviour
{
    public Transform slotPosition { get; set; }
    [SerializeField]
    public bool isUsed = false;

    private void Start()
    {
        slotPosition = this.transform;
    }
}
