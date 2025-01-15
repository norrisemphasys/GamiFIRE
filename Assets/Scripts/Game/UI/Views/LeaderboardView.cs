using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;

public class LeaderboardView : BasicView
{
    public Button buttonUpdate;

    public Button buttonBack;

    public Transform parent;

    public GameObject leaderboardPrefab;

    [SerializeField]
    private TextMeshProUGUI textRankNo;

    [SerializeField]
    private TextMeshProUGUI textName;

    [SerializeField]
    private TextMeshProUGUI textScore;

    public void Init()
    {

    }

    public void SetupRank(int idx, User user)
    {
        int rank = idx + 1;

        textName.text = user.Username;
        textScore.text = string.Format("{0}", user.Score);
        textRankNo.text = rank.ToString();
    }
}
