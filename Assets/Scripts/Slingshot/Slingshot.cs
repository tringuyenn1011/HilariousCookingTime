using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class Slingshot : MonoBehaviour
{
    public LineRenderer[] lineRenderers;
    public LineRenderer directionArrow;
    public Transform[] stripPositions;
    public Transform center;
    public Transform idlePosition;

    public Vector3 currentPosition;

    public float maxLength;

    public float bottomBoundary;

    bool isMouseDown;

    public GameObject dishPrefab;

    public float dishPositionOffset;

    Rigidbody2D dish;
    Collider2D dishCollider;

    public float force;
    // Start is called before the first frame update
    void Start()
    {
        lineRenderers[0].positionCount = 2;
        lineRenderers[1].positionCount = 2;
        lineRenderers[0].SetPosition(0, stripPositions[0].position);
        lineRenderers[1].SetPosition(0, stripPositions[1].position);

        directionArrow.positionCount = 2;
        directionArrow.enabled = false;

        CreateDish();
    }

    void CreateDish()
    {
        dish = Instantiate(dishPrefab).GetComponent<Rigidbody2D>();
        dishCollider = dish.GetComponent<Collider2D>();
        dishCollider.enabled = false;

        dish.isKinematic = true;

        ResetStrips();
    }

    // Update is called once per frame
    void Update()
    {
        if (isMouseDown)
        {
            Vector3 mousePosition = Input.mousePosition;
            mousePosition.z = 10;

            currentPosition = Camera.main.ScreenToWorldPoint(mousePosition);
            Debug.Log(currentPosition);
            currentPosition = center.position + Vector3.ClampMagnitude(currentPosition - center.position, maxLength);

            currentPosition = ClampBoundary(currentPosition);

            SetStrips(currentPosition);
            UpdateDirectionArrow();
            if (dishCollider)
            {
                dishCollider.enabled = true;
            }
        }
        else
        {
            ResetStrips();
        }
    }

    private void OnMouseDown()
    {
        isMouseDown = true;
    }

    private void OnMouseUp()
    {
        isMouseDown = false;
        Shoot();
    }

    void Shoot()
    {
        dish.isKinematic = false;
        Vector3 dishForce = (currentPosition - center.position) * force * -1;
        dish.velocity = dishForce;

        dish = null;
        dishCollider = null;
        directionArrow.enabled = false;
        Invoke("CreateDish", 2);
    }

    void ResetStrips()
    {
        currentPosition = idlePosition.position;
        SetStrips(currentPosition);
    }

    void SetStrips(Vector3 position)
    {
        lineRenderers[0].SetPosition(1, position);
        lineRenderers[1].SetPosition(1, position);

        if (dish)
        {
            Vector3 dir = position - center.position;
            dish.transform.position = position + dir.normalized * dishPositionOffset;
        }
    }

    Vector3 ClampBoundary(Vector3 vector)
    {
        vector.y = Mathf.Clamp(vector.y, bottomBoundary, 10);
        return vector;
    }
    void UpdateDirectionArrow()
    {
        directionArrow.enabled = true;
        Vector3 direction = center.position - currentPosition;
        Vector3 arrowEnd = currentPosition + direction.normalized * 2.0f; 
        directionArrow.SetPosition(0, currentPosition);
        directionArrow.SetPosition(1, arrowEnd);
    }
}
