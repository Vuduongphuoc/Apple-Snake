using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class GamePlayUI : MonoBehaviour
{
    public static GamePlayUI instance;

    public GameObject Level1;
    //public GameObject Level2;
    //public GameObject Level3;
    //public GameObject Level4;
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
        HomeButton();
    }
    public void LoadGamePlay()
    {
        Level1.SetActive(false);
        gameplay.SetActive(true);
        playerController.SetActive(true);
        gameplayUIBtns.SetActive(true);
    }
    public void HomeButton()
    {
        GameManager.Instance.WinPanel.SetActive(false);
        gameplay.SetActive(false);
        playerController.SetActive(false);
        gameplayUIBtns.SetActive(false);
    }


}
