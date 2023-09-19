using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalController : MonoBehaviour
{
    public Transform destination;
    public bool isProtalA;
    private float distance = 0.2f;

    private void Start()
    {
        if (!isProtalA)
        {
            destination =GameObject.FindGameObjectWithTag("PortalA").GetComponent<Transform>();
            
        }
        else
        {
            destination = GameObject.FindGameObjectWithTag("PortalB").GetComponent<Transform>();
            
        }
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(Vector2.Distance(transform.position, collision.transform.position) > distance && !collision.CompareTag("Wall") )
        {
           collision.transform.position = new Vector2(destination.position.x - 0.000001f, destination.position.y);
        }   
    }
}
