//using NaughtyAttributes;
//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using System.Linq;
//using UnityEditor;
//using System;
//using UnityEditor.VersionControl;

//public class LevelManager : MonoBehaviour
//{
//    public LevelObjFactory ObjFactory;
//    [SerializeField] LevelObjects[] Objects;
//    public int levelID;

//    [Button]
//    public void SaveLevel()
//    {
//        var newlevel = ScriptableObject.CreateInstance<Level>();
//        newlevel.LevelID = levelID;
//        newlevel.name = $"Level_{levelID}";

//        Objects = FindObjectsOfType(typeof(LevelObjects)).Cast<LevelObjects>().ToArray();

//        for (int i = 0; i < Objects.Length; i++)
//        {
//            if (newlevel.LevelObj == null)
//            {
//                newlevel.LevelObj = new List<LevelObjData>();
//            }
//            newlevel.LevelObj.Add(Objects[i].GetData());
//        }
//        ScriptableObjectUtility.SaveLevelFile(newlevel);
//    }

//    public void LoadLevel()
//    {
//        var loadlevel = Resources.Load<Level>("Levels/Level_" + levelID);
//        if (loadlevel == null)
//        {
//            Debug.Log("Don't have any Levels");
//        }

//        foreach (var obj in loadlevel.LevelObj)
//        {
//            var levelObj = Resources.Load<LevelObjects>(ConfigFile.Instance.levelFactory.ReturnPath(obj.ObjID));
//            var createObj = Instantiate<LevelObjects>(levelObj, obj.ObjPos, Quaternion.identity);
//            createObj.transform.SetParent(this.transform);
//            createObj.SetData(obj);
//        }
//    }
//    public void ResumeLevel()
//    {
//        Time.timeScale = 1f;
//    }
//    public void PauseLevel()
//    {
//        Time.timeScale = 0f;
//    }
//    public void BackMenu()
//    {
        
//    }
//    public void ClearLevel()
//    {
//        var clearObj = FindObjectsOfType<LevelObjects>();
//        foreach(LevelObjects i in clearObj)
//        {
//            i.gameObject.SetActive(false);
//            if (!i.gameObject.activeSelf)
//            {
//                Destroy(i.gameObject);
//            }
//        }
//    }
//}
//public static class ScriptableObjectUtility
//{
//    public static void SaveLevelFile(ScriptableObject level)
//    {
//        AssetDatabase.CreateAsset(level, $"Assets/Resources/Levels/{level.name}.asset");
//        AssetDatabase.SaveAssets();
//        AssetDatabase.Refresh();
//    }
//}

