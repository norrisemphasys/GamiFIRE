using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;


public class GameOverITView : BasicView
{
    public Button btnYes;
    public Button btnNo;

    public Button btnProceed;

    [SerializeField] GameObject winGO;
    [SerializeField] GameObject loseGO;

    public void Init()
    {

    }

    public void Show(bool win)
    {
        winGO.SetActive(win);
        loseGO.SetActive(!win);
    }
}
