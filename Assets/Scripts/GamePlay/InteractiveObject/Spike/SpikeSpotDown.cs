using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeSpotDown : MonoBehaviour
{
    private Spike spike;

    private void Start()
    {
        spike = GetComponentInParent<Spike>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            spike.GetComponent<SpriteRenderer>().sprite = spike.spr[3];
        }
    }
}
