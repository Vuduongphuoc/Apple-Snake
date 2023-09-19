using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonUp : MonoBehaviour
{
    public Button btn;
    public bool isActive;
    private void OnEnable()
    {
        
       isActive = true;
    }
    private void OnDisable()
    {
        btn.onClick.RemoveAllListeners();
    }
    void Start()
    {
        btn = GetComponent<Button>();
    }
    private void Update()
    {
        if (isActive)
        {
           
            btn.onClick.AddListener(WormMovement.Instance.Up);
            isActive = false;
        }
    }
}
