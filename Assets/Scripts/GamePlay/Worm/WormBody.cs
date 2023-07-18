
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class WormBody : MonoBehaviour
{
    [SerializeField] private LayerMask layer;

    public GameObject DownRay;
    float distanceFromTailToWall;
    BoxCollider2D box;
    BoxCollider2D ManyBoxes;
    float speed;

    // Start is called before the first frame update
    void Start()
    {
        speed = 5f;
    }

    // Update is called once per frame
    void Update()
    {
        DrawRayCastAtLastPart();
        DrawRayCastEveryPartOfBody();
        CheckOnGround();
    }
    void DrawRayCastAtLastPart()
    {
        float distanceRuler = 0.01f;
        WormController.lastPart = WormController.DropDownParts.Last();
        box = WormController.lastPart.GetComponent<BoxCollider2D>();
        RaycastHit2D hitdown = Physics2D.Raycast(box.bounds.center, Vector2.down, box.bounds.extents.y + distanceRuler, layer);
        Debug.DrawRay(box.bounds.center, Vector2.down * (box.bounds.extents.y + distanceRuler), Color.red);
        if (hitdown.collider != null)
        {
            if (hitdown.collider.tag == "Wall" || hitdown.collider.tag == "Apple")
            {
                WormController.isTailOnGround = true;
            }
        }
        else
        {
            WormController.isTailOnGround = false;
        }

        RaycastHit2D distanceHitdown = Physics2D.Raycast(DownRay.transform.position, Vector2.down);
        Debug.DrawRay(DownRay.transform.position,Vector2.down * distanceHitdown.distance, Color.red);
        if(distanceHitdown.collider != null)
        {
            distanceFromTailToWall = distanceHitdown.distance;
        }
    }
    void DrawRayCastEveryPartOfBody()
    {
        for (int i = 0; i < WormController.BodyParts.Count - 1; i++)
        {
            float distanceRulers = 0.01f;
            ManyBoxes = WormController.BodyParts[i].GetComponent<BoxCollider2D>();
            RaycastHit2D hitdowns = Physics2D.Raycast(ManyBoxes.bounds.center, Vector2.down, ManyBoxes.bounds.extents.y + distanceRulers, layer);
            Debug.DrawRay(ManyBoxes.bounds.center, Vector2.down * (box.bounds.extents.y + distanceRulers), Color.blue);
            if (hitdowns.collider != null)
            {
                if (hitdowns.collider.tag == "Wall" || hitdowns.collider.tag == "Apple")
                {
                    WormController.isBodyOnGround = true;
                }
            }
            else
            {
                WormController.isBodyOnGround = false;
            }
        }
    }
    void CheckOnGround()
    {
        if (WormController.isTailOnGround == false)
        {
            if(WormController.isBodyOnGround == false)
            {
                if(WormController.isHeadOnGround == false)
                {
                    WormController.isGround = false;
                    if (WormController.isGround == false)
                    {
                        Debug.Log("not on ground");
                        StartCoroutine(PositionAfterDrop());
                    }
                }
            }
        }
        else
        {
            Debug.Log("on ground");
            WormController.isGround = true;
        }
    }
    void DropDown()
    {
        StartCoroutine(WaitBeforeDrop());
        foreach (GameObject part in WormController.DropDownParts)
        {
            part.transform.position = new Vector3(part.transform.position.x, part.transform.position.y - distanceFromTailToWall * speed * Time.deltaTime);
        }
    }
    IEnumerator PositionAfterDrop()
    {
        DropDown();
        yield return new WaitForSeconds(0.1f);
        WormController.isDropping = true;
        if (WormController.isDropping == true)
        {
            
            WormController.PositionHistory.Clear();
            foreach (GameObject bodies in WormController.DropDownParts)
            {
                WormController.PositionHistory.Add(bodies.transform.position);
            }
            WormController.isDropping = false;
        }

    }
    IEnumerator WaitBeforeDrop()
    {
        yield return new WaitForSeconds(1f);
    }
}

