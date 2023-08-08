using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelNext : MonoBehaviour
{
    public Sprite imglock, unlock, completeimg;
    Sprite currentSprite;
    // Start is called before the first frame update
    void Start()
    {
        currentSprite = GetComponent<Image>().sprite;
        currentSprite = imglock;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

}
