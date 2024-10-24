using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection.Emit;
using UnityEngine;
using UnityEngine.UI;

public class Currency : MonoBehaviour
{

    private Text data; 
    void Awake() 
    {
        data = transform.Find("Label").GetComponent<Text>();
    }

    // Start is called before the first frame update
    void Start()
    {

        data.text = "0";
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddMoney(int money)
    {
        GameData.instance.Money += money;
        data.text = string.Format("{0}",GameData.instance.Money);
        
    }

}
