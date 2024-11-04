using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.Events;

public class CutsceneAnimation : MonoBehaviour
{
    [SerializeField] UnityEvent OnFinishedCutScene;

    [SerializeField] Transform speedBoatParent;
    [SerializeField] Transform speedBoat;
    [SerializeField] Transform cameraTransform;
    [SerializeField] Transform targetDestination;
    [SerializeField] Transform targetCameraDest;

    [SerializeField] bool startCutScene = false;

    [SerializeField] CanvasGroup canvasGroup;
    [SerializeField] RectTransform rectText;

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1;
        PlayCutScene();
    }

    public void PlayCutScene()
    {
        Audio.PlayBGMSea();
        Audio.PlaySFXEngine();

        speedBoatParent.DOMove(targetDestination.position, 15f).SetUpdate(true);
        cameraTransform.DOMove(targetCameraDest.position, 20f).OnComplete(()=> 
        {
            ShowText();
        }).SetUpdate(true);
    }

    void ShowText()
    {
        canvasGroup.DOFade(1, 0.2f).SetUpdate(true);
        rectText.DOScaleY(1, 0.2f).OnComplete(()=> 
        {
            Utils.Delay(this, () =>
            {
                LoadingManager.instance.FadeIn(() =>
                {
                    LoadIslandScene();
                }, 1f);
                OnFinishedCutScene?.Invoke();
            }, 2f);
        }).SetUpdate(true);
    }

    void LoadIslandScene()
    {
        LoadSceneManager.instance.LoadSceneLevel(3,
        UnityEngine.SceneManagement.LoadSceneMode.Single,
        () =>
        {
            LoadingManager.instance.FadeOut(null, 1f);
        });
    }
}
