using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public TileMapManager tileMapManager;
    // Start is called before the first frame update
    void Start()
    {

        StartCoroutine(BeginLevel());
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void LoadLevel(int id = 0)
    {
        tileMapManager.LevelID = id;
        tileMapManager.LoadMap();
    }
    public void NextLevel()
    {
        tileMapManager.LevelID = tileMapManager.LevelID + 1;
        tileMapManager.LoadMap();
    }
    IEnumerator BeginLevel()
    {
        tileMapManager.ClearMap();
        yield return new WaitForSeconds(1f);
        LoadLevel();
    }

}
