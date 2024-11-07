using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.Events;
using TMPro;

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
    [SerializeField] TextMeshProUGUI textMessage;

    [SerializeField] bool enableAudio = true;

    JobType islandType;
    string currentIsland;

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1;
        PlayCutScene();
    }

    public void PlayCutScene()
    {
        if(enableAudio)
        {
            Audio.PlayBGMSea();
            Audio.PlaySFXEngine();  
        }

        islandType = GameManager.instance.IslandType;
        currentIsland = islandType.ToString();

        textMessage.text = "You have arrived on the " + currentIsland + " ISLAND";

        speedBoatParent.DOMove(targetDestination.position, 15f).SetUpdate(true);
        cameraTransform.DOMove(targetCameraDest.position, 20f).OnComplete(()=> 
        {
            canvasGroup.DOFade(1, 0.2f).SetUpdate(true);
            ShowText(true);

        }).SetUpdate(true);
    }

    void ShowText(bool hasDelay)
    {
        rectText.DOScaleY(1, 0.2f).OnComplete(()=> 
        {
            if(hasDelay)
            {
                Utils.Delay(this, () =>
                {
                    rectText.DOScaleY(0, 0.2f).OnComplete(()=> 
                    {
                        textMessage.text = "Each choice you make will influence your path. Manage your time, money, and opportunities to thrive in this new academic adventure!";
                        ShowText(false);
                    });
                }, 4f);
            }
            else
            {
                DelayAndFade();
            }
        }).SetUpdate(true);
    }

    void DelayAndFade()
    {
        Utils.Delay(this, () =>
        {
            LoadingManager.instance.FadeIn(() =>
            {
                LoadIslandScene();
            }, 1f);
            OnFinishedCutScene?.Invoke();
        }, 5f);
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
