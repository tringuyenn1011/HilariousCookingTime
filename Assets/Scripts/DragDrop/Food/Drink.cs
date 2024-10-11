using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Drink : MonoBehaviour, IDraggable
{
    public GameObject GameObject => this.gameObject; // Cài đặt thuộc tính GameObject
    private Canvas canvas;

    private void Start()
    {
        // Lấy tham chiếu đến Canvas
        canvas = GetComponentInParent<Canvas>();
    }

    public void OnBeginDrag()
    {
        GetComponent<CanvasGroup>().blocksRaycasts = false; // Ngăn chặn raycast
    }

    public void OnDrag()
    {
        // Vector3 worldPos;
        // // Chuyển đổi vị trí chuột sang thế giới trong không gian canvas
        // RectTransformUtility.ScreenPointToWorldPointInRectangle(
        //     canvas.GetComponent<RectTransform>(),
        //     Input.mousePosition,
        //     canvas.worldCamera, // Sử dụng camera của canvas
        //     out worldPos);
        
        // // Di chuyển đối tượng đến vị trí mới
        // transform.position = worldPos;
    }

    public void OnEndDrag()
    {
        GetComponent<CanvasGroup>().blocksRaycasts = true; // Khôi phục raycast
    }

}
