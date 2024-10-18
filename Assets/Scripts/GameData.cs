using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameData
{
    public static Menu Menu;

    [RuntimeInitializeOnLoadMethod]
    static void Init()
    {
        Menu = Resources.Load<Menu>("Menu");
        if(Menu == null)
        {
            Debug.LogError("Không tìm thấy Menu trong Resources!");
        }
    }
}

public enum SlotType
{
    Food,
    Drink,
    Ingredient,
    Spice,
}
