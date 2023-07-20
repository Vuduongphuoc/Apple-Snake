using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConfigFile : MonoBehaviour
{
    public static ConfigFile Instance;

    [SerializeField] private LevelObjFactory LevelObjFactoryConfig;

    public LevelObjFactory levelFactory { get => LevelObjFactoryConfig; }

    private void Awake()
    {
       if(Instance == null)
        {
            Instance = this;
        }
    }
}
