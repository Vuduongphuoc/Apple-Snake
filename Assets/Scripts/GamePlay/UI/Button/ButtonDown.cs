using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonDown : MonoBehaviour
{
    
    public Button btn;
    public bool isActive;
    // Start is called before the first frame update
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

    // Update is called once per frame
    void Update()
    {
        if (isActive)
        {
            btn.onClick.AddListener(WormMovement.Instance.Down);
            isActive = false;
        }
    }
    
}
