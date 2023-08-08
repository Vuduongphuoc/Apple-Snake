using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public TileMapManager tileMapManager;
    public GameObject WinPanel;

    // Start is called before the first frame update
    void Start()
    {
        WinPanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(Worm.Instance.isWin == true)
        {
            Win();
        }
    }
    public void LoadLevel(int id)
    {

        StartCoroutine(BeginLevel());
        tileMapManager.LevelID = id;
        tileMapManager.LoadMap();
    }
    public void NextLevel()
    {
        StartCoroutine(BeginLevel());
        tileMapManager.LevelID = tileMapManager.LevelID + 1;
        tileMapManager.LoadMap();
    }
    public void ResetLevel()
    {
        tileMapManager.LoadMap();
    }
    public void ClearMap()
    {
        tileMapManager.ClearMap();
        GamePlayUI.instance.HomeButton();
    }
    IEnumerator BeginLevel()
    {
        tileMapManager.ClearMap();
        tileMapManager.ResumeLevel();
        GamePlayUI.instance.LoadGamePlay();
        yield return new WaitForSeconds(1f);
    }
    public void Win()
    {
        WinPanel.SetActive (true);

    }

}
