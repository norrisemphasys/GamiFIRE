using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;

public class AnswerView : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI textAnswer;
    [SerializeField] TextMeshProUGUI textPrize;

    public void SetData(string answer, int gp, int ip, int sp, int mcp)
    {
        textAnswer.text = answer;
        textPrize.text = string.Format("<#1eb7fd>Innovation {0} " +
            "<#3FC611>Satisfaction {1} <#F83510>Growth {2} <#F8C810>Money {3}", 
            ip, sp, gp, mcp);
    }
}
