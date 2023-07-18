using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelSelector : MonoBehaviour
{
    public static int BtnLevelID;
    public int BtnID;
    public Text BtnText;

    void Start()
    {
        BtnText.text = BtnID.ToString();
    }
}
