using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;

public class GameIntroView : BasicView
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
            if(goImageTutorial.Length > 0)
                goImageTutorial[i].SetActive(show);
            if (goTextInfo.Length > 0)
                goTextInfo[i].SetActive(show);
            if (goPagination.Length > 0)
                goPagination[i].SetActive(show);
        }
    }
}
