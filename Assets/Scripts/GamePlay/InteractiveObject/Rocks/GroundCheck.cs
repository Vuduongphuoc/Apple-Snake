using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class GroundCheck : MonoBehaviour
{
    [SerializeField] private LayerMask _mask;
    public bool isGrounded;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        isGrounded = collision != null && (((1 << collision.gameObject.layer) & _mask) != 0);
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        isGrounded = false;
    }
}
