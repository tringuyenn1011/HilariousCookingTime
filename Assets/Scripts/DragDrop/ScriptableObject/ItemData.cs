using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewItem", menuName = "Items/New Item")]
public class ItemData : ScriptableObject
{
    public string itemName;
    public SlotType slotType;
    public Sprite icon;
    public bool isCompleted;
}
