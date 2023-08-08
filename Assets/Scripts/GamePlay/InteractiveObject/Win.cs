using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Win : MonoBehaviour
{

    public GameObject Mask;
    // Start is called before the first frame update
    private void OnEnable()
    {
        Mask.SetActive(false);
    }
    // Update is called once per frame
    void Update()
    {
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.gameObject.TryGetComponent(out Worm hot))
        {
            Debug.Log("You Win");
            Worm.Instance.isWin = true;
            Mask.SetActive(true);
        }
    }
    //IEnumerator WinPopup()
    //{
    //    Time.timeScale = 0f;
    //    yield return new WaitForSeconds(2f);
    //    WinPanel.SetActive(true);
    //}
}
