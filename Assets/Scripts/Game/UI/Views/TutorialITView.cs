using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;

public class TutorialITView : BasicView
{
    public Button btnClose;
    public Button btnPrevious;
    public Button btnNext;

    public Button buttonEasy;
    public Button buttonMedium;
    public Button buttonHard;

    public GameObject[] goImageTutorial;
    public GameObject[] goTextInfo;
    public GameObject[] goPagination;

    [SerializeField] GameObject popupDifficultyGO;
    [SerializeField] GameObject popupTutorialGO;

    private int steps = 3;

    public void Init()
    {
        ShowDifficulty(false);
    }

    public void ShowTutorial(int index)
    {
        for(int i = 0; i < steps; i ++)
        {
            bool show = index == i;
            goImageTutorial[i].SetActive(show);
            goTextInfo[i].SetActive(show);
            goPagination[i].SetActive(show);
        }
    }

    public void ShowDifficulty(bool show) 
    {
        popupDifficultyGO.SetActive(show);
        popupTutorialGO.SetActive(!show);

        btnPrevious.gameObject.SetActive(!show);
        btnNext.gameObject.SetActive(!show);
    }
}
