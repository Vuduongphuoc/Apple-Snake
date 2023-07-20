using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEditor;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileMapManager : MonoBehaviour
{
    public LevelObjFactory ObjFactory;
    [SerializeField] private Tilemap _floorMap, _borderMap,_unitMap;
    [SerializeField] LevelObjects[] Objects;
    public int LevelID;

    public void SaveMap()
    {
        var newlevel = ScriptableObject.CreateInstance<TileMapScriptableObject>();

        newlevel.levelIndex = LevelID;
        newlevel.name = $"Level_{LevelID}";

        newlevel.FloorTiles = GetTilesFromMap(_floorMap).ToList();
        newlevel.BorderTiles = GetTilesFromMap(_borderMap).ToList();
        newlevel.UnitTiles = GetTilesFromMap(_unitMap).ToList();
        


        Objects = FindObjectsOfType(typeof(LevelObjects)).Cast<LevelObjects>().ToArray();

        for (int i = 0; i < Objects.Length; i++)
        {
            if (newlevel.ObjectTiles == null)
            {
                newlevel.ObjectTiles= new List<LevelObjData>();
            }
            newlevel.ObjectTiles.Add(Objects[i].GetData());
        }
        ScriptableUtility.SaveLevelFile(newlevel);

        //newlevel.GroundTiles;
        IEnumerable<LevelData> GetTilesFromMap(Tilemap map)
        {
            foreach(var pos in map.cellBounds.allPositionsWithin)
            {
                if (map.HasTile(pos))
                {
                   var levelTile = map.GetTile<LevelTile>(pos);
                    yield return new LevelData()
                    {
                        position = pos,
                        Tile = levelTile
                    };
                }
            }
        }
    }
    public void LoadMap()
    {
        var level = Resources.Load<TileMapScriptableObject>($"Levels/Level_{LevelID}");
        if(level == null)
        {
            Debug.LogError($"Level_{LevelID} does not exist");
            return;
        }
        ClearMap();

        foreach (var saveTile in level.FloorTiles)
        {
            switch (saveTile.Tile.type)
            {
                case TileType.Floor:
                    SetTile(_floorMap, saveTile);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
        //foreach (var saveTile in level.BorderTiles)
        //{
        //    switch (saveTile.Tile.type)
        //    {
        //        case TileType.Border:
        //            SetTile(_borderMap, saveTile);
        //            break;
        //        default:
        //            throw new ArgumentOutOfRangeException();
        //    }
        //}
        //foreach (var saveTile in level.UnitTiles)
        //{
        //    switch (saveTile.Tile.type)
        //    {
        //        case TileType.Apple:
        //            SetTile(_unitMap, saveTile);
        //            break;
        //        default:
        //            throw new ArgumentOutOfRangeException();
        //    }
        //}
        foreach (var obj in level.ObjectTiles)
        {
            var levelObj = Resources.Load<LevelObjects>(ConfigFile.Instance.levelFactory.ReturnPath(obj.ObjID));
            var createObj = Instantiate<LevelObjects>(levelObj, obj.ObjPos, Quaternion.identity);
            createObj.transform.SetParent(this.transform);
            createObj.SetData(obj);
        }
        void SetTile(Tilemap map, LevelData tile)
        {
            map.SetTile(tile.position, tile.Tile);
        }
    }
    public void ClearMap()
    {
        var maps = FindObjectsOfType<Tilemap>();
        foreach(var tilemap in maps)
        {
            tilemap.ClearAllTiles();
        }
        var clearObj = FindObjectsOfType<LevelObjects>();
        foreach (LevelObjects i in clearObj)
        {
            i.gameObject.SetActive(false);
       
        }
    }
    public void ResumeLevel()
    {
        Time.timeScale = 1f;
    }
    public void PauseLevel()
    {
        Time.timeScale = 0f;
    }
    public void BackMenu()
    {

    }
}

#if UNITY_EDITOR

public static class ScriptableUtility
{
    public static void SaveLevelFile(TileMapScriptableObject level)
    {
        AssetDatabase.CreateAsset(level,$"Assets/Resources/Levels/{level.name}.asset");
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
    }
}

#endif

public struct Levels
{
    public int levelIndex;
    public List<LevelData> FloorTiles;
    public List<LevelData> BorderTiles;
    public List<LevelData> UnitTiles;

    public string Serialize()
    {
        var builder = new StringBuilder();
        builder.Append("g[");
        foreach(var floorTile in FloorTiles)
        {
            builder.Append($"{(int)floorTile.Tile.type}({floorTile.position.x}.{floorTile.position.y})");
        }
        builder.Append(']');
        return builder.ToString();
    }
}