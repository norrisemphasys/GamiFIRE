using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;

public class MGGOView : BasicView
{
    public Button buttonBack;

    [SerializeField] TextMeshProUGUI textGrowth;
    [SerializeField] TextMeshProUGUI textInnovation;
    [SerializeField] TextMeshProUGUI textSatisfaction;
    [SerializeField] TextMeshProUGUI textCoin;

    [SerializeField] TextMeshProUGUI textBonus;

    [SerializeField] TextMeshProUGUI textMessage;

    public void Init()
    {
        
    }

    public void UpdateUserPoints(ScoreManager.TempScore tempscore)
    {
        textCoin.text = tempscore.coin != 0 ? string.Format("{0:#,#}", tempscore.coin) : "0";
        textGrowth.text = tempscore.growthPoint != 0 ? string.Format("{0:#,#}", tempscore.growthPoint) : "0";
        textInnovation.text = tempscore.innovationPoint != 0 ? string.Format("{0:#,#}", tempscore.innovationPoint) : "0";
        textSatisfaction.text = tempscore.satsifactionPoint != 0 ? string.Format("{0:#,#}", tempscore.satsifactionPoint) : "0";

        textBonus.text = tempscore.score != 0 ? string.Format("{0:#,#}", tempscore.score) : "0";
    }

    public void SetMessage(string message)
    {
        string translatedMessage = LanguageManager.instance.GetUITranslatedText(message);
        textMessage.text = translatedMessage;
    }
}
