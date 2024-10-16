using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Customer Slots Scriptable Object", menuName = "ScriptableObjects/CustomerSlotScriptableObject")]
public class ScriptableObjectSlots : ScriptableObject
{
    public List<GameObject> clientSlots;
    public GameObject endPoint;
}
