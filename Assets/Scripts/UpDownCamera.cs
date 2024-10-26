using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpDownCamera : MonoBehaviour
{
    public GameObject backGround;
    public Vector3 upBgPos;
    public Vector3 downBgPos;
    public Vector3 originalBgPos;
    public Vector2 upPos;
    public Vector2 downPos;
    private RectTransform parentTransform;
    private Vector3 currentBgPos;
    public float moveDuration = 1f;


    
    void Awake() 
    {
        parentTransform = this.GetComponent<RectTransform>();
    }

    public void DownCam()
    {
        
        
        StartCoroutine(MoveCoroutine(downPos, downBgPos));    
    }

    public void BackToOriginalCam()
    {
        StartCoroutine(MoveCoroutine(Vector2.zero, originalBgPos));  
    }

    public void UpCam()
    {

    }

    private IEnumerator MoveCoroutine(Vector2 targetPosition, Vector3 targetBgPos)
    {
        currentBgPos = backGround.transform.position;
        Vector2 startPosition = parentTransform.anchoredPosition;
        float elapsedTime = 0f; // Thời gian đã trôi qua

        while (elapsedTime < moveDuration)
        {
            // Tính toán tỷ lệ (0-1) để Lerp
            float t = elapsedTime / moveDuration;
            parentTransform.anchoredPosition = Vector2.Lerp(startPosition, targetPosition, t);
            backGround.transform.position = Vector3.Lerp(currentBgPos, targetBgPos, t);

            elapsedTime += Time.deltaTime;
            yield return null; // Chờ đến frame tiếp theo
        }

        // Check đúng vị trí
        parentTransform.anchoredPosition = targetPosition;
    }
}
