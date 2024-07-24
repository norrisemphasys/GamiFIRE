using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class StageView : BasicView
{
    public Button btnBack;

    public Button[] btnIslands;

    public override void Show(Action callback = null)
    {
        base.Show(()=>
        {
            for(int i = 0; i < btnIslands.Length; i++)
                btnIslands[i].transform.DOScale(Vector3.one, 0.2f).SetDelay(i * 0.2f);
        });
    }

    public void Init()
    {
        foreach (Button b in btnIslands)
            b.transform.localScale = Vector3.zero;
    }
}
