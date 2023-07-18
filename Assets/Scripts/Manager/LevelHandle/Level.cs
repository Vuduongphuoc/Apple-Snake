using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : ScriptableObject
{
    public int LevelID;
    public List<LevelObjData> LevelObj;
}

[System.Serializable]
public class LevelObjData
{
    public int ObjID;
    public Vector2 ObjPos;
}