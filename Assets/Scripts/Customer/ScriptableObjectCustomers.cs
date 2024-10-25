using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Customers Scriptable Object", menuName = "ScriptableObjects/CustomersScriptableObject")]
public class ScriptableObjectCustomers : ScriptableObject
{
    public List<GameObject> listOfCustomers;
}
