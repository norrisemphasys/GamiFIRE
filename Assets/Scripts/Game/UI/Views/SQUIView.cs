using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;

public class SQUIView : BasicView
{
    public Button[] btnAnswers;

    [SerializeField] TextMeshProUGUI textQuestion;
    [SerializeField] TextMeshProUGUI textTitle;
    public void Init()
    {

    }

    public void SetTextQuestion(string text)
    {
        textQuestion.text = text;
    }

    public void SetTextTitle(string text)
    {
        textTitle.text = text;
    }
}
