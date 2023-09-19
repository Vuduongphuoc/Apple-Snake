using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Sprites;

public class Spike : MonoBehaviour
{
    public Sprite[] spr;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.TryGetComponent(out Worm hot) || collision.gameObject.TryGetComponent(out WormBody i) 
            || collision.gameObject.TryGetComponent(out WormTail t))
        {
            Worm.Instance.isLose = true;
            WormMovement.Instance.TurnOffMovement();
            Debug.Log("You Lose");

        }
        if(collision.gameObject.TryGetComponent(out Rock cold))
        {
            gameObject.GetComponent<SpriteRenderer>().sortingOrder = -5;
        }
    }
}
