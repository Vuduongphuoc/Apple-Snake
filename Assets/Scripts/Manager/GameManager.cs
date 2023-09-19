using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public TileMapManager tileMapManager;
    public GameObject WinPanel;
    private PlayerData playerData = new PlayerData();
    // Start is called before the first frame update
    private void Awake()
    {
       if(Instance == null)
       {
            Instance = this;
       }
    }
    void Start()
    {
        WinPanel.SetActive(false);
        
    }
    // Update is called once per frame
    public void QuickPlay()
    {
        
        foreach(var lvl in playerData.lvlunlocked)
        {
            if(lvl.levelIndex == playerData.lvlunlocked.Last().levelIndex)
            {
                LoadLevel(lvl.levelIndex);
            }
        }
    }

    //code luu sang file json day
    public void LoadData()
    {
        string filePath = Application.persistentDataPath + "/PlayerData.json";
        Debug.Log(filePath);
        if (!File.Exists(filePath))
        {
            Initial();
        }
        else 
        { 
            string data = File.ReadAllText(filePath);
            if (data == "")
            {
                Initial();
            }
            else
            {
                playerData = JsonUtility.FromJson<PlayerData>(data);
            }
        }   
    }
    public void DeleteData()
    {
        string filePath = Application.persistentDataPath + "/PlayerData.json";
        if(File.Exists(filePath))
        {
            File.Delete(filePath);
            
        }
    }
    public void SaveData()
    {
        string data = JsonUtility.ToJson(playerData);
        string filePath = Application.persistentDataPath + "/PlayerData.json";
        
        File.WriteAllText(filePath, data);
    }
    //end here

    public void Initial()
    {
        playerData.lvlunlocked.Add(LevelManager.Instance.lvls[0]);
        SaveData();
    }
    public void LoadLevel(int id)
    {
        StartCoroutine(BeginLevel());
        tileMapManager.LevelID = id;
        tileMapManager.ClearMap();
        tileMapManager.LoadMap();
    }
    //public void LoadLevelChallenge(int id)
    //{

    //}

    public void NextLevel()
    {
        //START FROM ID 0 - > 999
        StartCoroutine(BeginLevel());
        tileMapManager.LevelID = tileMapManager.LevelID + 1;
        tileMapManager.LoadMap();
        LevelManager.Instance.LoadButtonWhilePlay();
        WinPanel.SetActive(false);
    }
    //public void NextLevelChallenge()
    //{
    //   // START FROM ID 1000 -> 10000   
    //}
    public void ResetLevel()
    {
        StartCoroutine(BeginLevel());
        tileMapManager.LoadMap();
        WinPanel.SetActive(false);
    }
    public void ClearMap()
    {
        tileMapManager.ClearMap();
        GamePlayUI.instance.HomeButton();
        LevelManager.Instance.LoadButtonWhilePlay();
    }

    public bool CheckLevelIsUnlock(LevelScriptableObject lvl)
    {
        foreach(var level in playerData.lvlunlocked)
        {
            if(level.levelIndex == lvl.levelIndex)
            {
                return true;
            }
        }
        return false;
    }
    IEnumerator BeginLevel()
    {
        GamePlayUI.instance.HomeButton();
        tileMapManager.ClearMap();
        tileMapManager.ResumeLevel();
        GamePlayUI.instance.LoadGamePlay();
        yield return new WaitForSeconds(1f);
    }

    public IEnumerator Win()
    {
        yield return new WaitForSeconds(0.7f);
        WinPanel.SetActive(true);
        if (!CheckLevelIsUnlock(LevelManager.Instance.lvls[LevelManager.Instance.indexCurrentlvl]))
        {
            playerData.lvlunlocked.Add(LevelManager.Instance.lvls[LevelManager.Instance.indexCurrentlvl]);
        }
        SaveData(); 
    }

}
