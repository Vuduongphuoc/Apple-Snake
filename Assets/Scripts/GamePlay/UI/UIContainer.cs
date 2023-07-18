using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIContainer : MonoBehaviour
{
    public GameManager gameManager;
    public Grid grid;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void LevelMenu()
    {

    }
    public void Music()
    {

    }
    public void Sound()
    {

    }
    public void ResetLevel()
    {
        gameManager.LoadLevel();
    }
}
