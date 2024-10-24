using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpDownCamera : MonoBehaviour
{
    public Vector2 upPos;
    public Vector2 downPos;
    private RectTransform parentTransform;
    public float moveDuration = 1f;
    
    void Awake() 
    {
        parentTransform = this.GetComponent<RectTransform>();
    }

    public void DownCam()
    {
        StartCoroutine(MoveCoroutine(downPos));    
    }

    public void BackToOriginalCam()
    {
        StartCoroutine(MoveCoroutine(Vector2.zero));  
    }

    private IEnumerator MoveCoroutine(Vector2 targetPosition)
    {
        Vector2 startPosition = parentTransform.anchoredPosition;
        float elapsedTime = 0f; // Thời gian đã trôi qua

        while (elapsedTime < moveDuration)
        {
            // Tính toán tỷ lệ (0-1) để Lerp
            float t = elapsedTime / moveDuration;
            parentTransform.anchoredPosition = Vector2.Lerp(startPosition, targetPosition, t);

            elapsedTime += Time.deltaTime;
            yield return null; // Chờ đến frame tiếp theo
        }

        // Check đúng vị trí
        parentTransform.anchoredPosition = targetPosition;
    }
}
