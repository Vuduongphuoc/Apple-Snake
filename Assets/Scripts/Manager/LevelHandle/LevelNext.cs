using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class LevelNext : MonoBehaviour
{
    public static LevelNext Instance;
    public GameObject levelLock, levelUnlock;
    public bool isCompleted;
    public bool unLock;
    // Start is called before the first frame update
    private void Awake()
    {
        Instance = this;
            
    }
    // Update is called once per frame
    void Update()
    {
        UpdateLevelStatus();
    }
    public void UpdateLevelStatus()
    {
        
    }
    public void LevelActived()
    {
        levelLock.SetActive(false);
        levelUnlock.SetActive(true);
        
    }
    public void LevelLock()
    {
        levelLock.SetActive(true);
        levelUnlock.SetActive(false);
       
    }

}
