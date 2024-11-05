using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Star : MonoBehaviour
{
    public Sprite offStar;
    public GameObject loseBG;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LoseLife()
    {
        //GameData.instance.LoseLife();
        UpdateStars();
        CheckGameOver();
    }

    

    private void UpdateStars()
    {
        GameData.instance.Lives -= 1;
        int countStar = transform.childCount; 
        int lives = GameData.instance.Lives;
        if(lives >= 0)
        {
            for(int i=countStar-1; i>lives-1; i--)
            {
                transform.GetChild(i).GetComponent<Image>().sprite = offStar;
            }
        }
    
    }
    private void CheckGameOver()
    {
        if (GameData.instance.Lives <= 0)
        {
            GameObject.Find("GameFunction").GetComponent<GameFunction>().ClientWhenGameOver();
            
        }
    }
}
