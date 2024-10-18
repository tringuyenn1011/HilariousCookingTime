using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewItem", menuName = "Items/New Item")]
public class ItemData : ScriptableObject
{
    public string itemName;
    public SlotType slotType; // Kiểu slot: Đồ ăn hoặc Đồ uống
    public Sprite icon; // Hình ảnh đại diện cho món ăn hoặc đồ uống
    public bool isCompleted;
}
