using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.UI;

public class Bullet : MonoBehaviour
{
    public float speed = 1.2f;  

    private Vector3 direction;
    private Slingshot slingshot;
    public ItemData itemData;

    private Rigidbody rb;

    private Vector3 velocity;       
    public float gravity = -9.81f;  

    private bool isMovingLeft = false;
    private float targetX = 0f;
    private GameManager gameManager;
    private GameFunction gameFunction;
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
        gameFunction = GameObject.Find("GameFunction").GetComponent<GameFunction>();
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
        
        if(isSlingshot)
        {
            velocity.y += gravity * Time.deltaTime; 
            transform.localPosition += velocity * Time.deltaTime; 
            
            if(transform.localPosition.y > 600 && !isMovingLeft && gameFunction.isWind)
            {
                isMovingLeft = true;
            }

            if(isMovingLeft)
            {
                float newX = Mathf.Lerp(transform.localPosition.x, targetX, Time.deltaTime / 1f);
                transform.localPosition = new Vector3(newX, transform.localPosition.y, transform.localPosition.z);
                if (Mathf.Abs(transform.localPosition.x - targetX) < 0.01f)
                {
                    isMovingLeft = false;
                }
            }
        }else if(gameManager.isRandom && !isSlingshot)
        {
            Debug.LogWarning("Client bắn ra");
            //isSlingshot = true;
            lerpTime += 1f * Time.deltaTime ;
            lerpTime = Mathf.Clamp01(lerpTime);
            
            rectTransform.anchoredPosition3D = Vector3.Lerp(startPosition, targetPosition, lerpTime);
            rectTransform.localScale = Vector3.Lerp(initialScale, targetScale, lerpTime);
            rectTransform.Rotate(0, 0, 360 * 2 * Time.deltaTime);
            if(rectTransform.localScale.x >= targetScale.x)
            {
                GameObject prefab = Instantiate(Resources.Load<GameObject>("Prefabs/Dirt"), GameObject.Find("Canvas").transform.GetChild(0));
                prefab.GetComponent<Image>().sprite = FindObjectOfType<GameFunction>().ChooseRandomDirt();
                AudioManager.instance.PlaySound("Dirt");
                StartCoroutine(WaitToDestroy(prefab));
                
            }
        }
        

        if (transform.localPosition.y < -5000)
        {
            Destroy(gameObject);
        }
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
        direction = dir; 
        velocity = initialVelocity; 
        
    }

    private void OnTriggerEnter(Collider other) {
        Destroy(gameObject);
    }

}
