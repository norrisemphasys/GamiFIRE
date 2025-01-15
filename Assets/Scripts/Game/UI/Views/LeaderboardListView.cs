using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;

public class LeaderboardListView : MonoBehaviour
{
    [SerializeField] 
    private TextMeshProUGUI textRankNo;

    [SerializeField]
    private TextMeshProUGUI textName;

    [SerializeField]
    private TextMeshProUGUI textScore;

    [SerializeField] 
    private Color[] rankColor;

    [SerializeField]
    private Sprite[] rankSprites;

    [SerializeField]
    private Sprite[] rankFrames;

    [SerializeField]
    private Image rankImg;

    [SerializeField]
    private Image rankFrame;
    
    public void SetUp(int idx , User user) 
    {
        int rank = idx + 1;

        textName.text = user.Username;
        textScore.text = string.Format("{0}", user.Score);
        textRankNo.text = rank.ToString();

        if (idx >= 3)
            idx = 3;

        rankFrame.sprite = rankFrames[idx];
        rankImg.sprite = rankSprites[idx];

        if (idx >= 3)
            rankImg.enabled = false;
        else
            rankImg.enabled = true;
    }
}
