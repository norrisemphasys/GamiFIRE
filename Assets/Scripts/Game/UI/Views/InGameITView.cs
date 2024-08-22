using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;

public class InGameITView : BasicView
{
    public GameObject goTimerBG;
    public RectTransform rectTimer;

    public Button btnPause;

    [SerializeField] TextMeshProUGUI textTimer;
    [SerializeField] TextMeshProUGUI textTotalCoin;
    [SerializeField] TextMeshProUGUI textUserName;
    [SerializeField] TextMeshProUGUI textJob;
    [SerializeField] TextMeshProUGUI textTimeTraveled;

    [SerializeField] Slider sliderTimeTravel;

    public void Init()
    {
        textTimer.transform.DOScale(Vector3.zero, 0f);
        ShowTimer(false);
    }

    public void SetTimer(int time)
    {
        textTimer.text = time.ToString();
    }

    public void SetCoin(int coin, int maxCoin)
    {
        textTotalCoin.text = string.Format("{0} / {1}", coin, maxCoin);
    }

    public void SetUsername(string name)
    {
        textUserName.text = name;
    }

    public void SetJob()
    {

    }

    public void SetTimeTraveled(float time, bool init = false)
    {
        textTimeTraveled.text = init ? "0 sec." : string.Format("{0:#.#} secs.", time);
    }

    public void SetTimeTravelSlider(float val)
    {
        sliderTimeTravel.value = val;
    }

    public void ShowTimer(int time)
    {
        SetTimer(time);
        textTimer.transform.DOScale(Vector3.one, 0.5f).OnComplete(()=> 
        {
            textTimer.transform.DOScale(Vector3.zero, 0.5f).SetUpdate(true);
        }).SetEase(Ease.InOutBounce).SetUpdate(true);
    }

    public void ShowTimer(bool show)
    {
        goTimerBG.SetActive(show);
    }
}
