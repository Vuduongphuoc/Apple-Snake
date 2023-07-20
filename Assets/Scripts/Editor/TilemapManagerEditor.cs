using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(TileMapManager))]
public class TilemapManagerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        var script = (TileMapManager)target;
        if(GUILayout.Button("Save Map"))
        {
            script.SaveMap();
        }
        if (GUILayout.Button("Clear Map"))
        {
            script.ClearMap();
        }
        if (GUILayout.Button("Load Map"))
        {
            script.LoadMap();
        }
    }
}
