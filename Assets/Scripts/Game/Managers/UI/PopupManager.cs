using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.Events;

public class PopupManager : MonoSingleton<PopupManager>
{
    [SerializeField] CanvasGroup canvasGroup;
    [SerializeField] Transform popupViewParent;

    public Queue<PopupView> popupViews = new Queue<PopupView>();

    private bool _isPopupShowing = false;

    private void Update()
    {
        PopupUpdate();

#if UNITY_EDITOR
        if (Input.GetKeyDown(KeyCode.P))
            ShowDefaultPopup();
#endif
    }

    void AddToPopupQueue(PopupView view)
    {
        popupViews.Enqueue(view);
    }

    void ExecuteQueue()
    {
        PopupView view = popupViews.Dequeue();
        view.Show(true);

        StartCoroutine(ExecutQueueEnum());
    }

    IEnumerator ExecutQueueEnum()
    {
        _isPopupShowing = true;

        yield return new WaitUntil(() => !_isPopupShowing);

        if (popupViews.Count <= 0)
            ShowPopup(false);
    }

    void PopupUpdate()
    {
        if (_isPopupShowing || popupViews.Count <= 0)
            return;

        ExecuteQueue();
    }

    #region Create Popup 
    public void ShowPopup(bool show, UnityAction onShow = null)
    {
        bool isAlreadyShowing = canvasGroup.alpha >= 1;

        if(!show)
        {
            canvasGroup.blocksRaycasts = false;
            canvasGroup.interactable = false;
        }

        if(isAlreadyShowing && show)
        {
            onShow?.Invoke();
        }
        else
        {
            canvasGroup.DOFade(show ? 1 : 0, 0.2f).OnComplete(() =>
            {
                if (show)
                {
                    canvasGroup.blocksRaycasts = true;
                    canvasGroup.interactable = true;

                    onShow?.Invoke();
                }
            });
        }
    }

    public void ShowPopup(PopupData data)
    {
        ShowPopup(true, () =>
        {
            data.OnClosePopup += ()=> { _isPopupShowing = false; };

            PopupView view = PopupView.Create(popupViewParent, data);
            AddToPopupQueue(view);
        });
    }

    public void ShowDefaultPopup()
    {
        ShowPopup(true, () =>
        {
            PopupData data = PopupMessage.CreatePopupData("Test", "This is a test description", 
                onClosePopup: ()=> { _isPopupShowing = false; });
            PopupView view = PopupView.Create(popupViewParent, data);
            AddToPopupQueue(view);
        });
    }

    #endregion
}
