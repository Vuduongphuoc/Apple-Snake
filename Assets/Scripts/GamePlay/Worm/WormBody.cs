
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using UnityEditor;
using UnityEngine;

public class WormBody : MonoBehaviour
{
    [SerializeField] private LayerMask layer;
    public Sprite tailSprite;
    public GameObject DownRay;
    
    BoxCollider2D box;
    BoxCollider2D ManyBoxes;
    float distanceFromTailToWall;
    float speed;

    // Start is called before the first frame update
    void Start()
    {
        Worm.Instance.BodyParts.Add(gameObject);
        Worm.Instance.DropDownParts.Add(gameObject);
        speed = 10f;
    }

    // Update is called once per frame
    void Update()
    {
        DrawRayCastAtLastPart();
        DrawRayCastEveryPartOfBody();
    }
    void DrawRayCastAtLastPart()
    {
        float distanceRuler = 0.1f;
        Worm.Instance.lastPart = Worm.Instance.BodyParts.Last();
        box = Worm.Instance.lastPart.GetComponent<BoxCollider2D>();
        Worm.Instance.lastPart.GetComponent<SpriteRenderer>().sprite = tailSprite;
        
        RaycastHit2D hitdown = Physics2D.Raycast(box.bounds.center, Vector2.down, box.bounds.extents.y + distanceRuler, layer);
        Debug.DrawRay(box.bounds.center, Vector2.down * (box.bounds.extents.y + distanceRuler), Color.white);
        if (hitdown.collider != null)
        {
            
            if (hitdown.collider.tag == "Wall" || hitdown.collider.tag == "Apple" || hitdown.collider.tag == "Rock")
            {
                Worm.Instance.isTailOnGround = true;
            }
        }
        else
        {
            Worm.Instance.isTailOnGround = false;
        }

        RaycastHit2D distanceHitdown = Physics2D.Raycast(DownRay.transform.position, Vector2.down);
        Debug.DrawRay(DownRay.transform.position, Vector2.down * distanceHitdown.distance, Color.red);
        if (distanceHitdown.collider != null)
        {
            distanceFromTailToWall = distanceHitdown.distance;
        }
        else
        {
            distanceFromTailToWall = 0.1f;
        }
    }
    // Draw Ray at every parts of body
    
    void DrawRayCastEveryPartOfBody()
    {
        for(int i = 0; i < Worm.Instance.BodyParts.Count - 1; i++)
        {
            float distanceRulers = 0.1f;
            ManyBoxes = Worm.Instance.BodyParts[i].GetComponent<BoxCollider2D>();
            RaycastHit2D hitdowns = Physics2D.Raycast(ManyBoxes.bounds.center, Vector2.down, ManyBoxes.bounds.extents.y + distanceRulers, layer);
            Debug.DrawRay(ManyBoxes.bounds.center, Vector2.down * (box.bounds.extents.y + distanceRulers), Color.yellow);
            if (hitdowns.collider != null)
            {
                
                if (hitdowns.collider.tag == "Wall" || hitdowns.collider.tag == "Apple" || hitdowns.collider.tag == "Rock")
                {
                    Worm.Instance.isBodyOnGround = true;
                }
            }
            else
            {
                Worm.Instance.isBodyOnGround = false;
            }
        }
        CheckOnGround();
    }
    
    void CheckOnGround()
    {
        if (Worm.Instance.isTailOnGround == false)
        {
            
            if(Worm.Instance.isBodyOnGround == false)
            {
                
                if(Worm.Instance.isHeadOnGround == false)
                {

                    Worm.Instance.isGround = false;
                    if (Worm.Instance.isGround == false)
                    {
                        StartCoroutine(PositionAfterDrop());
                    }
                }
                else
                {
                    Worm.Instance.isGround = true;
                }
            }
            else
            {
                Worm.Instance.isGround = true;
            }
        }
        else
        {
            Worm.Instance.isGround = true;
        }
    }
    void DropDown()
    {
        StartCoroutine(WaitBeforeDrop());
        foreach (GameObject part in Worm.Instance.DropDownParts)
        {
            part.transform.position = new Vector3(part.transform.position.x, part.transform.position.y - distanceFromTailToWall * speed * Time.deltaTime);
        }
    }
    
    IEnumerator PositionAfterDrop()
    {
        DropDown();
        yield return new WaitForSeconds(0.1f);
        Worm.Instance.isDropping = true;
        if (Worm.Instance.isDropping == true)
        {
            
            Worm.Instance.PositionHistory.Clear();
            foreach (GameObject bodies in Worm.Instance.DropDownParts)
            {
                Worm.Instance.PositionHistory.Add(bodies.transform.position);
            }
            Worm.Instance.isDropping = false;
        }

    }
    IEnumerator WaitBeforeDrop()
    {
        yield return new WaitForSeconds(1f);
    }
}

