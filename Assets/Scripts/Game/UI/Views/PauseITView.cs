using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;

public class PauseITView : BasicView
{
    public Button btnContinue;
    public Button btnRestart;
    public Button btnGiveup;

    [SerializeField] TextMeshProUGUI textIslandName;

    public void Init()
    {

    }

    public void SetIslandName(string name)
    {
        textIslandName.text = name;
    }
}
