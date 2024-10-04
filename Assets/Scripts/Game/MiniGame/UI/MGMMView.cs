using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;

public class MGMMView : BasicView
{
    public Button buttonPlay;
    [SerializeField] TextMeshProUGUI textLevel; 

    public void Init()
    {

    }

    public void SetText(int level)
    {
        textLevel.text = string.Format("{0}", level);
    }
}
