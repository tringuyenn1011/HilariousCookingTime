using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI; 

public class Slingshot : MonoBehaviour
{
    public LineRenderer[] lineRenderers;
    public Transform[] stripPositions;
    public Transform center;
    public Transform idlePosition;

    public Vector3 currentPosition;

    public float maxLength;

    public float bottomBoundary;
    public float topBoundary;

    bool isMouseDown;

    private GameObject foodPrefab;
    Collider2D foodCollider;

    public float foodPositionOffset;

    public ItemData itemData;
    
    // Collider2D dishCollider;
    public float force;

    private GameObject food;

    public Vector3 direction;

    // GameObject clone;
    // public RectTransform canvasRectTransform;

    public GameObject slingSlot;
    public float second = 1;

    private bool isDragging = false;

    private Vector3 startMousePosition;
    private Vector3 startObjectPosition;

    void Start()
    {
        lineRenderers[0].positionCount = 2;
        lineRenderers[1].positionCount = 2;
        lineRenderers[0].SetPosition(0, stripPositions[0].position);
        lineRenderers[1].SetPosition(0, stripPositions[1].position);

        ResetStrips();

    }


    void SetItemData()
    {

    }

    public void SetFood()
    {
        food = this.transform.Find("object").gameObject;
        SpriteRenderer foodRd = food.GetComponent<SpriteRenderer>();
        food.name = itemData.name;
        foodRd.sprite = itemData.icon;


        Color color = foodRd.color;
        color.a = 255;
        foodRd.color = color;

        // foodCollider = foodPrefab.GetComponent<Collider2D>();
        // foodCollider.enabled = false;

        ResetStrips();
    }

    void Update()
    {
       
    }

    public void OnMouseDown() 
    {
        isMouseDown = true;
        isDragging = true;
        if(itemData != null && itemData.slotType == SlotType.Food)
        {
            AudioManager.instance.PlaySound("SlingshotDrag");
            startMousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            startObjectPosition = transform.position;
            if (isMouseDown)
            {
                
                Vector3 mousePosition = Input.mousePosition;
                mousePosition.z = 100;
                currentPosition = Camera.main.ScreenToWorldPoint(mousePosition);

                currentPosition = center.position + Vector3.ClampMagnitude(currentPosition - center.position, maxLength);
                currentPosition = ClampBoundary(currentPosition);
                
                currentPosition = new Vector3(transform.position.x, -3.5f, 110);
                SetStrips(currentPosition);
                // if (dishCollider)
                // {
                //     dishCollider.enabled = true;
                // }
            }
            
                
        }
    }

    void OnMouseDrag()
    {
        if (isDragging)
        {
            Vector3 currentMousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            float deltaX = currentMousePosition.x - startMousePosition.x;

            transform.position = new Vector3(startObjectPosition.x + deltaX, startObjectPosition.y, startObjectPosition.z);
            currentPosition = new Vector3(transform.position.x, -3.5f, 110);
            SetStrips(currentPosition);
        }
    }

    private void OnMouseUp() 
    {    
        isMouseDown = false;
        isDragging = false;
        if(itemData != null)
        {
            AudioManager.instance.PlaySound("SlingshotDrop");
            direction = center.transform.position - currentPosition;
            direction.z = 1000f;
            direction.Normalize();
            

            
            GameObject prefab = Resources.Load<GameObject>("Prefabs/EmptyProjectile");
            prefab.GetComponent<Bullet>().isSlingshot = true;
            foodPrefab = Instantiate(prefab, currentPosition, Quaternion.identity,GameObject.Find("Canvas").transform.GetChild(0));

            foodPrefab.name = itemData.name;
            foodPrefab.GetComponent<Image>().sprite = itemData.icon;
            foodPrefab.GetComponent<Image>().SetNativeSize();
            foodPrefab.GetComponent<Bullet>().itemData = itemData;

            foodPrefab.GetComponent<Bullet>().SetDirection(direction, CalculateInitialVelocity(direction));
            //GameObject clone = Instantiate(foodPrefab, this.transform);
            //Destroy(foodPrefab);
            //Shoot();
            StartCoroutine(Wait(second));
            SetObjectToOriginal();
        }  
    }

    private Vector3 CalculateInitialVelocity(Vector3 direction)
    {
        float speed = 700f; 
        float arcHeight = 1700f; 
        
        Vector3 initialVelocity = direction * speed;
        initialVelocity.y += arcHeight; 
        Debug.Log(initialVelocity);
        return initialVelocity;
    }

    private IEnumerator Wait(float second)
    {
        yield return new WaitForSeconds(second);
        GameObject.Find("DragDrop").GetComponent<UpDownCamera>().BackToOriginalCam();
        this.gameObject.SetActive(false);
    }

    
    

    void ResetStrips()
    {
        currentPosition = idlePosition.position;
        SetStrips(currentPosition);
        //SetStrips(idlePosition.position);
    }

    void SetStrips(Vector3 position)
    {
        lineRenderers[0].SetPosition(0, new Vector3(position.x - 1, stripPositions[0].position.y, 110));
        lineRenderers[1].SetPosition(0, new Vector3(position.x + 1, stripPositions[1].position.y, 110));

        lineRenderers[0].SetPosition(1, position);
        lineRenderers[1].SetPosition(1, position);

        if (itemData != null && itemData.slotType == SlotType.Food)
        {
            Vector3 dir = position - center.position;
            food.transform.position = position + dir.normalized * foodPositionOffset;
            //foodPrefab.transform.right = -dir.normalized;
        }
    }

    void SetObjectToOriginal()
    {
        itemData = null;
        SpriteRenderer foodRd = food.GetComponent<SpriteRenderer>();
        food.name = "object";
        foodRd.sprite = null;


        Color color = foodRd.color;
        color.a = 0;
        foodRd.color = color;

        slingSlot.SetActive(true);
        slingSlot.GetComponent<SlingSlot>().firstDrag.SetActive(true);
    }

    Vector3 ClampBoundary(Vector3 vector)
    {
        vector.y = Mathf.Clamp(vector.y, bottomBoundary, topBoundary);
        return vector;
    }

}
