using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;

public class ResultView : BasicView
{
    public Button buttonBack;

    [SerializeField] TextMeshProUGUI textGrowth;
    [SerializeField] TextMeshProUGUI textInnovation;
    [SerializeField] TextMeshProUGUI textSatisfaction;
    [SerializeField] TextMeshProUGUI textCoin;
    [SerializeField] TextMeshProUGUI textScore;
    public void Init()
    {

    }

    public void UpdateUserPoints(User user)
    {
        textCoin.text = user.CurrencyPoint != 0 ? string.Format("{0:#,#}", user.CurrencyPoint) : "0";
        textGrowth.text = user.GrowthPoint != 0 ? string.Format("{0:#,#}", user.GrowthPoint) : "0";
        textInnovation.text = user.InnovationPoint != 0 ? string.Format("{0:#,#}", user.InnovationPoint) : "0";
        textSatisfaction.text = user.SatisfactionPoint != 0 ? string.Format("{0:#,#}", user.SatisfactionPoint) : "0";

        int totalScore = user.CurrencyPoint + user.GrowthPoint + user.InnovationPoint + user.SatisfactionPoint;

        textScore.text = totalScore != 0 ? string.Format("{0:#,#}", totalScore) : "0";
    }
}
