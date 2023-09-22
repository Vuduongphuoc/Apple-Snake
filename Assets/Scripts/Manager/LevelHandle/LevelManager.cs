using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEditor;
using System;
using TMPro;
using UnityEngine.UI;
public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance;
    public List<LevelScriptableObject> lvls = new List<LevelScriptableObject>();
    public Transform btnPos;
    public GameObject btnPrefab;
    public int indexCurrentlvl;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }
    }
    private void Reset()
    {
        lvls.Clear();
        foreach(LevelScriptableObject data in Resources.LoadAll<LevelScriptableObject>("Levels"))
        {
            lvls.Add(data);
        }
        lvls.OrderBy(x => x.name);
        btnPos = GameObject.FindGameObjectWithTag("LevelContainer").transform;
        btnPrefab = Resources.Load("Prefabs/Buttons/Button") as GameObject;
    }
    private void Start()
    {
        //GameManager.Instance.LoadData();
        LoadButtonWhenStart();
        
    }
    private void setIndexCurrentLevel(int index)
    {
        indexCurrentlvl = index;
    }
    public void LoadButtonWhenStart()
    {
        foreach (var lvl in lvls)
        {
            GameObject btnlvl = Instantiate(btnPrefab, btnPos);
            btnlvl.GetComponent<ButtonLevel>().levelScriptableObject = lvl;
            btnlvl.GetComponentInChildren<TextMeshProUGUI>().text = lvl.name;
            btnlvl.GetComponent<Button>().onClick.AddListener(delegate { GameManager.Instance.LoadLevel(lvl.levelIndex); });
            btnlvl.GetComponent<Button>().onClick.AddListener(delegate { setIndexCurrentLevel(lvl.levelIndex); });

            //if (GameManager.Instance.CheckLevelIsUnlock(lvl))
            //{
            //    btnlvl.GetComponent<Image>().sprite = btnlvl.GetComponent<ButtonLevel>().unLockImg;
            //    btnlvl.GetComponent<Button>().interactable = true;
            //}
            //else
            //{
            //    btnlvl.GetComponent<Image>().sprite = btnlvl.GetComponent<ButtonLevel>().lockImg;
            //    btnlvl.GetComponent<Button>().interactable = false;
            //}
        }
    }
    public void LoadButtonWhilePlay()
    {
        foreach(Transform tp in btnPos)
        {

            if (GameManager.Instance.CheckLevelIsUnlock(tp.GetComponent<ButtonLevel>().levelScriptableObject))
            {
                tp.GetComponent<Image>().sprite = tp.GetComponent<ButtonLevel>().unLockImg;
                tp.GetComponent<Button>().interactable = true;
            }
            else
            {
                tp.GetComponent<Image>().sprite = tp.GetComponent<ButtonLevel>().lockImg;
                tp.GetComponent<Button>().interactable = false;
            }
        }
    }
    
}

