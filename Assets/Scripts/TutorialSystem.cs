using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TutorialSystem : MonoBehaviour
{
    public GameObject tutorialDetail;

    private List<GameObject> guides = new List<GameObject>();

    public Button leftButton; 
    public Button rightButton; 
    public GameObject closeButton;
    public Button openButton;
    private RectTransform rectTransform;

    private int currentIndex = 0;

    private bool isDone = false;
    private Vector3 startPosition;
    private Vector3 initialScale;
    private Vector3 targetScale;
    private float lerpTime;

    public GameObject recipe;
    public Button recipeButton;

    void Awake() 
    {
        rectTransform = tutorialDetail.transform.parent.GetComponent<RectTransform>();
    }

    void Start()
    {   
        recipe.SetActive(false);
        tutorialDetail.transform.parent.gameObject.SetActive(true);
        startPosition = rectTransform.anchoredPosition3D;
        targetScale = rectTransform.localScale *0.1f;
        initialScale = rectTransform.localScale;

        foreach(Transform child in tutorialDetail.transform)
        {
            guides.Add(child.gameObject);
            child.gameObject.SetActive(false);
        }
        guides[currentIndex].SetActive(true);
        leftButton.gameObject.SetActive(false);
        rightButton.gameObject.SetActive(true);
        Time.timeScale = 0;
        lerpTime = 0f;
    }

    void Update()
    {
        
        if(isDone)
        {
            lerpTime += 1f * Time.unscaledDeltaTime; // Dùng unscaledDeltaTime để tính toán thời gian không bị ảnh hưởng bởi Time.timeScale
            lerpTime = Mathf.Clamp01(lerpTime);
            rectTransform.anchoredPosition3D = Vector3.Lerp(startPosition, closeButton.GetComponent<RectTransform>().anchoredPosition3D, lerpTime);
            rectTransform.localScale = Vector3.Lerp(initialScale, targetScale, lerpTime);
            if(lerpTime == 1)
            {
                rectTransform.localScale = initialScale;
                tutorialDetail.transform.parent.gameObject.SetActive(false); 

            }
        }
    }

    public void NextDetail()
    {
        currentIndex++;
        guides[currentIndex].SetActive(true);
        guides[currentIndex-1].SetActive(false);
        leftButton.gameObject.SetActive(true);

        if(currentIndex == guides.Count-1)
        {
            leftButton.gameObject.SetActive(true);
            rightButton.gameObject.SetActive(false);
        }
    }

    public void ForwardDetail()
    {
        currentIndex--;
        guides[currentIndex].SetActive(true);
        guides[currentIndex+1].SetActive(false);
        rightButton.gameObject.SetActive(true);
        if(currentIndex == 0)
        {
            leftButton.gameObject.SetActive(false);
            rightButton.gameObject.SetActive(true);
        }
    }

    public void CloseTutorial()
    {
        openButton.interactable = true;
        isDone = true;
        Time.timeScale = 1f;
    }

    public void OpenTutorial()
    {
        openButton.interactable = false;
        rectTransform.anchoredPosition = Vector2.one;
        isDone = false;
        lerpTime = 0f;
        Time.timeScale = 0;
        tutorialDetail.transform.parent.gameObject.SetActive(true); 
    }

    public void OpenRecipe()
    {
        Time.timeScale = 0;
        recipeButton.interactable = false;
        recipe.SetActive(true);
    }
    public void CloseRecipe()
    {
        Time.timeScale = 1;
        recipeButton.interactable = true;
        recipe.SetActive(false);
    }

}
