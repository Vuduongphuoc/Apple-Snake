using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;


public class WormMovement : MonoBehaviour
{
    public static WormMovement Instance;
    public Sprite[] Spr;
    
    [Header("-------------BUTTON DIRECTION CHECK-----------------")]
    public bool isMoving;
    public Vector2 WinDirection;

    bool isCoolDown;
    Vector2 startPos;
    Vector2 endPos;
    float speed;
    float moveDuration = 0.1f;
    float gridSize = 1f;

    // Update is called once per frame
    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        speed = 5f;
        isCoolDown = false;
    }
    private void FixedUpdate()
    {
        if (!isMoving)
        {
            CheckBody();
            CheckOnGround();
        }
    }
    public IEnumerator WormMove(Vector2 direction)
    {
        isMoving = true;
        AudioManager.instance.PlaySFX(AudioManager.instance.move);
        startPos = transform.position;
        endPos = startPos +(direction * gridSize);
        float elappedTime = 0;
        while(elappedTime < moveDuration)
        {
            elappedTime += Time.deltaTime;
            float percent = elappedTime / moveDuration;
            transform.position = Vector2.Lerp(startPos,endPos,percent);
            BodyMoving();
            yield return new WaitForEndOfFrame();
        }
        transform.position = endPos;
        Worm.Instance.PositionHistory.Insert(0, transform.position);
        yield return new WaitForSeconds(0.1f);
        isMoving = false;
    }
    public void BodyMoving()
    {
        int index = 0;

        foreach (var body in Worm.Instance.BodyParts)
        {
            Vector2 point = Worm.Instance.PositionHistory[Mathf.Min(index * Worm.Instance.distance, Worm.Instance.PositionHistory.Count - 1)];
            body.transform.position = Vector2.Lerp(body.transform.position, point, 30f * Time.deltaTime);
            index++;
        }
    }
    void CheckBody()
    {
        Worm.Instance.isBodyOnGround = Worm.Instance.bodylist.Any(c=> c.GetComponent<WormBody>().IsBodyOnGround == true);
    }
    void DropDown()
    {
        foreach (var part in Worm.Instance.DropDownParts)
        {
            part.transform.position = new Vector2(part.transform.position.x, part.transform.position.y - WormTail.Instance.distanceFromTailToWall * speed * Time.deltaTime);
        }
    }
    void DropDownAndPositionAfterDrop()
    {
        int i =0;
        if (Worm.Instance.isWin)
        {
            this.enabled = false;
        }
        else
        {
            if (!Worm.Instance.isGround)
            {
                DropDown();
            }
            Worm.Instance.PositionHistory.Clear();
            for(i = 0; i < Worm.Instance.DropDownParts.Count;)
            {
                Worm.Instance.PositionHistory.Add(Worm.Instance.DropDownParts[i].transform.position);
                i++;
            }
        }
    }
    public void TurnOffMovement()
    {
        this.enabled = false;
    }
    public void CheckOnGround()
    {
        if (!Worm.Instance.isTailOnGround)
        {
            if (!Worm.Instance.isHeadOnGround)
            {
                if(!Worm.Instance.isBodyOnGround)
                {
                    Worm.Instance.isGround = false;
                    DropDownAndPositionAfterDrop();
                }
                else { Worm.Instance.isGround = true; }
            }
            else { Worm.Instance.isGround = true; }
        }
        else { Worm.Instance.isGround = true; }
    }
    public void Left()
    {
        if (Worm.Instance.canMoveLeft && !isCoolDown)
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = Spr[1];
            StartCoroutine(WormMove(Vector2.left));
            WinDirection = Vector2.left;
            StartCoroutine(CoolDown());
        }
    }
    public void Right()
    {
        if (Worm.Instance.canMoveRight && !isCoolDown)
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = Spr[2];
            StartCoroutine(WormMove(Vector2.right));
            WinDirection = Vector2.right;
            StartCoroutine(CoolDown());
        }
    }
    public void Up()
    {
        if (Worm.Instance.canMoveUp && !isCoolDown)
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = Spr[3];
            StartCoroutine(WormMove(Vector2.up));
            WinDirection = Vector2.up;
            StartCoroutine(CoolDown());
        }
    }
    public void Down()
    {
        if (Worm.Instance.canMoveDown && !isCoolDown)
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = Spr[0];
            StartCoroutine(WormMove(Vector2.down));
            WinDirection = Vector2.down;
            StartCoroutine(CoolDown());
        }
    }

    IEnumerator CoolDown()
    {
        isCoolDown = true;
        yield return new WaitForSeconds(0.3f);
        isCoolDown = false;
    }
    

}
