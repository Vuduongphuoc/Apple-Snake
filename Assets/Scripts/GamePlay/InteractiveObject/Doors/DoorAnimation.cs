using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorAnimation : MonoBehaviour, IDoor
{

    private Animator doorAnimator;

    private void Awake()
    {
        doorAnimator = GetComponent<Animator>();
    }

    public void OpenDoor()
    {
        doorAnimator.SetBool("Open", true);
    }
    public void CloseDoor()
    {
        doorAnimator.SetBool("Open", false);
    }
    public void ToggleDoor()
    {

    }
    public void PlayOpenFailAnim()
    {
        doorAnimator.SetTrigger("Open Fail");
    }
}
