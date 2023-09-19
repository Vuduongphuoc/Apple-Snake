using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveLoad : MonoBehaviour
{
    public PlayerData playerData = new PlayerData();

}

[System.Serializable]
public class PlayerData
{
    public List<LevelScriptableObject> lvlunlocked = new List<LevelScriptableObject>();
}
