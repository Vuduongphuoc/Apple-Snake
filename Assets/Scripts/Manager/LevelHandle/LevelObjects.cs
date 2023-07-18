using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class LevelObjects : MonoBehaviour
{
    public abstract int GetObjectID();
    public virtual void SetData(LevelObjData objdata)
    {

    } 
    public virtual LevelObjData GetData()
    {
        LevelObjData levelobj = new LevelObjData();
        levelobj.ObjID = GetObjectID();
        levelobj.ObjPos = transform.position;

        return levelobj;
    }
}
