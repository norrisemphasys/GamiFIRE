using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;

public class BuildingUIView : BasicView
{
    public Button buttonClose;
    [SerializeField] TextMeshProUGUI textCoin;

    [SerializeField] GameObject[] buildingList;

    public void Init()
    {

    }

    public void SetCoin(int coin)
    {
        textCoin.text = coin.ToString();
    }

    public void ShowBuildingList(int idx)
    {
        for (int i = 0; i < buildingList.Length; i++)
            buildingList[i].SetActive(i == idx);
    }
}
