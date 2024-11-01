using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Menu", menuName = "Items/Menu")]
public class Menu : ScriptableObject
{
    public List<Recipe> recipes; // Danh sách các công thức nấu ăn
    public List<Recipe> recipesInMenu; // Danh sách các công thức nấu ăn có trong Menu


}
