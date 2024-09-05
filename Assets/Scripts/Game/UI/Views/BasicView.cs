using UnityEngine;
using System.Collections;
using System;
using DG.Tweening;

public class BasicView : MonoBehaviour
{
    protected CanvasGroup canvasGroup;
    protected RectTransform panelRect;

    [SerializeField] bool scaleAnimation = false;
    [SerializeField] RectTransform popupPanel;

    void Awake()
    {
        canvasGroup = GetComponentInChildren<CanvasGroup>();
        panelRect = canvasGroup.GetComponent<RectTransform>();

        canvasGroup.gameObject.SetActive(false);

        if (scaleAnimation)
            popupPanel.DOScale(0f, 0f).SetUpdate(true);
    }
    
    public virtual void Reset()
    {

    }

    public virtual void Show(Action callback = null)
    {
        canvasGroup.DOKill();
        panelRect.DOKill();

        canvasGroup.gameObject.SetActive(true);
        canvasGroup.interactable = true;
        canvasGroup.blocksRaycasts = true;

        if (scaleAnimation)
        {
            popupPanel.DOScale(1, 0.2f).SetUpdate(true);
        }

        canvasGroup.DOFade(1f, 0.2f)
            .OnComplete(() =>
            {
                    

                if (callback != null)
                    callback();
            }).SetUpdate(true);
    }

    public virtual void Hide(Action callback = null)
    {
        canvasGroup.DOKill();
        panelRect.DOKill();

        canvasGroup.interactable = false;

        if(scaleAnimation)
        {
            popupPanel.DOScale(0, 0.2f).SetUpdate(true);
        }

        canvasGroup.DOFade(0f, 0.2f)
            .OnComplete(() =>
            {
                canvasGroup.blocksRaycasts = false;
                canvasGroup.gameObject.SetActive(false);

                if (callback != null)
                    callback();
            }).SetUpdate(true);
    }
}
