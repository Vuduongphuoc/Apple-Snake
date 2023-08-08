using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonUp : MonoBehaviour
{
    public static ButtonUp instance;
    public WormMovement movement;
    public Button btn;
    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        movement = GameObject.FindGameObjectWithTag("Worm").GetComponent<WormMovement>();
        btn = GetComponent<Button>();
        btn.onClick.AddListener(movement.Up);
    }
}
