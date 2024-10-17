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
        string GPSign = gp > 0 ? "+" : "";
        string IPSign = ip > 0 ? "+" : "";
        string SPSign = sp > 0 ? "+" : "";
        string MCPSign = mcp > 0 ? "+" : "";

        textPrize.text = string.Format("<#1eb7fd>Innovation " + IPSign + "{0}   " +
            "<#3FC611>Satisfaction " + IPSign + "{1}   " + "<#F83510>Growth " + GPSign + "{2}   " + "<#F8C810>Money " + MCPSign + "{3}", 
            ip, sp, gp, mcp);
    }
}
