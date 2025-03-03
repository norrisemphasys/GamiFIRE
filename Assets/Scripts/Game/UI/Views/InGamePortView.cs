using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;

public class InGamePortView : BasicView
{
    public Button buttonInfo;

    [SerializeField] Image profilePicture;
    [SerializeField] TextMeshProUGUI textCoin;
    [SerializeField] TextMeshProUGUI textGrowthPoint;
    [SerializeField] TextMeshProUGUI textInnovationPoint;
    [SerializeField] TextMeshProUGUI textSatisfactionPoint;

    [SerializeField] TextMeshProUGUI textUserName;
    [SerializeField] TextMeshProUGUI textJobType;

    [SerializeField] CanvasGroup miniMapCanvas;

    [SerializeField] Sprite[] icons;

    public void Init()
    {
        ShowMiniMap(false);
    }

    public void UpdateUserPoints(User user)
    {
        JobType job = (JobType)user.JobType;

        int idx = (int)user.JobType;
        profilePicture.sprite = icons[idx];

        textUserName.text = user.Username;
        textJobType.text = UserManager.GetJobName( job );

        textCoin.text = user.Coin != 0 ? string.Format("{0:#,#}", user.Coin) : "0";
        //textGrowthPoint.text = user.GrowthPoint != 0 ? string.Format("{0:#,#}", user.GrowthPoint) : "0";
        //textInnovationPoint.text = user.InnovationPoint != 0 ? string.Format("{0:#,#}", user.InnovationPoint) : "0";
        textSatisfactionPoint.text = user.Score != 0 ? string.Format("{0:#,#}", user.Score) : "0";
    }

    public void ShowMiniMap(bool show)
    {
        miniMapCanvas.alpha = show ? 1 : 0;
        miniMapCanvas.interactable = show;
        miniMapCanvas.blocksRaycasts = show;
    }
}
