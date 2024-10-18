using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewRecipe", menuName = "Items/Create New Recipe")]
public class Recipe : ScriptableObject
{
    public string foodName; // Tên món ăn
    public ItemData foodSO;
    public List<ItemData> ingredient; // Danh sách các nguyên liệu theo thứ tự
}
