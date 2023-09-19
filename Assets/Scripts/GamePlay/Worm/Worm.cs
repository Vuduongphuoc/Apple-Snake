using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;


public class Worm: MonoBehaviour
{
    public static Worm Instance;

    [Header("----------RAYCAST OBJECTS AND PREFABS------------")]
    public GameObject UpRay;
    public GameObject LeftRay;
    public GameObject RightRay;
    public GameObject DownRay;
    public GameObject BodyPrefabs;
    public GameObject TailPrefabs;
    public BoxCollider2D headbox;
    [SerializeField] private LayerMask layer;

    [Header("-----------------POSITION------------------")]
    public List<GameObject> BodyParts = new List<GameObject>();
    public List<GameObject> DropDownParts = new List<GameObject>();
    public List<Vector3> PositionHistory = new List<Vector3>();
    public List<GameObject> bodylist = new List<GameObject>();


    [Header("--------CHECKING WORM IS ON THE GROUND OR NOT----------")]
    public bool isHeadOnGround;
    public bool isTailOnGround;
    public bool isBodyOnGround;
    public bool isGround;

    [Header("------RAYCAST CHECK DIRECTION CAN MOVEABLE------")]
    public bool canMoveLeft;
    public bool canMoveRight;
    public bool canMoveUp;
    public bool canMoveDown;

    [Header("-----------WIN OR LOSE CONDICTION-------------")]
    public bool isWin;
    public bool isLose;

    public int distance;

    private void Awake()
    {
        DropDownParts.Add(gameObject);
        Instance = this;
    }
    private void OnEnable()
    {
        PositionHistory.Insert(0, transform.position);
    }
    void Start()
    {
        distance = 1;
        isHeadOnGround = false;
        GrowthFirstBody();
        GrowthTail();
    }

    // Update is called once per frame
    void Update()
    {

        DrawRayCast();
    }

    void DrawRayCast()
    {
        RaycastHit2D hitdown = Physics2D.Raycast(DownRay.transform.position,Vector2.down);
        RaycastHit2D hitup = Physics2D.Raycast(UpRay.transform.position, Vector2.up);
        RaycastHit2D hitright = Physics2D.Raycast(RightRay.transform.position, Vector2.right);
        RaycastHit2D hitleft = Physics2D.Raycast(LeftRay.transform.position, Vector2.left);
        if (hitdown.collider != null)
        {
            if (hitdown.distance < 0.09f)
            {
                if (hitdown.collider.CompareTag("Apple") || hitdown.collider.CompareTag("Win") ||
                    hitdown.collider.CompareTag("PortalA") || hitdown.collider.CompareTag("PortalB"))
                {
                    canMoveDown = true;
                    isHeadOnGround = true;
                }
                else if (hitdown.collider.CompareTag("Spike"))
                {
                    canMoveDown = true;
                    isHeadOnGround = false;
                }
                else if (hitdown.collider.CompareTag("Wall") || hitdown.collider.CompareTag("Rock"))
                {
                    canMoveDown = false;
                    isHeadOnGround = true;
                }
                else
                {
                    isHeadOnGround = false;
                }

            }
            else
            {
                canMoveDown = true;
                isHeadOnGround = false;
            }
        }
        else
        {
            canMoveDown = true;
            isHeadOnGround = false; 
        }
        if (hitup.collider != null)
        {
            if (hitup.distance < 0.1)
            {
                if (hitup.collider.CompareTag("Wall") || hitup.collider.CompareTag("Body") || hitup.collider.CompareTag("Tail"))
                {
                    canMoveUp = false;
                    
                }
                else
                {
                    canMoveUp = true;
                    
                }
            }
            else
            {
                canMoveUp = true;
                
            }
        }
        else
        {
           canMoveUp = true;
            
        }
        if (hitright.collider != null)
        {
            if (hitright.distance < 0.1)
            {
                if (hitright.collider.CompareTag("Wall") || hitright.collider.CompareTag("Body") || hitright.collider.CompareTag("Tail"))
                {
                    canMoveRight = false;
                }
                else
                {
                    canMoveRight = true;
                }
            }
            else
            {
                canMoveRight =true;   
            }
        }
        else
        {
            canMoveRight = true;   
        }
        if(hitleft.collider != null)
        {
            if(hitleft.distance < 0.1)
            {
                if (hitleft.collider.CompareTag("Wall") || hitleft.collider.CompareTag("Body") || hitleft.collider.CompareTag("Tail"))
                {
                    canMoveLeft= false; 
                }
                else
                {
                    canMoveLeft = true;
                }
            }
            else
            {
                canMoveLeft = true;
            }
        }
        else
        {
            canMoveLeft = true;
        }
    }
    void GrowthFirstBody()
    {
        GameObject firstbody  = Instantiate(BodyPrefabs, new Vector3(DropDownParts.First().transform.position.x - 1, DropDownParts.First().transform.position.y), Quaternion.identity);
        bodylist.Add(firstbody);
        PositionHistory.Insert(1, firstbody.transform.position);
        BodyParts.Insert(0,firstbody);
        DropDownParts.Insert(1,firstbody);
    }
    void GrowthTail()
    {
        GameObject tail = Instantiate(TailPrefabs, new Vector3(DropDownParts.First().transform.position.x - 2, DropDownParts.First().transform.position.y), Quaternion.identity);
        PositionHistory.Insert(2, tail.transform.position);
        BodyParts.Insert(1, tail);
        DropDownParts.Insert(2, tail);
    }
    void GrowthBody()
    {
        GameObject newbody = Instantiate(BodyPrefabs,BodyParts.First().transform.position, Quaternion.identity);
        bodylist.Add(newbody);
        BodyParts.Insert(1,newbody);
        DropDownParts.Insert(2, newbody);

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent(out Apple hot))
        {
            GrowthBody();
            AudioManager.instance.PlaySFX(AudioManager.instance.eat);   
        }
        if(collision.gameObject.TryGetComponent(out SuperApple hhot))
        {
            GrowthBody();
            GrowthBody();
            AudioManager.instance.PlaySFX(AudioManager.instance.eat);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent(out Spike hot))
        {
            AudioManager.instance.PlaySFX(AudioManager.instance.death);
        }
    }
}


