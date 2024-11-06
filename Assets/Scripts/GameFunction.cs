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

    public ParticleSystem wind;

    public List<Sprite> dirts;

    public bool isWind = false;
    private float lastThreshold = 0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(GameData.instance.Timer >= lastThreshold + 100f && !isWind)
        {
            Debug.LogWarning("Windddd!!!!!");
            isWind = true;
            lastThreshold += 100f;
            wind.Play();
        }

        if(isWind && GameData.instance.Timer >= lastThreshold + 30f)
        {
            Debug.LogWarning("EndWindddd!!!!!");
            isWind = false;
            wind.Stop();
        }
    }

    public Sprite ChooseRandomDirt()
    {
        Sprite dirt;
        dirt = dirts[Random.Range(0, dirts.Count)];
        return dirt;
    }

    public void ShowPointPopup(Vector2 position, int score, Transform transform)
    {
        GameObject prefab = Resources.Load<GameObject>("Prefabs/PopUpPoint");
        TextMeshProUGUI scoreText = prefab.GetComponent<TextMeshProUGUI>();
        scoreText.text = score.ToString();
        // Tạo instance của popup điểm từ prefab và đặt trong Canvas
        GameObject instantiate = Instantiate(prefab, transform);
        // Hủy popup sau một thời gian để tránh tràn bộ nhớ
        Destroy(instantiate, 2f); // Popup sẽ tự động bị hủy sau 1 giây
    }

    public void ShowMoneyPopup(Vector2 position, int score, Transform transform)
    {
        GameObject prefab = Resources.Load<GameObject>("Prefabs/PopUpMoney");
        TextMeshProUGUI scoreText = prefab.GetComponent<TextMeshProUGUI>();
        scoreText.text = "$" + score.ToString();
        // Tạo instance của popup điểm từ prefab và đặt trong Canvas
        GameObject instantiate = Instantiate(prefab, transform);
        // Hủy popup sau một thời gian để tránh tràn bộ nhớ
        Destroy(instantiate, 2f); // Popup sẽ tự động bị hủy sau 1 giây
    }

    public void ClientWhenGameOver()
    {
        isLose = true;
        
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



    
    public void SaveScore(string playerName, int score) {
        for (int i = 0; i < 5; i++) {
            int savedScore = PlayerPrefs.GetInt($"HighScore_{i}_Score", 0);
            if (score > savedScore) {
                for (int j = 4; j > i; j--) {
                    PlayerPrefs.SetInt($"HighScore_{j}_Score", PlayerPrefs.GetInt($"HighScore_{j - 1}_Score", 0));
                    PlayerPrefs.SetString($"HighScore_{j}_Name", PlayerPrefs.GetString($"HighScore_{j - 1}_Name", "Player"));
                }
                PlayerPrefs.SetInt($"HighScore_{i}_Score", score);
                PlayerPrefs.SetString($"HighScore_{i}_Name", playerName);
                break;
            }
        }

        PlayerPrefs.Save();

        GameData.instance.Point = 0;
    }

    // Hàm OnGameEnd có tham số (không trực tiếp dùng cho Button)
    public void OnGameEnd(string playerName, int score) {
        SaveScore(playerName, score);
        UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu"); // Thay "MainMenu" bằng tên scene của bạn
    }

    // Hàm không có tham số để gọi từ Button
    public void OnGameEndButton() {
        OnGameEnd(GameData.instance.PlayerName, GameData.instance.Point); // Gọi hàm OnGameEnd với dữ liệu đã lưu
    }


}
