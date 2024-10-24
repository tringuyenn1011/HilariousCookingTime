using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI; 

public class Slingshot : MonoBehaviour
{
    public LineRenderer[] lineRenderers;
    public Transform[] stripPositions;
    public Transform center;
    public Transform idlePosition;

    private Vector3 currentPosition;

    public float maxLength;

    public float bottomBoundary;
    public float topBoundary;

    bool isMouseDown;

    // public float dishPositionOffset;

    // public ItemData itemData;
    // Rigidbody2D dish;
    // Collider2D dishCollider;

    // public float force;
    // GameObject clone;
    // public RectTransform canvasRectTransform;

    void Start()
    {
        lineRenderers[0].positionCount = 2;
        lineRenderers[1].positionCount = 2;
        lineRenderers[0].SetPosition(0, stripPositions[0].position);
        lineRenderers[1].SetPosition(0, stripPositions[1].position);

        //CreateDish();
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
    // void CreateDish()
    // {
    //     clone = Instantiate(Resources.Load<GameObject>("Prefabs/EmptyProjectile"), this.transform);
    //     clone.name = itemData.name;
    //     clone.GetComponent<Image>().sprite = itemData.icon;

    //     dish = clone.GetComponent<Rigidbody2D>();
    //     dishCollider = clone.GetComponent<Collider2D>();
    //     dishCollider.enabled = false;

    //     dish.isKinematic = true;

    //     ResetStrips();
    // }

    void Update()
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
        else
        {
            ResetStrips();
        }
    }

    public void OnMouseDown() 
    {
        isMouseDown = true;
    }

    private void OnMouseUp() {
        isMouseDown = false;
        //Shoot();
    }

    /// <summary>
    /// Cho projectile 1 lực force với hướng ngược lại của vector từ center đến con trỏ
    /// </summary>
    // void Shoot()
    // {
    //     dish.isKinematic = false;
    //     Vector3 dishForce = (currentPosition - center.anchoredPosition3D) * force * -1;
    //     dish.velocity = dishForce;

    //     dish = null;
    //     dishCollider = null;
    //     //Loại bỏ dòng này nếu như đã làm chức năng setItemData
    //     Invoke("CreateDish", 2);
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

        // if (dish)
        // {
        //     Vector3 dir = position - center.anchoredPosition3D;
        //     clone.GetComponent<RectTransform>().anchoredPosition = position + dir.normalized * dishPositionOffset;
        // }
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
