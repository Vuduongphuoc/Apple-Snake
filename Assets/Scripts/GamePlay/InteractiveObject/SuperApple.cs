using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuperApple : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.TryGetComponent(out Worm hot))
        {
            gameObject.SetActive(false);
        }
    }
}
