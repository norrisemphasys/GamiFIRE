using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "SO/Survey", order = 5)]
public class SurveySO : ScriptableObject
{
    public string id;
    public string title;
    public SurveyQuestion[] surveyQuestion;
}
