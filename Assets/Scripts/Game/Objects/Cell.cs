using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell : MonoBehaviour
{
    [SerializeField] CellType type;
    [SerializeField] GameObject[] platforms;
    [SerializeField] GameObject initPlatform;

    public CellType Type { get { return type; } }

    public void SetCellType(int idx)
    {
        type = (CellType)idx;

        for (int i = 0; i < platforms.Length; i++)
            platforms[i].SetActive(i == idx);

        SetInitPlatform(false);
    }

    public void SetInitPlatform(bool init)
    {
        initPlatform.SetActive(init);

        if(init)
        {
            for (int i = 0; i < platforms.Length; i++)
                platforms[i].SetActive(false);
        }
    }
}
