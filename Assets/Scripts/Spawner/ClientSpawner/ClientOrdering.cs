using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;
using UnityEngine.UI;

public class ClientOrdering : MonoBehaviour
{
    //Nếu như khách hàng chưa chọn chỗ ngồi hoặc chưa đến chỗ ngồi thì chưa order
    public void Order(Client client)
    {
        client.foodOrders = ChooseRandomFoods(client);
        DisplayOrder(client);
    }

    public List<ItemData> ChooseRandomFoods(Client client)
    {
        List<ItemData> orderedFoods = new List<ItemData>();
        int foodCount = ChangeRandomFood();
        int recipeCount = ChangeDifficultFood();
        
        
        List<Recipe> availableRecipes = new List<Recipe>(GameData.instance.Menu.recipesInMenu);
        
        for (int i = 0; i < foodCount && availableRecipes.Count > 0; i++)
        {
            Recipe selectedRecipe = availableRecipes[Random.Range(0, recipeCount)];

            orderedFoods.Add(selectedRecipe.foodSO);
            availableRecipes.Remove(selectedRecipe);
        }

        return orderedFoods;
    }

    private int ChangeRandomFood()
    {
        int count = 2;
        if(GameData.instance.Timer > 120)
        {
            count = Random.Range(1, 3);
            
        }else if(GameData.instance.Timer > 240)
        {
            count = Random.Range(1, 4);
        }else
        {
            count = 1;
        }

        return count;
    }

    private int ChangeDifficultFood()
    {
        int count = 4;
        if(GameData.instance.Timer > 120)
        {
            count = 8;
        }else if(GameData.instance.Timer > 300)
        {
            count = GameData.instance.Menu.recipesInMenu.Count;
        }else
        {
            count = 4;
        }

        return count;
    }




    public void DisplayOrder(Client client)
    {
        if (client.foods.transform.childCount > 0)
        {
            foreach (Transform child in client.foods.transform)
            {
                Destroy(child.gameObject);
            }
        }

        Vector3 distanceBetweenFoods = new Vector3(0, 0, 0);
        foreach (var item in client.foodOrders)
        {
            GameObject clone;
            if(item.itemName.StartsWith("To"))
            {
                clone = Instantiate(Resources.Load<GameObject>("Prefabs/EmptyTo"), client.foods.transform);
            }   
            else
            {
                clone = Instantiate(Resources.Load<GameObject>("Prefabs/EmptyDia"), client.foods.transform);
            }
            clone.GetComponent<BoxCollider2D>().enabled = false;
            clone.transform.localPosition += distanceBetweenFoods;
            distanceBetweenFoods += new Vector3(0, 200, 0);
            clone.name = item.name;
            clone.GetComponent<Image>().sprite = item.icon;
        }      
    }

}
