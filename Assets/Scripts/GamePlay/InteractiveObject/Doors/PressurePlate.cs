using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePlate : MonoBehaviour
{
    [SerializeField] private GameObject doorGameObject;
    private IDoor door;

    private void Awake()
    {
        door = doorGameObject.GetComponent<IDoor>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag != null)
        {
            //Player entered collider!
            door.OpenDoor();
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.tag != null)
        {
            //Player exited collider!
            door.CloseDoor();
        }
    }
}
