using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;

public class MGTView : BasicView
{
    public Button buttonNext;
    public Button buttonPrev;
    public Button buttonClose;

    [SerializeField] MGTIView[] views;
    [SerializeField] GameObject[] pagination;

    public void Init()
    {

    }

    public void SetMGTView(int idx)
    {
        for (int i = 0; i < views.Length; i++)
            views[i].gameObject.SetActive(i == idx);
    }

    public void ShowStep(int idx, int stepIdx)
    {
        views[idx].ShowStep(stepIdx);

        for (int i = 0; i < pagination.Length; i++)
            pagination[i].SetActive(i == stepIdx);
    }
}
