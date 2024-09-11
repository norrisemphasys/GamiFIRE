using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;

public class IslandUIView : BasicView
{
    public Button buttonRoll;

    [SerializeField] GameObject[] diceResultView;

    public void Init()
    {

    }

    public void SetDiceResult(int idx)
    {
        for(int i = 0; i < diceResultView.Length; i++)
            diceResultView[i].SetActive(false);
        diceResultView[idx-1].SetActive(true);
    }
}
