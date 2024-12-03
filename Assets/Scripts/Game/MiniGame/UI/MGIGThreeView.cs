using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;

public class MGIGThreeView : BasicView
{
    [SerializeField] TextMeshProUGUI textPoints;

    public void Init()
    {
        
    }
    public void SetPoints(int points)
    {
        textPoints.text = string.Format("{0}<size=70>pts", points);
    }
}
