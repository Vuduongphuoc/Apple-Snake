using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Map Object" , menuName = "New Object Factory")]

public class LevelObjFactory : ScriptableObject
{
    public List<ObjData> ObjectsData;
    private  Dictionary<int, string> ObjDic;

    public string ReturnPath(int PathID)
    {
        if(ObjDic == null)
        {
            ObjDic = new Dictionary<int, string>();
            
            for(int i = 0; i < ObjectsData.Count; i++)
            {
                ObjDic.Add(ObjectsData[i].ObjID, ObjectsData[i].ObjPath);
            }
        }
        
        return ObjDic[PathID];
    }
}

[System.Serializable]
public class ObjData
{
    public int ObjID;
    public string ObjPath;
    
}
