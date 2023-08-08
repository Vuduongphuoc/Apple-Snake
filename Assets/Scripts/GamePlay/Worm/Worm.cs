using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using Unity.VisualScripting.Dependencies.Sqlite;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class Worm: MonoBehaviour
{
    public static Worm Instance;

    [Header("----------RAYCAST OBJECTS AND PREFABS------------")]
    public GameObject DownRay;
    public GameObject UpRay;
    public GameObject LeftRay;
    public GameObject RightRay;
    public GameObject BodyPrefabs;
    public GameObject TailPrefabs;

    [Header("-----------------POSITION------------------")]
    public List<GameObject> BodyParts = new List<GameObject>();
    public List<GameObject> DropDownParts = new List<GameObject>();
    public GameObject lastPart;
    public List<Vector3> PositionHistory = new List<Vector3>();
    public Vector3 LastPos;
    public Vector3 OriPos;

    [Header("--------CHECKING WORM IS ON THE GROUND OR NOT----------")]
    public bool isHeadOnGround;
    public bool isBodyOnGround;
    public bool isTailOnGround;
    public bool isGround;
    public bool isDropping;
    public bool isReseting;

    [Header("-------------BUTTON DIRECTION CHECK-----------------")]
    public bool isMoving;
    public bool isLeft;
    public bool isRight;
    public bool isUp;
    public bool isDown;

    [Header("------RAYCAST CHECK DIRECTION CAN MOVEABLE------")]
    public bool canMoveLeft;
    public bool canMoveRight;
    public bool canMoveUp;
    public bool canMoveDown;

    [Header("-----------WIN OR LOSE CONDICTION-------------")]
    public bool isWin;
    public bool isLose;

    private WormMovement movement;
    private WormBody body;
    int distance;

    private void Awake()
    {
        DropDownParts.Add(gameObject);
        Instance = this;
    }
    void Start()
    {
        distance = 1;
        isHeadOnGround = false;
        GrowthFirstBody();
        GrowthBody();
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
            if(hitdown.distance <= 0.1f)
            {
                if (hitdown.collider.tag == "Apple" || hitdown.collider.tag == "Win")
                {
                    canMoveDown = true;
                    isHeadOnGround = true;
                    ButtonDown.instance.btn.enabled = true;
                }
                else if (hitdown.collider.tag == "Spike")
                {
                    canMoveDown = true;
                    isHeadOnGround = false;
                    ButtonDown.instance.btn.enabled = true;
                }
                else if( hitdown.collider.tag == "Wall" || hitdown.collider.tag == "Rock")
                {
                    isHeadOnGround = true;
                    canMoveDown= false;
                    ButtonDown.instance.btn.enabled = false;
                }
                else
                {
                    isHeadOnGround = false;
                    ButtonDown.instance.btn.enabled = false;
                }
            }
            else
            {
                canMoveDown = true;
                isHeadOnGround = false;
                ButtonDown.instance.btn.enabled = true;
            }
        }
        else
        {
            canMoveDown = true;
            isHeadOnGround = false;
            ButtonDown.instance.btn.enabled = true;
        }
        
        RaycastHit2D hitup = Physics2D.Raycast(UpRay.transform.position, Vector2.up);
        Debug.DrawRay(UpRay.transform.position, Vector2.up * hitup.distance, Color.red);
        if (hitup.collider != null)
        {
            if (hitup.distance < 0.5)
            {

                if (hitup.collider.tag == "Apple" || hitup.collider.tag == "Win" || hitup.collider.tag == "Spike" || hitup.collider.tag == "Rock")
                {
                    canMoveUp = true;
                    ButtonUp.instance.btn.enabled = true;
                }
                else
                {
                    canMoveUp = false;
                    ButtonUp.instance.btn.enabled = false;
                }
            }
            else
            {
                canMoveUp = true;
                ButtonUp.instance.btn.enabled = true;
            }
        }
        else
        {
           canMoveUp = true;
            ButtonUp.instance.btn.enabled = true;
        }
        RaycastHit2D hitright = Physics2D.Raycast(RightRay.transform.position, Vector2.right);
        Debug.DrawRay(RightRay.transform.position, Vector2.right * hitright.distance, Color.red);
        if (hitright.collider != null)
        {
            if (hitright.distance < 0.5)
            {

                if (hitright.collider.tag == "Apple" || hitright.collider.tag == "Win" || hitright.collider.tag == "Spike" || hitright.collider.tag == "Rock")
                {
                    canMoveRight = true;
                    ButtonRight.instance.btn.enabled = true;
                }
                else
                {
                    canMoveRight = false;
                    ButtonRight.instance.btn.enabled = false;

                }
            }
            else
            {
                canMoveRight =true;
                ButtonRight.instance.btn.enabled = true;
            }
        }
        else
        {
            canMoveRight = true;
            ButtonRight.instance.btn.enabled = true;
        }

        RaycastHit2D hitleft = Physics2D.Raycast(LeftRay.transform.position, Vector2.left);
        Debug.DrawRay(LeftRay.transform.position, Vector2.left* hitleft.distance, Color.red);
        if(hitleft.collider != null)
        {
            if(hitleft.distance < 0.5)
            {
                if (hitleft.collider.tag == "Apple" || hitleft.collider.tag == "Win" || hitleft.collider.tag == "Spike" || hitleft.collider.tag == "Rock")
                {
                    canMoveLeft= true;
                    ButtonLeft.instance.btn.enabled = true;
                }
                else
                {
                    canMoveLeft = false;
                    ButtonLeft.instance.btn.enabled = false;
                }
            }
            else
            {
                canMoveLeft = true;
                ButtonLeft.instance.btn.enabled = true;
            }
        }
        else
        {
            canMoveLeft = true;
            ButtonLeft.instance.btn.enabled = true;
        }
    }
    void GrowthFirstBody()
    {
        Instantiate(BodyPrefabs, new Vector3(DropDownParts.First().transform.position.x - 1, DropDownParts.First().transform.position.y), Quaternion.identity);
    }
    void GrowthBody()
    {
        Instantiate(BodyPrefabs, DropDownParts.First().transform.position, Quaternion.identity);
    }
    public void BodyMoving()
    {
        int index = 0;
        if (isMoving)
        {
            foreach (var body in BodyParts)
            {
                Vector3 point = PositionHistory[Mathf.Min(index + distance, PositionHistory.Count - 1)];
                body.transform.position = point;
                LastPos = point;
                index++;

            }
        }
       resetMoveFlags();
    }
    public void resetMoveFlags()
    {
       
        isMoving = false;
        isLeft = false;
        isRight = false;
        isUp = false;
        isDown = false;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent(out Apple hot))
        {
            GrowthBody();
            
        }
        if(collision.gameObject.TryGetComponent(out SuperApple hhot))
        {
            GrowthBody();
            GrowthBody();
        }
    }
    //private void OnCollisionEnter2D(Collision2D collision)
    //{
    //    if(collision.gameObject.TryGetComponent(out Rock hot))
    //    {
    //        WormPush.Instance.DoPush();
    //    }
    //}
}


