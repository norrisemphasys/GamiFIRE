using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "SO/Question", order = 2)]
public class QuestionSO : ScriptableObject
{
    public string questionTitle;
    public AnswerData[] answerData;
}


[System.Serializable]
public class AnswerData
{
    public string answer;

    public int growthPoint;
    public int innovationPoint;
    public int satisfactionPoint;
    public int moneyCurrencyPoints;
}