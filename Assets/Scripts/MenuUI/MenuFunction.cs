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
        batTransition.SetActive(true);
        AnimatorStateInfo stateInfo = batTransition.transform.GetChild(0).GetComponent<Animator>().GetCurrentAnimatorStateInfo(0);
        StartCoroutine(WaitForAnimationEnd(stateInfo.length));

        GameData.instance.PlayerName = playerName.text;
            
        
    }

    private IEnumerator WaitForAnimationEnd(float duration)
    {
        yield return new WaitForSeconds(duration);
        
        gameManager.ChangeToMainGameScene();
    }

    public void ExitAndDisable()
    {
        // Kích hoạt Trigger để chuyển sang Exit Animation
        leaderBoard.GetComponent<Animator>().SetTrigger("Exit");

        // Tắt GameObject sau khi Exit Animation kết thúc
        Invoke("DisableGameObject", 1.5f);
    }

    private void DisableGameObject()
    {
        leaderBoard.SetActive(false);
        HideLeaderBoard();
    }

    private void LoadHighScores() 
    {
        bool hasData = false; // Biến để kiểm tra xem có dữ liệu hay không
        for (int i = 0; i < 5; i++) {
            int score = PlayerPrefs.GetInt($"HighScore_{i}_Score", 0);
            string name = PlayerPrefs.GetString($"HighScore_{i}_Name", "Player");

            string nameText = "NoData"; 
            string scoreText = "No Data"; 

            // Kiểm tra xem có dữ liệu không
            if (score > 0) {
                hasData = true; // Có dữ liệu nếu score > 0
                // Hiển thị vào UI Text
                if (i < highScoreTexts.Length) 
                {
                    nameText = $"{name}";
                    scoreText = $"{score}";
                }
            } 
            // else {
            //     // Nếu không có dữ liệu, hiển thị "No data"
            //     if (i < highScoreTexts.Length) 
            //     {
            //         nameText = $"No Data";
            //         scoreText = $"No Data";
            //     }
            // }

            highScoreTexts[i].transform.GetChild(0).GetComponent<TMP_Text>().text = nameText;
            highScoreTexts[i].transform.GetChild(1).GetComponent<TMP_Text>().text = scoreText; 
        }

        // Nếu không có dữ liệu nào, có thể in ra "No data" cho tất cả
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
