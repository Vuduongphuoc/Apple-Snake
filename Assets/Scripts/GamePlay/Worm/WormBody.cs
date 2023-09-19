using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

public class WormBody : MonoBehaviour
{
    [Header("----------------PREFABS & OBJECT----------------")]
    [SerializeField] private LayerMask layer;
    public List<WormBody> list = new List<WormBody>();
    public Sprite[] bodySprite;
    [Header("----------------RayCast Object----------------")]
    public GameObject DownRay;
    public GameObject UpRay;
    public GameObject LeftRay;
    public GameObject RightRay;

    [Header("----------------Body Flags----------------")]
    bool ObjectSpotLeft; 
    bool ObjectSpotRight; 
    bool ObjectSpotUp; 
    bool ObjectSpotDown;

    public int a = 0;
    public bool IsBodyOnGround;
    BoxCollider2D ManyBoxes;
    // Start is called before the first frame update

    // Update is called once per frame
    void Update()
    {
        DrawRayCastEveryPartOfBody();
    }
    //Draw Ray at every parts of body

    void DrawRayCastEveryPartOfBody()
    {
        float distanceRulers = 0.09f;
        ManyBoxes = gameObject.GetComponent<BoxCollider2D>();
        RaycastHit2D hitdowns = Physics2D.Raycast(ManyBoxes.bounds.center, Vector2.down, ManyBoxes.bounds.extents.y + distanceRulers, layer);
        if (hitdowns.collider != null)
        {
            if (hitdowns.collider.CompareTag("Wall") || hitdowns.collider.CompareTag("Apple") || hitdowns.collider.CompareTag("Rock"))
            {   
                IsBodyOnGround = true;
            }
            else
            {
                IsBodyOnGround = false;
            }
        }
        else
        {
            IsBodyOnGround = false;
        }
        RaycastHit2D hitdown = Physics2D.Raycast(DownRay.transform.position, Vector2.down);   
        RaycastHit2D hitleft = Physics2D.Raycast(LeftRay.transform.position, Vector2.left);
        RaycastHit2D hitright = Physics2D.Raycast(RightRay.transform.position, Vector2.right);
        RaycastHit2D hitup = Physics2D.Raycast(UpRay.transform.position, Vector2.up);
        if (hitdown.collider != null && hitdown.distance < 0.1f)
        {
            if (hitdown.collider.tag == "Tail" || hitdown.collider.tag == "Head" || hitdown.collider.tag =="Body")
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
        if (hitleft.collider != null && hitleft.distance < 0.1f)
        {
            if (hitleft.collider.tag == "Tail" || hitleft.collider.tag == "Head" || hitleft.collider.tag == "Body")
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
        if (hitright.collider != null && hitright.distance < 0.1f)
        {
            if (hitright.collider.tag == "Tail" || hitright.collider.tag == "Head" || hitright.collider.tag == "Body")
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
        if (hitup.collider != null && hitup.distance < 0.1f)
        {
            if (hitup.collider.tag == "Tail" || hitup.collider.tag == "Head" || hitup.collider.tag == "Body")
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
        ChangeSprite();
    }
    void ChangeSprite()
    {
        if (ObjectSpotRight)
        {
           if (ObjectSpotLeft )
           {
                ManyBoxes.GetComponent<SpriteRenderer>().sprite = bodySprite[6];
           }
           else if (ObjectSpotUp )
           {
                ManyBoxes.GetComponent<SpriteRenderer>().sprite = bodySprite[4];          
           }
           else if (ObjectSpotDown)
           {
                ManyBoxes.GetComponent<SpriteRenderer>().sprite = bodySprite[3];

           }

        }
        else if (ObjectSpotLeft) 
        {
            if(ObjectSpotRight)
            {
                ManyBoxes.GetComponent<SpriteRenderer>().sprite = bodySprite[6];
            }
            else if (ObjectSpotUp)
            {                
                ManyBoxes.GetComponent<SpriteRenderer>().sprite = bodySprite[5];  
            }
            else if (ObjectSpotDown)
            {
                ManyBoxes.GetComponent<SpriteRenderer>().sprite = bodySprite[2];
            }
        }
        else if (ObjectSpotUp)
        {

            if (ObjectSpotRight)
            {
                ManyBoxes.GetComponent<SpriteRenderer>().sprite = bodySprite[5];

            }
            else if (ObjectSpotLeft)
            {
                ManyBoxes.GetComponent<SpriteRenderer>().sprite = bodySprite[4];
            }
            if (ObjectSpotDown)
            {
                ManyBoxes.GetComponent<SpriteRenderer>().sprite = bodySprite[7];

            }
        }
        else if (ObjectSpotDown)
        {
            if (ObjectSpotRight)
            {
                ManyBoxes.GetComponent<SpriteRenderer>().sprite = bodySprite[3];
            }
            else if (ObjectSpotLeft)
            {
                ManyBoxes.GetComponent<SpriteRenderer>().sprite = bodySprite[2];

            }
            else if (ObjectSpotUp)
            {
                ManyBoxes.GetComponent<SpriteRenderer>().sprite = bodySprite[7];
            }


        }

    }

}

