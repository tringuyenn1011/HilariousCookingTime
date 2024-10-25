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

    void Start()
    {
        lineRenderers[0].positionCount = 2;
        lineRenderers[1].positionCount = 2;
        lineRenderers[0].SetPosition(0, stripPositions[0].position);
        lineRenderers[1].SetPosition(0, stripPositions[1].position);

        food = this.transform.Find("object").gameObject;

        SetFood();
    }

    /// <summary>
    /// Chuyển giá trị itemData từ dĩa lên ná
    /// </summary>
    void SetItemData()
    {

    }

    /// <summary>
    /// Tạo một đối tượng gameObject lấy data từ item data
    /// </summary>
    void SetFood()
    {
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
        if(itemData != null && itemData.slotType == SlotType.Food)
        {
            if (isMouseDown)
            {
                // Lấy position của con trỏ và chuyển nó thành position trên canvas
                Vector3 mousePosition = Input.mousePosition;
                mousePosition.z = 10;
                currentPosition = Camera.main.ScreenToWorldPoint(mousePosition);

                // Vector2 canvasPosition;
                // RectTransformUtility.ScreenPointToLocalPointInRectangle(canvasRectTransform, mousePosition, null, out canvasPosition);
                // currentPosition = canvasPosition;

                // // Tính giá trị vector từ vị trí center đến vị trí con trỏ trên canvas
                currentPosition = center.position + Vector3.ClampMagnitude(currentPosition - center.position, maxLength);
                currentPosition = ClampBoundary(currentPosition);
                SetStrips(currentPosition);
                // if (dishCollider)
                // {
                //     dishCollider.enabled = true;
                // }
            }
            
                
        }else
        {
            ResetStrips();
        }
    }

    public void OnMouseDown() 
    {
        isMouseDown = true;
    }

    private void OnMouseUp() {
        if(itemData != null)
        {
            direction = center.transform.position - currentPosition;
            foodPrefab = Instantiate(Resources.Load<GameObject>("Prefabs/EmptyProjectile"), currentPosition, Quaternion.identity,this.transform);
            foodPrefab.name = itemData.name;
            foodPrefab.GetComponent<SpriteRenderer>().sprite = itemData.icon;
            
            
            //GameObject clone = Instantiate(foodPrefab, this.transform);
            //Destroy(foodPrefab);
            //Shoot();
            isMouseDown = false;
            SetObjectToOriginal();
        }
        
    }

    /// <summary>
    /// Cho projectile 1 lực force với hướng ngược lại của vector từ center đến con trỏ
    /// </summary>
    // void Shoot()
    // {
    //     GameObject clone = Instantiate(foodPrefab, this.transform);
    //     //Destroy(foodPrefab);
        
    //     Vector3 foodForce = currentPosition - center.position;
    //     Debug.Log("oke");
    //     foodForce.z = 0;
    //     Rigidbody2D rb = clone.GetComponent<Rigidbody2D>();
    //     rb.AddForce(foodForce.normalized * force * -1, ForceMode2D.Impulse);

    //     // dish.isKinematic = false;
        
    //     // dish = null;
    //     //foodCollider = null;
    //     // //Loại bỏ dòng này nếu như đã làm chức năng setItemData
    //     // Invoke("CreateDish", 2);
    //     foodPrefab.GetComponent<Bullet>().Shoot(foodForce);
    // }

    void ResetStrips()
    {
        currentPosition = idlePosition.position;
        SetStrips(currentPosition);
        //SetStrips(idlePosition.position);
    }

    void SetStrips(Vector3 position)
    {
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
    }

    /// <summary>
    /// Tăng độ căng của ná, giá trị càng cao ná càng yếu
    /// </summary>
    /// <param name="vector"></param>
    /// <returns></returns>
    Vector3 ClampBoundary(Vector3 vector)
    {
        vector.y = Mathf.Clamp(vector.y, bottomBoundary, topBoundary);  // Giới hạn bình thường
        return vector;
    }
}
