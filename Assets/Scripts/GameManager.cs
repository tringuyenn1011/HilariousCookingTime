using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    
    public bool isRandom = false;
    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeToMainGameScene()
    {
        SceneManager.LoadScene("MainGame");
    }

    public bool IsEventRandom(float chance)
    {
        isRandom = true;
        return Random.Range(0f, 100f) < chance;
    }
}
