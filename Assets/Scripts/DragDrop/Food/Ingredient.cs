using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ingredient : MonoBehaviour, IDraggable
{
    public GameObject GameObject => this.gameObject; // Cài đặt thuộc tính GameObject
    public void OnBeginDrag()
    {
        GetComponent<CanvasGroup>().blocksRaycasts = false; // Ngăn chặn raycast
    }

    public void OnDrag()
    {
        
    }

    public void OnEndDrag()
    {
        GetComponent<CanvasGroup>().blocksRaycasts = true; // Khôi phục raycast
    }

}
