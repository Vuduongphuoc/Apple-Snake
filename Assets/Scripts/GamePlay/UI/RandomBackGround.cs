using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RandomBackGround : MonoBehaviour
{
    public static RandomBackGround instance;
    public Image BGs;
    public Sprite[] sprs;
    int x;
    // Start is called before the first frame update
    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        x = Random.Range(0, sprs.Length);
        BGs.sprite = sprs[x];
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
