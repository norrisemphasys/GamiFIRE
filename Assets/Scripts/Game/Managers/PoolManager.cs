using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class PoolManager : MonoSingleton<PoolManager>
{
    public ResourceManager resourceManager;

    public override void Init()
    {
        base.Init();
        GetInstance();
    }

    public void ResetAllObjectList(string name)
    {
        ObjectData data = resourceManager.GetObject(name);
        for (int i = 0; i < data.objectList.Count; i++)
        {
            data.objectList[i].SetActive(false);
            data.objectList[i].transform.SetParent(data.objectParent.transform);
        }
    }

    public void ResetObject(string name, GameObject obj)
    {
        ObjectData data = resourceManager.GetObject(name);
        for (int i = 0; i < data.objectList.Count; i++)
        {
            if (obj == data.objectList[i])
            {
                data.objectList[i].SetActive(false);
                data.objectList[i].transform.SetParent(data.objectParent.transform);

                break;
            }
        }
    }

    public void RemoveAddObjectToList(string name, GameObject go)
    {
        ObjectData data = resourceManager.GetObject(name);
        data.objectList.Remove(go);
        data.objectList.Insert(0, go);
    }

    public GameObject GetObject(string name)
    {
        ObjectData data = resourceManager.GetObject(name);
        GameObject g = data.objectList[0];

        if (!g.activeSelf)
        {
            g.SetActive(true);
            data.objectList.Remove(g);
            data.objectList.Add(g);
        }
        else
        {
            resourceManager.AddObject(name, true);
            g = data.objectList[0];

            g.SetActive(true);

            data.objectList.Remove(g);
            data.objectList.Add(g);
        }

        return g;
    }

    public GameObject GetObject(string name, Vector3 pos, Quaternion rot)
    {
        GameObject g = GetObject(name);

        g.transform.position = pos;
        g.transform.rotation = rot;

        return g;
    }

    public List<GameObject> GetObjectPool(string name)
    {
        ObjectData data = resourceManager.GetObject(name);
        return data.objectList;
    }

    void GetInstance()
    {
        if (resourceManager == null)
            resourceManager = ResourceManager.instance;
    }
}
