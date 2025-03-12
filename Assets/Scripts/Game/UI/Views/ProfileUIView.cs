using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;

public class ProfileUIView : BasicView
{
    public Button buttonClose;

    [SerializeField] TextMeshProUGUI textUserName;
    [SerializeField] TextMeshProUGUI textUserID;
    [SerializeField] TextMeshProUGUI textRank;
    [SerializeField] TextMeshProUGUI textJob;

    [SerializeField] TextMeshProUGUI textScore;
    [SerializeField] TextMeshProUGUI textCoin;

    [SerializeField] RectTransform badgeContent;

    [SerializeField] Image jobImage;

    [SerializeField] Sprite[] jobIcons;

    public void Init()
    {

    }

    public void SetUser(User user)
    {
        textUserName.text = user.Username;
        textUserID.text = user.ID;

        //textRank.text = string.Format("RANK: <#43536c><size=170%>{0}", 1);

        textScore.text = string.Format("{0}", user.Score);
        textCoin.text = string.Format("{0}", user.Coin);

        JobType job = (JobType)user.JobType;

        int idx = (int)user.JobType;
        jobImage.sprite = jobIcons[idx];

        textJob.text = UserManager.GetJobName(job);
    }
}
