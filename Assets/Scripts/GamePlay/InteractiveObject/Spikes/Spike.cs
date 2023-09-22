using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Sprites;

public class Spike : MonoBehaviour
{
    public Sprite[] sprs;
    public GameObject[] spikeSpotPoint;
    SpriteRenderer thisSpr;
    private void Start()
    {
        thisSpr = GetComponent<SpriteRenderer>();
    }
    private void OnEnable()
    {
        DrawRay();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent(out Worm hot) || collision.gameObject.TryGetComponent(out WormBody cold) || collision.gameObject.TryGetComponent(out WormTail Freeze))
        {
            WormMovement.Instance.TurnOffMovement();
            AudioManager.instance.PlaySFX(AudioManager.instance.death);
            Debug.Log("You Lose");
        }

    }
    #region RayCast
    void DrawRay()
    {
        RaycastHit2D hitdown = Physics2D.Raycast(spikeSpotPoint[0].transform.position, Vector2.down, 0.1f, 6);
        RaycastHit2D hitleft= Physics2D.Raycast(spikeSpotPoint[1].transform.position, Vector2.left, 0.1f, 6);
        RaycastHit2D hitright = Physics2D.Raycast(spikeSpotPoint[2].transform.position, Vector2.right, 0.1f, 6);
        RaycastHit2D hitup = Physics2D.Raycast(spikeSpotPoint[3].transform.position, Vector2.up, 0.1f, 6);
        if (hitdown.collider != null && hitdown.distance < 0.1f)
        {
            if (hitdown.collider.CompareTag("Wall"))
            {
                thisSpr.sprite = sprs[0];
            }
        }
        if (hitleft.collider != null && hitleft.distance < 0.1f)
        {
            if (hitleft.collider.CompareTag("Wall"))
            {
                thisSpr.sprite = sprs[1];
            }
        }
        if (hitright.collider != null && hitright.distance < 0.1f)
        {
            if (hitright.collider.CompareTag("Wall"))
            {
                thisSpr.sprite = sprs[2];
            }
        }
        if (hitup.collider != null && hitup.distance < 0.1f)
        {
            if (hitup.collider.CompareTag("Wall"))
            {
                thisSpr.sprite = sprs[3];
            }
        }
    }
    #endregion
}
