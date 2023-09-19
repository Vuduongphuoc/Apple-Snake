using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;


public class SaveManager : MonoBehaviour
{

    private string path = "";
    private string presistentPath = "";

    // Start is called before the first frame update
    private void setPaths()
    {
        path = Application.dataPath + Path.AltDirectorySeparatorChar + "SaveData.json";
        presistentPath = Application.persistentDataPath + Path.AltDirectorySeparatorChar + "SaveData.json";
    }
    private void SaveData()
    {
        string savePath = path;
        Debug.Log("Save data at: " + savePath);

        
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
