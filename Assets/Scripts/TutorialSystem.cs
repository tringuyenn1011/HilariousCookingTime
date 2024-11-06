using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TutorialSystem : MonoBehaviour
{
    public List<GameObject> tutorialSteps; // Danh sách các đối tượng cần highlight theo thứ tự
    public GameObject arrowIcon;           // Biểu tượng chỉ dẫn (mũi tên)

    private int currentStep = 0;           // Bước hướng dẫn hiện tại

    private void Start()
    {
        arrowIcon.SetActive(false);       // Ẩn biểu tượng khi bắt đầu
        ShowStep(currentStep);            // Hiển thị bước đầu tiên
    }

    // Hàm quản lý các bước hướng dẫn
    private void ShowStep(int step)
    {
        ClearHighlights();  // Tắt mọi biểu tượng chỉ dẫn trước khi chuyển sang bước mới

        if (step < tutorialSteps.Count)
        {
            ShowArrow(tutorialSteps[step]); // Hiển thị mũi tên cho đối tượng trong bước hiện tại
        }
        else
        {
            EndTutorial();  // Kết thúc hướng dẫn nếu hết bước
        }
    }

    // Phương thức chuyển sang bước tiếp theo khi hoàn thành hành động
    public void NextStep()
    {
        currentStep++;
        ShowStep(currentStep);
    }

    // Phương thức hiển thị biểu tượng mũi tên ở bên cạnh đối tượng cần highlight
    private void ShowArrow(GameObject target)
    {
        arrowIcon.SetActive(true);
        // Đặt biểu tượng mũi tên ở ngay trên đối tượng cần làm nổi bật
        arrowIcon.transform.position = target.transform.position + new Vector3(0, 1, 0); // Điều chỉnh vị trí nếu cần
    }

    // Phương thức ẩn biểu tượng chỉ dẫn
    private void HideArrow()
    {
        arrowIcon.SetActive(false);
    }

    // Phương thức xóa tất cả các highlight
    private void ClearHighlights()
    {
        HideArrow();
    }

    // Kết thúc hướng dẫn
    private void EndTutorial()
    {
        ClearHighlights();
        Debug.Log("Tutorial Completed");
    }
}
