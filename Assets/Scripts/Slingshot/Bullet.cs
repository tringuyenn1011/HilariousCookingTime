using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.UI;

public class Bullet : MonoBehaviour
{
    public float speed = 1.2f;  // Vận tốc di chuyển của viên đạn

    private Vector3 direction;
    private Slingshot slingshot;
    public ItemData itemData;

    private Rigidbody rb;

    private Vector3 velocity;       // Vận tốc của viên đạn
    public float gravity = -9.81f;  // Gia tốc trọng lực (có thể điều chỉnh)

    private bool isMovingLeft = false;
    private float targetX = 0f;
    private GameManager gameManager;
    public bool isSlingshot = false;
    private RectTransform rectTransform;
    private Vector2 initialScale;
    private Vector3 startPosition;
    private Vector3 targetPosition;
    private Vector3 targetScale;
    private float lerpTime;
    
    public 
    void Awake() 
    {
        rb = GetComponent<Rigidbody>();
        if(isSlingshot)
        {
            slingshot = GameObject.Find("Slingshot").GetComponent<Slingshot>();
        }else
        {
            rb.useGravity = false;
        }
        
        //this.GetComponent<Bullet>().enabled = true;    
        
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        rectTransform  = GetComponent<RectTransform>();
    }
    
    // Start is called before the first frame update
    void Start()
    {
        startPosition = transform.localPosition;
        targetPosition = Vector3.zero;
        targetScale = transform.localScale *3f;
        //itemData = slingshot.itemData;
        targetX = transform.localPosition.x - 500;
        initialScale = rectTransform.localScale;

        lerpTime = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        // // transform.Translate(/*slingshot.direction*/Vector3.forward * slingshot.force * Time.deltaTime);
        // // Tính toán hướng bắn dựa trên góc bắn
        // Vector3 shootDirection = Quaternion.Euler(-45, 0, 0) * slingshot.gameObject.transform.forward;

        // // Thêm lực bắn cho viên đạn theo hướng chếch lên
        // rb.AddForce(shootDirection * slingshot.force, ForceMode.Impulse);

        // // Xóa viên đạn sau một khoảng thời gian để tránh tràn bộ nhớ

        // //DestroyObject();
        // Cập nhật vị trí viên đạn
        if(isSlingshot)
        {
            velocity.y += gravity * Time.deltaTime; // Cộng thêm gia tốc trọng lực
            transform.localPosition += velocity * Time.deltaTime; // Di chuyển viên đạn theo vị trí cục bộ trong Canvas
            
            if(transform.localPosition.y > 600 && !isMovingLeft && gameManager.isWind)
            {
                isMovingLeft = true;
            }

            if(isMovingLeft)
            {
                float newX = Mathf.Lerp(transform.localPosition.x, targetX, Time.deltaTime / 1f);
                transform.localPosition = new Vector3(newX, transform.localPosition.y, transform.localPosition.z);
                if (Mathf.Abs(transform.localPosition.x - targetX) < 0.01f)
                {
                    isMovingLeft = false; // Dừng lại sau khi đạt mục tiêu
                }
            }
        }else if(gameManager.isRandom && !isSlingshot)
        {
            Debug.LogWarning("Client bắn ra");
            //isSlingshot = true;
            // Tăng giá trị Lerp theo thời gian dựa trên speed
            lerpTime += 1f * Time.deltaTime ;
            lerpTime = Mathf.Clamp01(lerpTime); // Giữ giá trị trong khoảng từ 0 đến 1
            
            rectTransform.anchoredPosition3D = Vector3.Lerp(startPosition, targetPosition, lerpTime);
            rectTransform.localScale = Vector3.Lerp(initialScale, targetScale, lerpTime);
            rectTransform.Rotate(0, 0, 360 * 2 * Time.deltaTime);
            if(rectTransform.localScale.x >= targetScale.x)
            {
                //Sinh vet ban ra
                GameObject prefab = Instantiate(Resources.Load<GameObject>("Prefabs/Dirt"), GameObject.Find("Canvas").transform.GetChild(0));
                prefab.GetComponent<Image>().sprite = FindObjectOfType<GameFunction>().ChooseRandomDirt();
                AudioManager.instance.PlaySound("Dirt");
                StartCoroutine(WaitToDestroy(prefab));
                
            }
        }
        

        // Kiểm tra xem viên đạn có vượt quá giới hạn nào đó không, nếu có thì hủy
        // if (transform.localPosition.z >= 1000f || transform.localPosition.y < 0) // hoặc một giá trị khác tùy thuộc vào thiết kế game
        // {
        //     Destroy(gameObject);
        // }
    }

    private IEnumerator WaitToDestroy(GameObject gameObject)
    {
        isSlingshot = true;
        yield return new WaitForSeconds(1f);
        
        Destroy(gameObject);
        Destroy(this.gameObject);
        
    }
    public void SetDirection(Vector3 dir, Vector3 initialVelocity)
    {
        direction = dir; // Lưu hướng bắn
        velocity = initialVelocity; // Lưu vận tốc ban đầu
        
    }
    void DestroyObject()
    {
        // if(this.transform.position.y > 12 || this.transform.position.x < -5 || this.transform.position.x > 5)
        // {
        //     Destroy(this.gameObject);
        // }

        Destroy(this.gameObject, 5);
    }

    private void OnTriggerEnter(Collider other) {
        Destroy(gameObject);
    }
    // public void Shoot(Vector3 foodForce)
    // {
    //     // Vector3 dishForce = (currentPosition - center.anchoredPosition3D) * force * -1;
    //     rb.velocity = foodForce;

    //     //rb = null;
    //     // dishCollider = null;
    //     // //Loại bỏ dòng này nếu như đã làm chức năng setItemData
    //     // Invoke("CreateDish", 2);
    // }

    void Shoot()
    {
        
        // Vector3 foodForce = currentPosition - center.position;
        // foodForce.z = 0;
        // Rigidbody2D rb = this.GetComponent<Rigidbody2D>();
        // rb.AddForce(foodForce.normalized * force * -1, ForceMode2D.Impulse);

        // // dish.isKinematic = false;
        
        // // dish = null;
        // //foodCollider = null;
        // // //Loại bỏ dòng này nếu như đã làm chức năng setItemData
        // // Invoke("CreateDish", 2);
        // foodPrefab.GetComponent<Bullet>().Shoot(foodForce);
    }
}
