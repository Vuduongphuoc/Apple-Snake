using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class WormController : MonoBehaviour
{
    public static WormController instance;

    public GameObject DownRay;
    public GameObject UpRay;
    public GameObject LeftRay;
    public GameObject RightRay;

    public Button leftBtn;
    public Button rightBtn;
    public Button upBtn;
    public Button downBtn;

    public GameObject BodyPrefabs;
    public static List<GameObject> BodyParts = new List<GameObject>();
    public static List<GameObject> DropDownParts = new List<GameObject>();
    public static GameObject lastPart;
    public static List<Vector3> PositionHistory = new List<Vector3>();
    public static Vector3 LastPos;
    public static Vector3 OriPos;

    int distance;
    public static bool isMoving;
    public static bool isHeadOnGround;
    public static bool isBodyOnGround;
    public static bool isTailOnGround;
    public static bool isGround;
    public static bool isDropping;
    public static bool isReseting;
    public bool isLeft;
    public bool isRight;
    public bool isUp;
    public bool isDown;
    // Start is called before the first frame update
    private void Awake()
    {
        if(instance != null)
        {
            instance = this;
        }
    }
    void Start()
    {
        isMoving = false;
        isHeadOnGround = false;
        isTailOnGround = false;
        isReseting = false;
        distance = 1;
        DropDownParts.Add(gameObject);

    }

    // Update is called once per frame
    void Update()
    {
        DrawRayCast();
    }
    
    void DrawRayCast()
    {
        RaycastHit2D hitdown = Physics2D.Raycast(DownRay.transform.position, Vector2.down);
        Debug.DrawRay(DownRay.transform.position, Vector2.down * hitdown.distance, Color.red);
        if(hitdown.collider != null)
        {
            if(hitdown.distance <= 0.01f)
            {
                if (hitdown.collider.tag == "Apple" || hitdown.collider.tag == "Victory")
                {
                    downBtn.enabled = true;
                }
                else if( hitdown.collider.tag == "Wall")
                {
                    isHeadOnGround = true;
                    downBtn.enabled = false;
                }
            }
            else
            {
                downBtn.enabled =true;
                isHeadOnGround = false;
            }
        }
        else
        {
            downBtn.enabled = true;
            isHeadOnGround = false;
        }
        
        RaycastHit2D hitup = Physics2D.Raycast(UpRay.transform.position, Vector2.up);
        Debug.DrawRay(UpRay.transform.position, Vector2.up * hitup.distance, Color.red);
        if (hitup.collider != null)
        {
            if (hitup.distance < 0.5)
            {

                if (hitup.collider.tag == "Apple" || hitup.collider.tag == "Victory")
                {
                    upBtn.enabled = true;
                }
                else
                {
                    upBtn.enabled = false;
                }
            }
            else
            {
                upBtn.enabled = true;
            }
        }
        else
        {
            upBtn.enabled = true;
        }


        RaycastHit2D hitright = Physics2D.Raycast(RightRay.transform.position, Vector2.right);
        Debug.DrawRay(RightRay.transform.position, Vector2.right * hitright.distance, Color.red);
        if (hitright.collider != null)
        {
            if (hitright.distance < 0.5)
            {
               
                if (hitright.collider.tag == "Apple" || hitright.collider.tag == "Victory")
                {
                    rightBtn.enabled = true;
                }
                else
                {
                    rightBtn.enabled = false;
                }
            }
            else
            {
                
                rightBtn.enabled = true;
            }
        }
        else
        {
            rightBtn.enabled = true;
        }

        RaycastHit2D hitleft = Physics2D.Raycast(LeftRay.transform.position, Vector2.left);
        Debug.DrawRay(LeftRay.transform.position, Vector2.left* hitleft.distance, Color.red);
        if(hitleft.collider != null)
        {
            if(hitleft.distance < 0.5)
            {
                if (hitleft.collider.tag == "Apple" || hitleft.collider.tag =="Victor")
                {
                    leftBtn.enabled = true;
                }
                else
                {
                    leftBtn.enabled = false;
                }
            }
            else
            {
                leftBtn.enabled = true;
            }
        }
        else
        {
            leftBtn.enabled = true;
        }
        WormHeadMove();
    }
    void Growth()
    {
        GameObject firstbody = Instantiate(BodyPrefabs,DropDownParts.Last().transform.position, Quaternion.identity);
        BodyParts.Add(firstbody);
        DropDownParts.Add(firstbody);
  
    }
    void BodyMoving()
    {
        int index = 0;
        if (isMoving)
        {
            PositionHistory.Insert(0, transform.position);
            Debug.Log(PositionHistory[0]);
            foreach (var body in BodyParts)
            {
                Vector3 point = PositionHistory[Mathf.Min(index + distance, PositionHistory.Count - 1)];
                body.transform.position = point;
                LastPos = point;
                index++;
            }
        }
        isReseting = true;

        ResetMoving();
    }
    void WormHeadMove()
    {
        if (isMoving)
        {
            lastPart = DropDownParts.Last();
            LastPos = transform.position;
            if (isLeft)
            {
               transform.Translate( Vector2.left);
            }
            if (isRight)
            {
                transform.Translate(Vector2.right);
            }
            if (isUp)
            {
                transform.Translate(Vector2.up);
            }
            if (isDown)
            {
               transform.Translate(Vector2.down);
            }
        }
        BodyMoving();
    }
    void ResetMoving()
    {
        isLeft = false;
        isRight = false;
        isUp = false;
        isDown = false;
        isMoving = false;
        isReseting = false;
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Apple")
        {
            Growth();
        }
        if(collision.gameObject.tag == "Victory")
        {
            gameObject.GetComponent<WormController>().enabled = false;
            Time.timeScale = 0f;
        }
        if(collision.gameObject.tag == "Spike")
        {
            Time.timeScale = 0f;
            Debug.Log("You Lose");
        }
    }
    //Movement
    public void Left()
    {
        isMoving = true;
        isLeft = true;
    }
    public void Right()
    {
        isMoving = true;
        isRight =true;
    }
    public void Up()
    {  
        isMoving = true;
        isUp = true;
    }
    public void Down()
    {      
        isMoving = true;
        isDown =true;
    }


}


