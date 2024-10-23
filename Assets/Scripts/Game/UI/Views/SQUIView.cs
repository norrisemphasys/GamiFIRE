using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;

public class SQUIView : BasicView
{
    public Button[] btnAnswers;
    public Button buttonCollect;

    [SerializeField] TextMeshProUGUI textQuestion;
    [SerializeField] TextMeshProUGUI textTitle;

    [SerializeField] RectTransform scorePopup;

    [SerializeField] TextMeshProUGUI textGrowth;
    [SerializeField] TextMeshProUGUI textInnovation;
    [SerializeField] TextMeshProUGUI textSatisfaction;
    [SerializeField] TextMeshProUGUI textCoin;

    public void Init()
    {
        ShowScorePopup(false);
    }

    public void SetTextQuestion(string text)
    {
        textQuestion.text = text;
    }

    public void SetTextTitle(string text)
    {
        textTitle.text = text;
    }

    public void ShowScorePopup(bool show)
    {
        scorePopup.gameObject.SetActive(show);
    }

    public void SetPoints(int gp, int ip, int sp, int mcp)
    {
        string GPSign = gp > 0 ? "+" : "";
        string IPSign = ip > 0 ? "+" : "";
        string SPSign = sp > 0 ? "+" : "";
        string MCPSign = mcp > 0 ? "+" : "";

        textGrowth.text = string.Format(GPSign + "{0}", gp);
        textInnovation.text = string.Format(IPSign + "{0}", ip);
        textSatisfaction.text = string.Format(SPSign + "{0}", sp);
        textCoin.text = string.Format(MCPSign + "{0}", mcp);
    }
}
