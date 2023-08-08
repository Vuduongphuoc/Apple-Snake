using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConfigFile : MonoBehaviour
{
    public static ConfigFile Instance;
    [Header("Factory Config")]
    [SerializeField] private LevelObjFactory objFactory;

    
    public LevelObjFactory GameObjFactory { get => objFactory; }

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        objFactory = Resources.Load("LevelFactory", typeof(ScriptableObject)) as LevelObjFactory;

    }
}
