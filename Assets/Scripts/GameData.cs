using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameData : MonoBehaviour
{
    public static GameData instance { get; private set; }

    private int money = 0;
    private int point = 0;
    private int lives = 5;

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
        money += amount;
        Debug.Log($"Đã thêm {amount} tiền. Tổng tiền hiện tại: {money}");
    }

    public void AddPoints(int amount)
    {
        point += amount;
        Debug.Log($"Đã thêm {amount} điểm. Tổng điểm hiện tại: {point}");
    }

    public void LoseLife()
    {
        lives--;
        Debug.Log($"Mất 1 mạng. Số mạng còn lại: {lives}");
    }
}
