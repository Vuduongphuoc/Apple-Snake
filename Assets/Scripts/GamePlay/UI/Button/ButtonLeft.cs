using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonLeft : MonoBehaviour
{
    public static ButtonLeft instance;
    public WormMovement movement;
    public Button btn;
    // Start is called before the first frame update
    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        movement = GameObject.FindGameObjectWithTag("Worm").GetComponent<WormMovement>();
        btn = GetComponent<Button>();
        btn.onClick.AddListener(movement.Left);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
