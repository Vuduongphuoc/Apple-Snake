using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Win : MonoBehaviour
{
    public GameObject WinPanel;
    // Start is called before the first frame update
    void Start()
    {
        WinPanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Worm")
        {

            StartCoroutine(WinPopup());
        }
    }
    IEnumerator WinPopup()
    {
        Time.timeScale = 0f;
        yield return new WaitForSeconds(2f);
        WinPanel.SetActive(true);
    }
}
