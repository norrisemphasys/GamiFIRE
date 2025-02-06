using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;

public class InGamePortView : BasicView
{
    [SerializeField] TextMeshProUGUI textCoin;
    [SerializeField] TextMeshProUGUI textGrowthPoint;
    [SerializeField] TextMeshProUGUI textInnovationPoint;
    [SerializeField] TextMeshProUGUI textSatisfactionPoint;

    [SerializeField] TextMeshProUGUI textUserName;
    [SerializeField] TextMeshProUGUI textJobType;

    [SerializeField] CanvasGroup miniMapCanvas;

    public void Init()
    {
        ShowMiniMap(false);
    }

    public void UpdateUserPoints(User user)
    {
        JobType job = (JobType)user.JobType;

        textUserName.text = user.Username;
        textJobType.text = UserManager.GetJobName( job );

        textCoin.text = user.CurrencyPoint != 0 ? string.Format("{0:#,#}", user.CurrencyPoint) : "0";
        textGrowthPoint.text = user.GrowthPoint != 0 ? string.Format("{0:#,#}", user.GrowthPoint) : "0";
        textInnovationPoint.text = user.InnovationPoint != 0 ? string.Format("{0:#,#}", user.InnovationPoint) : "0";
        textSatisfactionPoint.text = user.SatisfactionPoint != 0 ? string.Format("{0:#,#}", user.SatisfactionPoint) : "0";
    }

    public void ShowMiniMap(bool show)
    {
        miniMapCanvas.alpha = show ? 1 : 0;
        miniMapCanvas.interactable = show;
        miniMapCanvas.blocksRaycasts = show;
    }
}
