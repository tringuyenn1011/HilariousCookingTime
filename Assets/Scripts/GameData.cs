using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameData : MonoBehaviour
{
    public static GameData instance { get; private set; }

    private int money = 0;
    private int point = 0;
    public int lives = 5;

    public float timer = 0f;

    private string playerName;
    private int score;
    private bool isPlayGame = false;

    public Menu Menu { get; private set; }

    public int Money
    {
        get { return money; }
        set { money = value; }
    }

    public int Point
    {
        get { return point; }
        set { point = value; }
    }

    public int Lives
    {
        get { return lives; }
        set { lives = value; }
    }

    public float Timer
    {
        get { return timer; }
        set { timer = value; }
    }

    public string PlayerName
    {
        get { return playerName; }
        set { playerName = value; }
    }

    public int Score
    {
        get { return score; }
        set { score = value; }
    }

    public bool IsPlayGame
    {
        get { return isPlayGame; }
        set { isPlayGame = value; }
    }

    private Currency currencycpn;
    private Point pointcpn;
    private Star starcpn;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            LoadMenu(); // Tải Menu khi khởi tạo GameData
        }
        else
        {
            Destroy(gameObject);
        }

        
    }

    void Update()
    {
        // Tăng thời gian theo thời gian đã trôi qua mỗi frame
        timer += Time.deltaTime;
    }

    private void LoadMenu()
    {
        Menu = Resources.Load<Menu>("Menu"); // Tải Menu từ Resources
        if (Menu == null)
        {
            Debug.LogError("Không tìm thấy Menu trong Resources!");
        }
    }

    // Các phương thức để thao tác với dữ liệu
    public void AddMoney(int amount)
    {
        currencycpn = GameObject.Find("Currency").GetComponent<Currency>();
        currencycpn.AddMoney(amount);
        Debug.Log($"Đã thêm {amount} tiền. Tổng tiền hiện tại: {money}");
    }

    public void AddPoints(int amount)
    {
        pointcpn = GameObject.Find("Point").GetComponent<Point>();
        pointcpn.AddPoint(amount);
        Debug.Log($"Đã thêm {amount} điểm. Tổng điểm hiện tại: {point}");
    }

    public void LoseLife()
    {
        starcpn = GameObject.Find("Star").GetComponent<Star>();
        starcpn.LoseLife();
        Debug.Log($"Mất 1 mạng. Số mạng còn lại: {lives}");
    }
}
