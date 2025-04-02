using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;

public class ProfileUIView : BasicView
{
    public Button buttonClose;
    public GameObject prefabBadgeview;
    public RectTransform badgeContent;

    [SerializeField] TextMeshProUGUI textUserName;
    [SerializeField] TextMeshProUGUI textUserID;
    [SerializeField] TextMeshProUGUI textRank;
    [SerializeField] TextMeshProUGUI textJob;

    [SerializeField] TextMeshProUGUI textScore;
    [SerializeField] TextMeshProUGUI textCoin;

    [SerializeField] Image jobImage;
    [SerializeField] Sprite[] jobIcons;

    [Header("Badge Popup View")]

    public Button buttonBadgeClose;

    [SerializeField] Image badgeIcon;
    [SerializeField] TextMeshProUGUI textTitle;
    [SerializeField] TextMeshProUGUI textDescription;
    [SerializeField] Transform badgePopupMain;
    [SerializeField] Transform badgePopup;

    [SerializeField] GameObject iconLock;

    public Button buttonClaimLink;

    public void Init()
    {
        badgePopupMain.gameObject.SetActive(false);
        badgePopup.localScale = Vector3.zero;
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

    public void ShowBadgePopup(BadgeListView view, bool show)
    {
        if(show)
        {
            badgeIcon.sprite = view.sprite;
            textTitle.text = view.title;
            textDescription.text = view.description;

            badgePopupMain.gameObject.SetActive(show);
            badgePopup.DOScale(Vector3.one, 0.2f).OnComplete(() => 
            {

            });
        }
        else
        {
            badgePopup.DOScale(Vector3.zero, 0.2f).OnComplete(()=> 
            {
                badgePopupMain.gameObject.SetActive(show);
            });
        } 
    }

    public void ShowIfLock(bool islock)
    {
        iconLock.SetActive(islock);
        buttonClaimLink.gameObject.SetActive(!islock);
    }
}
