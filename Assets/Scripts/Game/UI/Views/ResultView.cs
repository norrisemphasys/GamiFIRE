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
    [SerializeField] TextMeshProUGUI textCurrency;
    [SerializeField] TextMeshProUGUI textScore;

    [SerializeField] TextMeshProUGUI textResult;
    [SerializeField] TextMeshProUGUI textRating;

    [SerializeField] Slider growthSlider;
    [SerializeField] Slider satisfactionSlider;
    [SerializeField] Slider innovationSlider;
    [SerializeField] Slider currencySlider;

    [SerializeField] GameObject[] selected;

    public void Init()
    {

    }

    public void UpdateUserPoints(User user)
    {
       /* textCurrency.text = user.CurrencyPoint != 0 ? string.Format("{0:#,#}", user.CurrencyPoint) : "0";
        textGrowth.text = user.GrowthPoint != 0 ? string.Format("{0:#,#}", user.GrowthPoint) : "0";
        textInnovation.text = user.InnovationPoint != 0 ? string.Format("{0:#,#}", user.InnovationPoint) : "0";
        textSatisfaction.text = user.SatisfactionPoint != 0 ? string.Format("{0:#,#}", user.SatisfactionPoint) : "0";*/

        //int totalScore = user.CurrencyPoint + user.GrowthPoint + user.InnovationPoint + user.SatisfactionPoint;
    }

    public void ShowSelected(int idx)
    {
        for (int i = 0; i < selected.Length; i++)
            selected[i].SetActive(i == idx);
    }

    public void UpdateSliders(float growth, float satisfaction, float innovation, float currency)
    {
        growthSlider.value = growth;
        satisfactionSlider.value = satisfaction;
        innovationSlider.value = innovation;
        currencySlider.value = currency;

        textGrowth.text = string.Format("{0:#}%", growth * 100);
        textSatisfaction.text = string.Format("{0:#}%", satisfaction * 100);
        textInnovation.text = string.Format("{0:#}%", innovation * 100);
        textCurrency.text =  string.Format("{0:#}%", currency * 100);
    }

    public void UpdateScore(int score)
    {
        textScore.text = score != 0 ? string.Format("{0:#,#}", score) : "0";
    }

    public void ShowTextResult(string point, int value, float rate)
    {
        string stringPoint = string.Format("<size=60>{0}</size>", point);
        textResult.text = string.Format("For this island you focused on the {0} and you reached {1} Points!", stringPoint, value);

        if (rate <= 30)
            textRating.text = string.Format("You have scored BAD, next time try to select answers that are more focused on {0} so you can achieve more points", stringPoint);
        else if(rate >= 30 && rate < 70)
            textRating.text = string.Format("You have scored AVERAGE, next time try to select answers that are more focused on {0} so you can achieve more points", stringPoint);
        else if(rate >= 70)
            textRating.text = string.Format("You have scored GOOD. Well done!");
    }
}
