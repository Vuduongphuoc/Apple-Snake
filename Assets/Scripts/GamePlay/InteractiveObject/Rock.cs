using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Rock : MonoBehaviour
{
    [SerializeField] Worm worm;
    [SerializeField] Transform DownRay;
    [SerializeField] Transform UpRay;
    [SerializeField] Transform LeftRay;
    [SerializeField] Transform RightRay;
    [SerializeField] private LayerMask layer;
    [SerializeField] float argodis;
    BoxCollider2D box;
    float distanceFromFaceToWall;
    float speed;
    public bool rockOnGround;
    private bool Left;
    private bool Right;
    // Start is called before the first frame update
    void Start()
    {
        box = gameObject.GetComponent<BoxCollider2D>();
        speed = 100f;
        distanceFromFaceToWall = 0.1f;
    }

    // Update is called once per frame
    void Update()
    {
        CheckPlayerIsNear(argodis);
        {
            
        }
        CheckOnGround();
        if (!rockOnGround)
        {
            Drop();
        }
    }
    void CheckOnGround()
    {
        RaycastHit2D hitdown = Physics2D.Raycast(DownRay.transform.position, Vector2.down);
        Debug.DrawRay(DownRay.transform.position, Vector2.down * hitdown.distance, Color.white);
        if (hitdown.collider != null)
        {
            if(hitdown.distance <= 0.05f)
            {
                if (hitdown.collider.tag == "Wall" || hitdown.collider.tag == "Apple" || hitdown.collider.tag == "Worm")
                {
                    rockOnGround = true;
                    distanceFromFaceToWall = hitdown.distance;
                }
                else
                {
                    rockOnGround = false;
                }
            }
            else
            {
                rockOnGround = false;
            }
        }
        else
        {
            rockOnGround = false;
        }
    }
    bool CheckPlayerIsNear(float distance)
    {
        bool val = false;
        float castDis = distance;

        Vector2 leftendpos = LeftRay.position + Vector3.left * castDis;
        Vector2 rightendpos = RightRay.position + Vector3.right * castDis;
        Vector2 upendpos = UpRay.position + Vector3.up * castDis;
        RaycastHit2D hitleft = Physics2D.Linecast(LeftRay.position, leftendpos, 1 << LayerMask.NameToLayer("Action"));
        RaycastHit2D hitright = Physics2D.Linecast(RightRay.position, rightendpos, 1 << LayerMask.NameToLayer("Action"));
        if (hitleft.collider != null)
        {
            if (hitleft.collider.tag == "Worm")
            {
                val = true;
                Left = true;
            }
            else
            {
                val = false;
                Left = false;
                
            }
            Debug.DrawRay(LeftRay.position, hitleft.point, Color.yellow);
           
        }
        else
        {
            Debug.DrawRay(LeftRay.position,hitleft.point, Color.blue);
            Left = false;
        }
        if(hitright.collider != null)
        {
            if(hitright.collider.tag == "Worm")
            {
                val = true;
                Right = true;
            }
            else
            {
                val = false;
                
                Right = false;

            }
            Debug.DrawLine(RightRay.position,hitright.point, Color.yellow);
        }
        else
        {
            Debug.DrawLine(RightRay.position,hitright.point, Color.blue);
            Right = false;
        }

        return val;
    }
    void Drop()
    {
        gameObject.transform.position = new Vector3(transform.position.x, transform.position.y - distanceFromFaceToWall * speed * Time.deltaTime);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Worm")
        {
            if (Left)
            {
                transform.Translate(Vector2.right);
                Left = false;
            }
            if (Right)
            {
                transform.Translate(Vector2.left);
                Right = false;
            }
        }
    }
    //private void OnCollisionEnter2D(Collision2D collision)
    //{
    //    if (collision.gameObject.tag == "Worm")
    //    {
    //        if (Left)
    //        {
    //            transform.Translate(Vector2.right);
    //            Left = false;
    //        }
    //        if (Right)
    //        {
    //            transform.Translate(Vector2.left);
    //            Right = false;

    //        }
    //    }
    //}


}
