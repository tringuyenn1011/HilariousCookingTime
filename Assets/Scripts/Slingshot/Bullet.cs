using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class Bullet : MonoBehaviour
{
    public float speed = 1.2f;  // Vận tốc di chuyển của viên đạn

    private Vector3 direction;
    private Slingshot slingshot;
    public ItemData itemData;

    private Rigidbody rb;

    private Vector3 velocity;       // Vận tốc của viên đạn
    public float gravity = -9.81f;  // Gia tốc trọng lực (có thể điều chỉnh)

    void Awake() 
    {
        slingshot = GameObject.Find("Slingshot").GetComponent<Slingshot>();
        //this.GetComponent<Bullet>().enabled = true;    
        rb = GetComponent<Rigidbody>();
        
    }
    
    // Start is called before the first frame update
    void Start()
    {
        //itemData = slingshot.itemData;
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
        velocity.y += gravity * Time.deltaTime; // Cộng thêm gia tốc trọng lực
        transform.localPosition += velocity * Time.deltaTime; // Di chuyển viên đạn theo vị trí cục bộ trong Canvas

        // Kiểm tra xem viên đạn có vượt quá giới hạn nào đó không, nếu có thì hủy
        // if (transform.localPosition.z >= 1000f || transform.localPosition.y < 0) // hoặc một giá trị khác tùy thuộc vào thiết kế game
        // {
        //     Destroy(gameObject);
        // }
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
