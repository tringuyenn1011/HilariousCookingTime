using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuFunction : MonoBehaviour
{
    public GameObject leaderBoard;
    public GameObject titleGame;
    public GameObject batTransition;
    public GameObject dragDown;
    public GameObject dragUp;

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
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        AudioManager.instance.PlayMusic("MenuBackgroundMusic");
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
}
