using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;

public class PortTutorialView : BasicView
{
    public Button btnClose;
    public Button btnPrevious;
    public Button btnNext;

    public GameObject[] goImageTutorial;
    public GameObject[] goTextInfo;
    public GameObject[] goPagination;

    private int steps = 4;

    public void Init()
    {
        steps = goTextInfo.Length;
    }

    public void ShowTutorial(int index)
    {
        for (int i = 0; i < steps; i++)
        {
            bool show = index == i;
            goImageTutorial[i].SetActive(show);
            goTextInfo[i].SetActive(show);
            goPagination[i].SetActive(show);
        }
    }
}
