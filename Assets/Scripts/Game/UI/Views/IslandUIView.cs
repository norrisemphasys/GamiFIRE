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

    [SerializeField] TextMeshProUGUI textCoin;
    [SerializeField] TextMeshProUGUI textGrowthPoint;
    [SerializeField] TextMeshProUGUI textInnovationPoint;
    [SerializeField] TextMeshProUGUI textSatisfactionPoint;

    [SerializeField] TextMeshProUGUI textUserName;
    [SerializeField] TextMeshProUGUI textJobType;

    [SerializeField] GameObject[] diceResultView;

    public void Init()
    {

    }

    public void SetDiceResult(int idx)
    {
        for(int i = 0; i < diceResultView.Length; i++)
            diceResultView[i].SetActive(false);
        diceResultView[idx-1].SetActive(true);
    }

    public void UpdateUserPoints(User user)
    {
        JobType job = (JobType)user.JobType;

        textUserName.text = user.Username;
        textJobType.text = job.ToString();

        textCoin.text = user.CurrencyPoint != 0 ? string.Format("{0:#,#}", user.CurrencyPoint) : "0";
        textGrowthPoint.text = user.GrowthPoint != 0 ? string.Format("{0:#,#}", user.GrowthPoint) : "0";
        textInnovationPoint.text = user.InnovationPoint != 0 ? string.Format("{0:#,#}", user.InnovationPoint) : "0";
        textSatisfactionPoint.text = user.SatisfactionPoint != 0 ? string.Format("{0:#,#}", user.SatisfactionPoint) : "0";
    }
}
