using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MenuFunction : MonoBehaviour
{
    public GameObject leaderBoard;
    public GameObject titleGame;
    public GameObject batTransition;
    public GameObject dragDown;
    public GameObject dragUp;
    public TMP_InputField playerName;

    public GameObject[] highScoreTexts;

    private GameManager gameManager;
    void Awake() 
    {
        leaderBoard.SetActive(false);
        titleGame.SetActive(true);
        batTransition.SetActive(false);
        dragDown.SetActive(true);
        dragUp.SetActive(false);

    }
    // Start is called before the first frame update
    void Start()
    {
        playerName.characterLimit = 6;

        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        AudioManager.instance.PlayMusic("MenuBackgroundMusic");

        LoadHighScores();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ShowLeaderBoard()
    {
        leaderBoard.SetActive(true);
        //titleGame.SetActive(false);
        dragDown.SetActive(false);
        dragUp.SetActive(true);
    }

    public void HideLeaderBoard()
    {
        //titleGame.transform.position = Vector3.zero;
        //titleGame.SetActive(true);
        dragDown.SetActive(true);
        dragUp.SetActive(false);
        //ExitAndDisable();
    }

    public void StartGame()
    {
        if(!string.IsNullOrWhiteSpace(playerName.text) && playerName.text.Length > 0)
        {
            batTransition.SetActive(true);
            AnimatorStateInfo stateInfo = batTransition.transform.GetChild(0).GetComponent<Animator>().GetCurrentAnimatorStateInfo(0);
            StartCoroutine(WaitForAnimationEnd(stateInfo.length));

            GameData.instance.PlayerName = playerName.text;
            GameData.instance.Timer = 0;
        }
        
            
        
    }

    private IEnumerator WaitForAnimationEnd(float duration)
    {
        yield return new WaitForSeconds(duration);
        
        gameManager.ChangeToMainGameScene();
    }

    public void ExitAndDisable()
    {
        leaderBoard.GetComponent<Animator>().SetTrigger("Exit");

        Invoke("DisableGameObject", 1.5f);
    }

    private void DisableGameObject()
    {
        leaderBoard.SetActive(false);
        HideLeaderBoard();
    }

    private void LoadHighScores() 
    {
        bool hasData = false; 
        for (int i = 0; i < 5; i++) {
            int score = PlayerPrefs.GetInt($"HighScore_{i}_Score", 0);
            string name = PlayerPrefs.GetString($"HighScore_{i}_Name", "Player");

            string nameText = "NoData"; 
            string scoreText = "No Data";

            if (score > 0) {
                hasData = true; 
                if (i < highScoreTexts.Length) 
                {
                    nameText = $"{name}";
                    scoreText = $"{score}";
                }
            } 

            highScoreTexts[i].transform.GetChild(0).GetComponent<TMP_Text>().text = nameText;
            highScoreTexts[i].transform.GetChild(1).GetComponent<TMP_Text>().text = scoreText; 
        }
        if (!hasData) 
        {
            for (int i = 0; i < highScoreTexts.Length; i++) 
            {
                highScoreTexts[i].transform.GetChild(0).GetComponent<TMP_Text>().text = $"Nodata";
                highScoreTexts[i].transform.GetChild(1).GetComponent<TMP_Text>().text = $"No data";
            }
        }
    }
}
