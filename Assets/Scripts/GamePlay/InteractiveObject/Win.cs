using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
public class Win : MonoBehaviour
{

    //public Animation anim;

    // Start is called before the first frame update

    // Update is called once per frame

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent(out Worm hot))
        {
            AudioManager.instance.PlaySFX(AudioManager.instance.win);
            StartCoroutine(GameManager.Instance.Win());
            Worm.Instance.isWin = true;
            if (Vector2.Distance(WormTail.Instance.gameObject.transform.position, transform.position) > 0.01f)
            {
                MoveToPortal();

            }
        }
    }

    void MoveToPortal()
    {

        Worm.Instance.transform.position = Vector2.Lerp(Worm.Instance.transform.position, transform.position, 1f * Time.deltaTime);
        foreach (var p in Worm.Instance.DropDownParts)
        {

            p.GetComponent<SpriteRenderer>().enabled = false;
            p.GetComponent<BoxCollider2D>().enabled = false;
        
        }
    }
}
