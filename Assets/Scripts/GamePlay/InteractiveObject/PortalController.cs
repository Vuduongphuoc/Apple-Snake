using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalController : MonoBehaviour
{
    public Transform destination;
    public bool isProtalA;
    private float distance = 0.2f;
    private float coolDown;
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
        float time = 0f;
        if(Vector2.Distance(transform.position, collision.transform.position) > distance && !collision.CompareTag("Wall") )
        {
            while(time < 1f)
            {
                destination.GetComponent<BoxCollider2D>().enabled = false;
                collision.transform.position = new Vector2(destination.position.x + 0.5f, destination.position.y);
            }
            time = time * Time.deltaTime;
            
            destination.GetComponent<BoxCollider2D>().enabled = true;
        }
    }
}
