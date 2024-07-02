using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class LoadingManager : MonoSingleton<LoadingManager>
{
    [SerializeField] CanvasGroup canvasGroup;
    [SerializeField] Transform loader;

    [SerializeField] bool useTestMode = false;

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

    public void ShowLoader(bool show)
    {
        if(show)
        {
            canvasGroup.blocksRaycasts = true;
            canvasGroup.interactable = true;

            loader.DOKill();
            loader.DORotate(Vector3.forward * -360f, 1f, RotateMode.FastBeyond360).SetLoops(-1);
        }    

        canvasGroup.DOFade(show ? 1 : 0, 0.2f).OnComplete(()=> 
        {
            if(!show)
            {
                canvasGroup.blocksRaycasts = false;
                canvasGroup.interactable = false;

                loader.DOKill();
            }
        });
    }

    public void ShowAutoLoader()
    {

    }

    public void ShowLoaderWithPercentage(bool show)
    {

    }
}
