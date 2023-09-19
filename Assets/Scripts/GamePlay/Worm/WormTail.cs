using System;
using System.Collections;
using System.Linq;
using UnityEngine;


public class WormTail : MonoBehaviour
{
    public static WormTail Instance;
    [Header("----------------PREFABS & OBJECT----------------")]
    [SerializeField] private LayerMask layer;
    public Sprite[] tailSprites;
    [Header("----------------RayCast Object----------------")]
    public GameObject DownRay;
    public GameObject UpRay;
    public GameObject RightRay;
    public GameObject LeftRay;
    public float distanceFromTailToWall;

    [Header("----------------Tail Flags----------------")]
    bool ObjectSpotLeft;
    bool ObjectSpotRight;
    bool ObjectSpotUp;
    bool ObjectSpotDown;

    public BoxCollider2D box;
    // Start is called before the first frame update
    private void Awake()
    {
        Instance = this;
    }
    // Update is called once per frame
    void Update()
    {
        DrawRayCastAtLastPart();
        ChangeSprite();   
    }
    public void DrawRayCastAtLastPart()
    {
        float distanceRuler = 0.09f;
        box = gameObject.GetComponent<BoxCollider2D>();
        RaycastHit2D hitdowns = Physics2D.Raycast(box.bounds.center, Vector2.down, box.bounds.extents.y + distanceRuler, layer);
        if (hitdowns.collider != null)
        {
            if (hitdowns.collider.CompareTag("Wall") || hitdowns.collider.CompareTag("Apple") || hitdowns.collider.CompareTag("Rock"))
            {
                Worm.Instance.isTailOnGround = true; 
            }
            else
            {
                Worm.Instance.isTailOnGround = false;
            }
        }
        else
        {
            Worm.Instance.isTailOnGround = false;
        }

        RaycastHit2D distanceHitdown = Physics2D.Raycast(DownRay.transform.position, Vector2.down);
        if (distanceHitdown.collider != null && !distanceHitdown.collider.CompareTag("Body") && !distanceHitdown.collider.CompareTag("Head"))
        {
            distanceFromTailToWall = distanceHitdown.distance;
        }
        else
        {
            distanceFromTailToWall = 1f;
        }

        RaycastHit2D hitdown = Physics2D.Raycast(DownRay.transform.position, Vector2.down);
        if (hitdown.collider != null)
        {
            if (hitdown.collider.tag == "Body")
            {
                ObjectSpotDown = true;
            }
            else
            {
                ObjectSpotDown = false;
            }
        }
        else
        {
            ObjectSpotDown = false;
        }
        RaycastHit2D hitleft = Physics2D.Raycast(LeftRay.transform.position, Vector2.left);
        if (hitleft.collider != null)
        {
            if (hitleft.collider.tag == "Body")
            {
                ObjectSpotLeft = true;
            }
            else
            {
                ObjectSpotLeft = false;
            }
        }
        else
        {
            ObjectSpotLeft = false;
        }
        RaycastHit2D hitright = Physics2D.Raycast(RightRay.transform.position, Vector2.right);
        if (hitright.collider != null)
        {
            if (hitright.collider.tag == "Body")
            {
                ObjectSpotRight = true;
            }
            else
            {
                ObjectSpotRight = false;
            }
        }
        else
        {
            ObjectSpotRight = false;
        }
        RaycastHit2D hitup = Physics2D.Raycast(UpRay.transform.position, Vector2.up);
        if (hitup.collider != null)
        {
           if (hitup.collider.tag == "Body")
           {
                ObjectSpotUp = true;

           }
           else
           {
                ObjectSpotUp = false;
           }
        }
        else
        {
            ObjectSpotUp = false;
        }

    }
    void ChangeSprite()
    {
        if (ObjectSpotDown)
        {
            GetComponent<SpriteRenderer>().sprite = tailSprites[0];
        }
        else if (ObjectSpotLeft)
        {
            GetComponent<SpriteRenderer>().sprite = tailSprites[1];
        }
        else if (ObjectSpotRight)
        {
            GetComponent<SpriteRenderer>().sprite = tailSprites[2];
        }
        else if (ObjectSpotUp)
        {
            GetComponent<SpriteRenderer>().sprite = tailSprites[3];
        }
    }
}
