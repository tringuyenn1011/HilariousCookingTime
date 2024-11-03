using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Point : MonoBehaviour
{
    private Text data;
    void Awake() 
    {
        data = transform.Find("Label").GetComponent<Text>();
    }
    void Start()
    {
        data.text = string.Format("Point: {0}", 0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddPoint(int point)
    {
        GameData.instance.Point += point;
        if(GameData.instance.Point <0)
            GameData.instance.Point = 0;
        data.text = string.Format("Point: {0}",GameData.instance.Point);
    }
}
