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

    void Awake() 
    {
        slingshot = GameObject.Find("Slingshot").GetComponent<Slingshot>();
        //this.GetComponent<Bullet>().enabled = true;    
        
    }
    
    // Start is called before the first frame update
    void Start()
    {
        //itemData = slingshot.itemData;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(slingshot.direction * slingshot.force * Time.deltaTime);

        DestroyObject();
    }

    void DestroyObject()
    {
        if(this.transform.position.y > 12 || this.transform.position.x < -5 || this.transform.position.x > 5)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D other) {
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
