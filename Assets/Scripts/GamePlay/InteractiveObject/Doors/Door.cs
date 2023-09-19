using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    private Animator doorAnimator;

    private void Awake()
    {
        doorAnimator = GetComponent<Animator>();
    }
}
