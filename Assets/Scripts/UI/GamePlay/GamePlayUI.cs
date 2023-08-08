using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class GamePlayUI : MonoBehaviour
{
    public static GamePlayUI instance;
    public GameObject gameplay;
    public GameObject gameplayUIBtns;
    public GameObject playerController;
   
    // Start is called before the first frame update
    private void Awake()
    {
        instance = this;

    }
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void LoadGamePlay()
    {
        gameplay.SetActive(true);
        playerController.SetActive(true);
        gameplayUIBtns.SetActive(true);
    }
    public void HomeButton()
    {
        gameplay.SetActive(false);
        playerController.SetActive(false);
        gameplayUIBtns.SetActive(false);
    }


}
