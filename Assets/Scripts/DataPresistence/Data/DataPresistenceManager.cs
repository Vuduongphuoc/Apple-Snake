using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class DataPresistenceManager : MonoBehaviour
{
    private GameData gameData;

    private List<IDataPresistence> dataPresistencesObjects ;

    public static DataPresistenceManager instance { get; private set; }

    private void Awake()
    {
        if(instance != null)
        {
            Debug.LogError("More than one Data Presistence Manager in the scene");
        }
        instance = this;
    }

    public void Start()
    {
        this.dataPresistencesObjects = FindAllDataPresistenceObjects();
        LoadGame();
    }

    public void NewGame()
    {
        this.gameData = new GameData();
    }

    public void LoadGame()
    {
        //TODO - Load any saved data from a file using the data handler

        //if no data can be loaded, initialize to a new game.

        if(this.gameData != null)
        {
            Debug.Log("No data was found. Initializing data to defaults.");
            NewGame();
        }

        foreach(IDataPresistence dataPresistenceObj in dataPresistencesObjects)
        {
            dataPresistenceObj.LoadData(gameData);
        }

        Debug.Log("Loaded level ID = " + gameData.LevelID);
    }

    public void SaveGame()
    {
        foreach(IDataPresistence dataPresistenceObj in dataPresistencesObjects)
        {
            dataPresistenceObj.SaveData(ref gameData);

        }
        Debug.Log("Saved level ID = " + gameData.LevelID);
    }

    private void OnApplicationQuit()
    {
        SaveGame();
    }

    private List<IDataPresistence> FindAllDataPresistenceObjects()
    {
        IEnumerable<IDataPresistence> dataPresistenceObjects = FindObjectsOfType<MonoBehaviour>()
            .OfType<IDataPresistence>();

        return new List<IDataPresistence>(dataPresistencesObjects);
    }
    
}
