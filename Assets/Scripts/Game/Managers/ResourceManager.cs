using System.Collections.Generic;
using UnityEngine;

public class ResourceManager : MonoSingleton<ResourceManager>
{
    [Header("Island Object Pool")]
    public ObjectData[] objectIslandPool;

    [Header("Student Island Object Pool")]
    public ObjectData[] studentObjectIslandPool;

    [Header("UI Pool")]
    public ObjectData[] uiDataPool;

    [Header("Effect Pool")]
    public ObjectData[] effectDataPool;

    private Dictionary<string, ObjectData> objectDataDictionary =
        new Dictionary<string, ObjectData>();

    public override void Init()
    {
/*          InitializeData(objectDefaultDataPool);
        InitializeData(uiDataPool);
        InitializeData(effectDataPool);*/
    }

    public void IntializeIslandObjectData()
    {
        InitializeData(objectIslandPool);
    }
    public void IntializeStudentIslandObjectData()
    {
        InitializeData(studentObjectIslandPool);
    }

    public ObjectData GetObject(string name)
    {
        if (objectDataDictionary.ContainsKey(name))
            return objectDataDictionary[name];

        return null;
    }

    public void AddObject(string name, bool isNew = false)
    {
        ObjectData data = GetObject(name);
        if (data.shouldExpand)
        {
            data.AddObject(isNew);
        }
    }

    void InitializeData(ObjectData[] dataPool)
    {
        for (int i = 0; i < dataPool.Length; i++)
        {
            ObjectData data = dataPool[i];
            data.objectList.Clear();

            GameObject parent = new GameObject(data.name);
            data.objectParent = parent;

            for (int j = 0; j < data.count; j++)
            {
                data.AddObject();
            }

            if (!objectDataDictionary.ContainsKey(data.name))
                objectDataDictionary.Add(data.name, data);
        }
    }
}

[System.Serializable]
public class ObjectData
{
    public string name;
    public int count;

    public GameObject dataObject;
    public bool shouldExpand;

    [HideInInspector]
    public GameObject objectParent;

    [HideInInspector]
    public List<GameObject> objectList = new List<GameObject>();

    public void AddObject(bool isNew = false)
    {
        GameObject g = GameObject.Instantiate(dataObject);

        g.transform.SetParent(objectParent.transform);
        g.transform.SetAsFirstSibling();
        g.transform.position = Vector3.zero;
        g.SetActive(false);

        if (isNew)
            objectList.Insert(0, g);
        else
            objectList.Add(g);
    }
}

