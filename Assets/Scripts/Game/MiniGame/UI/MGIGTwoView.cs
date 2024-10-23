using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;

public class MGIGTwoView : BasicView
{
    [SerializeField] RectTransform rectFast;

    [SerializeField] TextMeshProUGUI textFast;
    [SerializeField] TextMeshProUGUI textPoints;

    [SerializeField] GameObject[] life;

    public void Init() 
    {
        rectFast.DOScaleY(0f, 0f).SetUpdate(true);
    }

    public void ShowFastPanel(bool show, float delay = 0, UnityAction callback = null)
    {
        if(show)
        {
            rectFast.DOScaleY(1f, 0.2f).SetDelay(delay).OnComplete(()=> 
            {
                callback?.Invoke();
            }).SetUpdate(true);
        }
        else
        {
            rectFast.DOScaleY(0f, 0.2f).SetDelay(delay).OnComplete(() =>
            {
                callback?.Invoke();
            }).SetUpdate(true);
        }
    }

    public void SetTextFast(int speed) 
    {
        textFast.text = string.Format("FAST <size=80> x{0}", speed);
    }

    public void SetPoints(int points)
    {
        textPoints.text = string.Format("{0}<size=70>pts", points);
    }

    public void LifeRemoved(int idx)
    {
        for (int i = 0; i < life.Length; i++)
            life[i].SetActive(idx < i);
    }
}
