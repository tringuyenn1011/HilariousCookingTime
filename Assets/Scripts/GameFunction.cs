using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameFunction : MonoBehaviour
{
    public GameObject spawnPoint;
    public GameObject loseBG;
    public bool isLose = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ShowPointPopup(Vector2 position, int score, Transform transform)
    {
        GameObject prefab = Resources.Load<GameObject>("Prefabs/PopUpPoint");
        TextMeshProUGUI scoreText = prefab.GetComponent<TextMeshProUGUI>();
        scoreText.text = "+" + score.ToString();
        // Tạo instance của popup điểm từ prefab và đặt trong Canvas
        GameObject instantiate = Instantiate(prefab, transform);
        
        

        

        // Hủy popup sau một thời gian để tránh tràn bộ nhớ
        Destroy(instantiate, 2f); // Popup sẽ tự động bị hủy sau 1 giây
    }

    public void ClientWhenGameOver()
    {
        isLose = true;
        AudioManager.instance.PlaySound("GameOver");
        foreach (Transform child in spawnPoint.transform)
        {
            Client temp = child.GetComponent<Client>();
            temp.waitingTime = 0;
            temp.isChoosingSeat = false;
            temp.isOrdering = false;
            temp.isChoosingFoods = false;
        
        }

        StartCoroutine(WaitForAnimation(2));
    }

    private IEnumerator WaitForAnimation(float duration)
    {
        yield return new WaitForSeconds(duration);

        loseBG.SetActive(true);

    }

}
