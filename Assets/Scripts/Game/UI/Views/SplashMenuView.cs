using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;
using UnityEngine.Events;

public class SplashMenuView : BasicView
{
    [SerializeField] Image imgLogo;
    [SerializeField] RectTransform rectText;

    public void Init()
    {
        imgLogo.DOFade(0f, 0f);
        rectText.DOScaleY(0f, 0f);
    }

    public void PlaySplashSequence(UnityAction callback = null)
    {
        Sequence seq = DOTween.Sequence();

        seq.Append(imgLogo.DOFade(1f, 4f));
        seq.AppendInterval(1f);
        seq.Append(imgLogo.DOFade(0f, 1f));
        seq.Append(rectText.DOScaleY(1, 0.2f));
        seq.AppendInterval(5f);
        seq.Append(rectText.DOScaleY(0f, 0.2f));
        seq.AppendCallback(()=> 
        {
            callback?.Invoke();
        });
    }

}
