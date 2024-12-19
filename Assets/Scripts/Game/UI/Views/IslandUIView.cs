using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;

public class IslandUIView : BasicView
{
    public Button buttonRoll;
    public Button buttonPause;
    public Button buttonStart;
    public Button buttonBuilding;

    public Button[] buttonPoints;

    [SerializeField] TextMeshProUGUI textCoin;
    [SerializeField] TextMeshProUGUI textGrowthPoint;
    [SerializeField] TextMeshProUGUI textInnovationPoint;
    [SerializeField] TextMeshProUGUI textSatisfactionPoint;

    [SerializeField] TextMeshProUGUI textUserName;
    [SerializeField] TextMeshProUGUI textJobType;

    [SerializeField] GameObject[] diceResultView;

    [SerializeField] AnimationPulse pulse;
    [SerializeField] GameObject textPressToRoll;

    [SerializeField] RectTransform platformPanel;
    [SerializeField] GameObject goPlatformPanelDim;

    [SerializeField] SpinnerResultView spinnerResultView;

    [SerializeField] TextMeshProUGUI textIslandName;
    [SerializeField] GameObject notif;

    ScoreManager.TempScore tempScore;

    bool testScore = false;

    public void Init()
    {
        platformPanel.DOScale(Vector3.zero, 0);
        ShowBoosterInfo(false);
        ShowNotif(false);
    }

    public void ShowNotif(bool show)
    {
        notif.SetActive(show);
    }

    public void ShowBoosterInfo(bool show, PrizeData data = null)
    {
        spinnerResultView.gameObject.SetActive(show);

        if (show && data != null)
            spinnerResultView.SetResultData(data);
    }

    public void ShowPlatformPanel(bool show)
    {
        goPlatformPanelDim.SetActive(show);
        platformPanel.DOScale(show ? Vector3.one : Vector3.zero, 0.3f).SetEase(Ease.InOutBounce);
    }

    public void SetDiceResult(int idx)
    {
        for(int i = 0; i < diceResultView.Length; i++)
            diceResultView[i].SetActive(false);
        diceResultView[idx-1].SetActive(true);
    }

    public void UpdateUserPoints(User user)
    {
        testScore = false;

        JobType job = (JobType)user.JobType;

        textUserName.text = user.Username;
        textJobType.text = UserManager.GetJobName(job);

        tempScore = ScoreManager.instance.tempScore;

        //textCoin.text = user.CurrencyPoint != 0 ? string.Format("{0:#,#}", user.CurrencyPoint) : "0";
        //textGrowthPoint.text = user.GrowthPoint != 0 ? string.Format("{0:#,#}", user.GrowthPoint) : "0";
        //textInnovationPoint.text = user.InnovationPoint != 0 ? string.Format("{0:#,#}", user.InnovationPoint) : "0";
        //textSatisfactionPoint.text = user.SatisfactionPoint != 0 ? string.Format("{0:#,#}", user.SatisfactionPoint) : "0"

        UpdateGrowhtPoints(user.GrowthPoint);
        UpdateInnovationPoints(user.InnovationPoint);
        UpdateSatisfactionPoint(user.SatisfactionPoint);
        UpdateCoinPoints(user.CurrencyPoint);
    }

    public void SetIslandName(string name)
    {
        textIslandName.text = name + " ISLAND";
    }
    
    public void UpdateTestScore()
    {
        testScore = true;

        UpdateGrowhtPoints(Random.Range(0, 20));
        UpdateInnovationPoints(Random.Range(0, 20));
        UpdateSatisfactionPoint(Random.Range(0, 20));
        UpdateCoinPoints(Random.Range(0, 20));
    }

    void UpdateGrowhtPoints(int endValue)
    {
        int origValue = endValue - (testScore ? Random.Range(0, 10) : tempScore.growthPoint);

        if (origValue == endValue)
        {
            textGrowthPoint.text = origValue != 0 ? string.Format("{0:#,#}", origValue) : "0";
        }
        else
        {
            textGrowthPoint.rectTransform.DOScale(Vector3.one * 1.5f, 0.1f).SetLoops(tempScore.growthPoint);

            DOTween.To(() => origValue, x => origValue = x, endValue, 1)
            .OnUpdate(() => {
                textGrowthPoint.text = origValue != 0 ? string.Format("{0:#,#}", origValue) : "0";
            }).OnComplete(() =>
            {
                textGrowthPoint.rectTransform.DOKill();
                textGrowthPoint.rectTransform.DOScale(Vector3.one, 0.1f);
            });
        }
    }

    void UpdateInnovationPoints(int endValue)
    {
        int origValue = endValue - (testScore ? Random.Range(0, 10) : tempScore.innovationPoint);

        if (origValue == endValue)
        {
            textInnovationPoint.text = origValue != 0 ? string.Format("{0:#,#}", origValue) : "0";
        }
        else
        {
            textInnovationPoint.rectTransform.DOScale(Vector3.one * 1.5f, 0.1f).SetLoops(tempScore.innovationPoint);

            DOTween.To(() => origValue, x => origValue = x, endValue, 1)
           .OnUpdate(() => {
               textInnovationPoint.text = origValue != 0 ? string.Format("{0:#,#}", origValue) : "0";
               
           }).OnComplete(() =>
           {
               textInnovationPoint.rectTransform.DOKill();
               textInnovationPoint.rectTransform.DOScale(Vector3.one, 0.1f);
           });
        }  
    }

    void UpdateSatisfactionPoint(int endValue)
    {
        int origValue = endValue - (testScore ? Random.Range(0, 10) : tempScore.satsifactionPoint);

        if (origValue == endValue)
        {
            textSatisfactionPoint.text = origValue != 0 ? string.Format("{0:#,#}", origValue) : "0";
        }
        else
        {
            textSatisfactionPoint.rectTransform.DOScale(Vector3.one * 1.5f, 0.1f).SetLoops(tempScore.satsifactionPoint);

            DOTween.To(() => origValue, x => origValue = x, endValue, 1)
            .OnUpdate(() => {
                textSatisfactionPoint.text = origValue != 0 ? string.Format("{0:#,#}", origValue) : "0";
            }).OnComplete(() =>
            {
                textSatisfactionPoint.rectTransform.DOKill();
                textSatisfactionPoint.rectTransform.DOScale(Vector3.one, 0.1f);
            });
        }

    }

    void UpdateCoinPoints(int endValue)
    {
        int origValue = endValue - (testScore ? Random.Range(0, 10) : tempScore.coin);

        if (origValue == endValue)
        {
            textCoin.text = origValue != 0 ? string.Format("{0:#,#}", origValue) : "0";
        }
        else
        {
            textCoin.rectTransform.DOScale(Vector3.one * 1.5f, 0.1f).SetLoops(tempScore.coin);

            DOTween.To(() => origValue, x => origValue = x, endValue, 1)
            .OnUpdate(() => {
                textCoin.text = origValue != 0 ? string.Format("{0:#,#}", origValue) : "0";
            }).OnComplete(()=> 
            {
                textCoin.rectTransform.DOKill();
                textCoin.rectTransform.DOScale(Vector3.one , 0.1f);
            });
        }
    }

    public void SetPulse(bool start)
    {
        pulse.StartPulse(start);
        textPressToRoll.SetActive(start);

        buttonRoll.interactable = start;
    }
}
