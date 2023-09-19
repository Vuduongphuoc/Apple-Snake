using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Rock : MonoBehaviour
{
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
    bool Left;
    bool Right;
    // Start is called before the first frame update
    void Start()
    {
        box = gameObject.GetComponent<BoxCollider2D>();
        speed = 8f;
    }
    // Update is called once per frame
    void Update()
    {
        //CheckPlayerIsNear(argodis);
        CheckOnGround();
    }
    void CheckOnGround()
    {
        RaycastHit2D hitdown = Physics2D.Raycast(DownRay.transform.position, Vector2.down);
        Debug.DrawRay(DownRay.transform.position, Vector2.down * hitdown.distance, Color.white);
        if (hitdown.collider != null)
        {
            if (hitdown.distance < 0.1f)
            {
                if (hitdown.collider.tag != "Spike")
                {
                    rockOnGround = true;
                    distanceFromFaceToWall = hitdown.distance;
                }
                else
                {
                    rockOnGround = false;
                    distanceFromFaceToWall = 1f;
                    Drop();
                }
            }
            else
            {
                rockOnGround = false;
                distanceFromFaceToWall = 1f;
                Drop();
            }
        }
        else
        {
            rockOnGround = false;
            distanceFromFaceToWall = 1f;
            Drop();
        }
        
    }
    //bool CheckPlayerIsNear(float distance)
    //{
    //    bool val = false;
    //    float castDis = distance;

    //    Vector2 leftEndPos = LeftRay.position + Vector3.left * castDis;
    //    Vector2 rightEndPos = RightRay.position + Vector3.right * castDis;
    //    RaycastHit2D hitLeft = Physics2D.Linecast(LeftRay.position, leftEndPos, 1 << LayerMask.NameToLayer("Action"));
    //    RaycastHit2D hitRight = Physics2D.Linecast(RightRay.position, rightEndPos, 1 << LayerMask.NameToLayer("Action"));
    //    if (hitLeft.collider != null)
    //    {
    //        if (hitLeft.collider.tag == "Head")
    //        {
    //            val = true;
    //            Left = true; 
    //        }
    //        else
    //        {
    //            val = false;
    //            Left = false;
    //        }
    //    }
    //    else
    //    {
    //        Left = false;
    //    }
        
    //    if(hitRight.collider != null)
    //    {
    //        if (hitRight.collider.tag == "Head")
    //        {
    //            val = true;
    //            Right = true;
    //        }
    //        else
    //        {
    //            val = false;
    //            Right = false; 
    //        }
    //    }
    //    else
    //    {
    //        Right = false;
    //    }
    //    return val;
    //}
    void Drop()
    {
        gameObject.transform.position = new Vector3(transform.position.x, transform.position.y - distanceFromFaceToWall * speed * Time.deltaTime);
    }
    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    if (collision.gameObject.tag == "Head")
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
