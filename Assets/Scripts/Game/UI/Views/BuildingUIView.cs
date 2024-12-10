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

    public void Init()
    {

    }

    public void SetCoin(int coin)
    {
        textCoin.text = coin.ToString();
    }
}
