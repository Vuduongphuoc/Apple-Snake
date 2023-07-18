using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public LevelManager levelSelected;
    // Start is called before the first frame update
    void Start()
    {

        StartCoroutine(BeginLevel());
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void LoadLevel(int id = 0)
    {
        levelSelected.levelID = id;
        levelSelected.LoadLevel();
    }
    public void NextLevel()
    {
        levelSelected.levelID = levelSelected.levelID + 1;
        levelSelected.LoadLevel();
    }
    IEnumerator BeginLevel()
    {
        levelSelected.ClearLevel();
        yield return new WaitForSeconds(1f);
        LoadLevel();
    }

}
