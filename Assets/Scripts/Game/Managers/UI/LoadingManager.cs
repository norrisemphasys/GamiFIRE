using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.Events;

public class LoadingManager : MonoSingleton<LoadingManager>
{
    [SerializeField] CanvasGroup loadingCanvasGroup;
    [SerializeField] CanvasGroup fadeCanvasGroup;
    [SerializeField] Transform loader;

    [SerializeField] bool useTestMode = false;

    private void Awake()
    {
        ShowLoadingCanvas(false);
        ShowFadeCanvas(false);
    }

    private void Update()
    {
#if UNITY_EDITOR
        if(useTestMode)
        {
            if (Input.GetKeyDown(KeyCode.T))
                ShowLoader(true);
            if (Input.GetKeyDown(KeyCode.S))
                ShowLoader(false);
        }
#endif
    }

    public void ShowLoader(bool show, UnityAction callback = null)
    {
        if(show)
        {
            ShowLoadingCanvas(true);
            ShowFadeCanvas(false);

            loader.DOKill();
            loader.DORotate(Vector3.forward * -360f, 1f, RotateMode.FastBeyond360)
                .SetLoops(-1).SetUpdate(true);
        }    

        loadingCanvasGroup.DOFade(show ? 1 : 0, 0.2f).OnComplete(()=> 
        {
            if(!show)
            {
                ShowLoadingCanvas(false);
                ShowFadeCanvas(false);

                loader.DOKill();
                callback?.Invoke();
            }
        }).SetUpdate(true);
    }

    public void ShowAutoLoader()
    {

    }

    public void ShowLoaderWithPercentage(bool show)
    {

    }

    public void FadeIn(UnityAction callback = null)
    {
        ShowLoadingCanvas(false);
        ShowFadeCanvas(true);

        fadeCanvasGroup.DOFade(1, 0.2f).OnComplete(() =>
        {
            callback?.Invoke();
        }).SetUpdate(true);
    }

    public void FadeOut(UnityAction callback = null) 
    {
        fadeCanvasGroup.DOFade(0, 0.2f).OnComplete(() =>
        {
            ShowLoadingCanvas(false);
            ShowFadeCanvas(false);

            callback?.Invoke();
        }).SetUpdate(true);
    }

    void ShowLoadingCanvas(bool show)
    {
        loadingCanvasGroup.blocksRaycasts = show;
        loadingCanvasGroup.interactable = show;

        if (!show)
            loadingCanvasGroup.alpha = 0;
    }

    void ShowFadeCanvas(bool show)
    {
        fadeCanvasGroup.blocksRaycasts = show;
        fadeCanvasGroup.interactable = show;

        if (!show)
            fadeCanvasGroup.alpha = 0;
    }
}
