using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class WormMovement : MonoBehaviour
{
    private Worm worm;
    private SpriteRenderer wormSprite;
    private GameObject win;
    float speed;
    // Update is called once per frame
    void OnEnable()
    {
        speed = 3f;
        worm = GetComponent<Worm>();
        win = GameObject.FindGameObjectWithTag("Win");
        wormSprite = worm.GetComponent<SpriteRenderer>();
    }
    private void Start()
    {
        
    }
    void Update()
    {
        WormMove();
        if (worm.isWin)
        {
            WormWin();
        }
    }
    void WormMove()
    {
        if (worm.isMoving)
        {
            if (worm.isLeft && worm.canMoveLeft)
            {
                worm.transform.Translate(Vector2.left);
               
                
            }
            if (worm.isRight && worm.canMoveRight)
            {
                
                worm.transform.Translate(Vector2.right );

            }

            if (worm.isUp && worm.canMoveUp)
            {
                
                worm.transform.Translate(Vector2.up );

            }

            if (worm.isDown && worm.canMoveDown)
            {
               
                worm.transform.Translate(Vector2.down );

            }
            worm.PositionHistory.Insert(0, transform.position);
            worm.BodyMoving();
        }
       
    }
    void WormWin()
    {
        worm.transform.Translate(Vector2.Lerp(worm.transform.position, win.transform.position, speed));
    }
    //void OnCollisionEnter2D(Collision2D collision)
    //{
    //    var direction = (worm.transform.position - rock.transform.position).normalized;
    //    playerrb.AddForce(direction * force, ForceMode2D.Impulse);
    //}
    //public bool Move(Vector2 direction)
    //{
    //    if (Mathf.Abs(direction.x) == 0)
    //    {
    //        direction.x = 0;
    //    }
    //    else
    //    {
    //        direction.y = 0;
    //    }
    //    direction.Normalize();
    //}
    //    if (Blocked(transform.position, direction))
    //    {
    //        return false;
    //    }
    //    else
    //    {
    //        transform.Translate(direction);
    //        return true;
    //    }
    //}

    //public bool Blocked(Vector3 pos,Vector2 direction)
    //{
    //    Vector2 newpos = new Vector2(pos.x, pos.y) + direction;
    //    foreach (var db in Wall)
    //    {
    //        if(db.transform.position.x == newpos.x && db.transform.position.y == newpos.y)
    //        {
    //            return true;
    //        }
    //    }
    //    foreach(var rock in ObjToPush)
    //    {
    //        if(rock.transform.position.x == newpos.x && rock.transform.position.y == newpos.y)
    //        {
    //            WormPush objpush = rock.GetComponent<WormPush>();
    //            if(objpush&& objpush.Move(direction))
    //            {
    //                return false;
    //            }
    //            else
    //            {
    //                return true;
    //            }
    //        }
    //    }
    //    return false;
    //}
    public void Left()
    {
        worm.isMoving = true;
        worm.isLeft = true;
        WormMove();
    }
    public void Right()
    {
        worm.isMoving = true;
        worm.isRight = true;
        WormMove();

    }
    public void Up()
    {
        worm.isMoving = true;
        worm.isUp = true;
        WormMove();

    }
    public void Down()
    {
        worm.isMoving = true;
        worm.isDown = true;
        WormMove();

    }

}
