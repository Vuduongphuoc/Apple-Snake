using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.Composites;

public class TabGroup : MonoBehaviour
{
    public List<TabButton> tabButtons;
    public Sprite tabIdle;
    public Sprite tabHover;
    public Sprite tabActive;
    public TabButton tabSelected;
    public List<GameObject> objectToSwap;

    public void CheckButton(TabButton button)
    {
        if(tabButtons == null)
        {
            tabButtons = new List<TabButton>();
        }
        tabButtons.Add(button);
    }
    public void OnTabEnter(TabButton button)
    {
        ResetTab();
        if(tabSelected == null && button != tabSelected)
        {
            button.background.sprite = tabHover;
        }
    }
    public void OnTabExit(TabButton button)
    {
        ResetTab();
    }
    public void OnTabSelected(TabButton button)
    {
        if(tabSelected != null)
        {
            tabSelected.Deselected();
        }
        tabSelected = button;
        tabSelected.Selected();
        ResetTab();
        button.background.sprite = tabActive;
        int index = button.transform.GetSiblingIndex();
        for(int i =0; i < tabButtons.Count; i++)
        {
            if(i == index)
            {
                objectToSwap[i].SetActive(true);
            }
            else
            {
                objectToSwap[i].SetActive(false);
            }
        }
    }
    public void ResetTab()
    {
        foreach(TabButton button in tabButtons)
        {
            if(tabSelected != null && button == tabSelected)
            {
                continue;
            }
            button.background.sprite = tabIdle;
        }
    }
}
