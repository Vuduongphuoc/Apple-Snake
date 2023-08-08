using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelScriptableObject : ScriptableObject
{
    public int levelIndex;
    public List<LevelData> FloorTiles;
    public List<LevelData> BorderTiles;
    public List<LevelData> UnitTiles;
    public List<LevelObjData> ObjectPrefabs;
}

[Serializable]
public class LevelData
{
    public Vector3Int position;
    public LevelTile Tile;
}
[Serializable]
public class LevelObjData
{
    public int ObjID;
    public Vector2 ObjPos;
}

