using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Text;

public class ProfileUIController : BasicController
{
	public BadgeInfoSO[] badgeInfos;

	private ProfileUIView view;
	private List<BadgeListView> badgeListView = new List<BadgeListView>();

	User currentUser;

	void Awake()
	{
		view = GetComponent<ProfileUIView>();
		view.Init();
	}

	public override void OnEnter()
	{
		base.OnEnter();
		view.Show();

		AddListener();
		Initialize();
	}

	public override void OnExit()
	{
		RemoveListener();
		view.Hide(ShowNextMenu);

		if (gameManager.currentScene == SCENE_TYPE.PORT_SCENE)
			gameManager.playerController.SetPause(false);
	}

	public override void Initialize()
	{
		if (gameManager.currentScene == SCENE_TYPE.PORT_SCENE)
			gameManager.playerController.SetPause(true);

		currentUser = UserManager.instance.currentUser;
		if (currentUser != null)
        {
			view.SetUser(currentUser);
			view.buttonShowRequestEmail.gameObject.SetActive(currentUser.IsAdministrator);
		}	

		ShowBadge();

	}

	public void ShowNextMenu()
	{
		uiController.Show(nextState);
	}

	void AddListener()
	{
		view.buttonClose.onClick.AddListener(OnClickClose);
		view.buttonBadgeClose.onClick.AddListener(OnClickCloseBadge);

		view.buttonRequestEmails.onClick.AddListener(OnClickRequestEmail);
		view.buttonShowRequestEmail.onClick.AddListener(OnClickShowRequestEmail);
		view.buttonCloseRequest.onClick.AddListener(OnClickCloseRequestEmail);
	}

	void RemoveListener()
	{
		view.buttonClose.onClick.RemoveListener(OnClickClose);
		view.buttonBadgeClose.onClick.RemoveListener(OnClickCloseBadge);

		view.buttonRequestEmails.onClick.RemoveListener(OnClickRequestEmail);
		view.buttonShowRequestEmail.onClick.RemoveListener(OnClickShowRequestEmail);
		view.buttonCloseRequest.onClick.AddListener(OnClickCloseRequestEmail);
	}

	void OnClickCloseRequestEmail()
    {
		view.ShowRequestEmailPopup(false);
	}

	void OnClickShowRequestEmail()
    {
		view.ShowRequestEmailPopup(true);
    }

	void OnClickRequestEmail()
    {
		string email = view.inputFieldEmail.text;
		if(Utils.IsValidEmail(email))
        {
			
			LoadingManager.instance.ShowLoader(true);
			DBManager.GetAllUsersByToken((users) =>
			{
				SendInfoEmail(users, email);

				LoadingManager.instance.ShowLoader(false);
				view.ShowRequestEmailPopup(false);
			});
        }
        else
        {
			PopupManager.instance.ShowPopup(PopupMessage.ErrorPopup("You entered and invalid Email."));
        }
    }

	void SendInfoEmail(User[] users, string email)
    {
		StringBuilder sb = new StringBuilder();

		sb.Append("Hi " + currentUser.Username);
		sb.Append("\n\n");
		sb.Append("Below is the list of all the users who subscribe to the newsletter:");
		sb.Append("\n\n");

		sb.Append("Registered Users: " + users.Length);
		sb.Append("\n\n");

		int countNewsLetter = 0;
		foreach (User user in users)
		{
			if (user.IsNewsletterSubscriber)
				countNewsLetter++;
		}

		sb.Append("\n\n");
		sb.Append("Registered Users Subscribed to Newsletter: " + countNewsLetter);
		sb.Append("\n\n");

		foreach (User user in users)
		{
			if (user.IsNewsletterSubscriber)
			{
				sb.Append(user.Username + " : " + user.Email);
				sb.Append("\n");
			}
		}

		int badgeCount = 0;
		foreach (UserBadge badge in DBManager.allUsersBadge)
		{
			if (badge.badges != null)
				badgeCount += 1;
		}

		sb.Append("\n\n");
		sb.Append("Number of badges given: " + badgeCount);

		int surveyCount = 0;
		foreach (UserSurvey survey in DBManager.allUsersWithSurvey)
		{
			if (survey.survey != null)
				surveyCount += 1;
		}

		sb.Append("\n\n");
		sb.Append("Number of users that answered the survey: " + DBManager.allUsersWithSurvey.Count);

		sb.Append("\n\n");
		sb.Append("Best Regards,");
		sb.Append("\n");
		sb.Append("GamiFIRE");

#if UNITY_EDITOR
		Debug.Log(sb.ToString());
#endif

#if UNITY_WEBGL && !UNITY_EDITOR
		EmailManager.instance.SendEmail(email, "GAMIFIRE Newsletter Info", sb.ToString());
#endif
	}

	void OnClickCloseBadge()
    {
		view.ShowBadgePopup(null, false);
		view.buttonClaimLink.onClick.RemoveAllListeners();
    }

	void OnClickClose()
    {
		switch (gameManager.currentScene)
		{
			case SCENE_TYPE.PORT_SCENE:
				OnClickDefault(UIState.PORT_INGAME);
				break;
			case SCENE_TYPE.ISLAND_SCENE:
				OnClickDefault(UIState.ISLAND_MENU);
				break;
		}
	}

	void ShowBadge()
    {
		/*badgeListView.Clear();

		foreach (Transform t in view.badgeContent.transform)
        {
			if (t != view.badgeContent.transform)
				GameObject.Destroy(t.gameObject);
        }

		for(int i = 0; i < BadgeManager.badgeList.Count; i++)
        {
			GameObject prefab = GameObject.Instantiate(view.prefabBadgeview);
			prefab.SetActive(true);
			prefab.transform.SetParent(view.badgeContent.transform);

			prefab.transform.localPosition = Vector3.zero;
			prefab.transform.localScale = Vector3.one;
			prefab.transform.localRotation = Quaternion.identity;

			BadgeListView bView = prefab.GetComponent<BadgeListView>();
			bView.SetBadgeData(BadgeManager.badgeList[i]);

			badgeListView.Add(bView);

			AddButtonListener(i, prefab.GetComponent<Button>(), OnClickBadgeView);
        }*/

		badgeListView.Clear();

		foreach (Transform t in view.badgeContent.transform)
		{
			if (t != view.badgeContent.transform)
				GameObject.Destroy(t.gameObject);
		}

		for (int i = 0; i < badgeInfos.Length; i++)
        {
			GameObject prefab = GameObject.Instantiate(view.prefabBadgeview);
			prefab.SetActive(true);
			prefab.transform.SetParent(view.badgeContent.transform);

			prefab.transform.localPosition = Vector3.zero;
			prefab.transform.localScale = Vector3.one;
			prefab.transform.localRotation = Quaternion.identity;

			BadgeListView bView = prefab.GetComponent<BadgeListView>();
			badgeListView.Add(bView);

			BadgeInfoSO info = badgeInfos[i];

			bool hasID = BadgeManager.badgeList.ContainsProperty(p => p.id, info.badgeID);
			info.locked = !hasID;

			bView.SetBadgeData(info);

			AddButtonListener(i, prefab.GetComponent<Button>(), OnClickBadgeView);
		}
    }

	void OnClickBadgeView(int idx)
    {
		BadgeInfoSO info = badgeInfos[idx];

		view.ShowBadgePopup(badgeListView[idx], true);
		view.ShowIfLock(info.locked);

		int index = BadgeManager.badgeList.FindIndex(x => x.id == info.badgeID);

		if (!info.locked)
        {
			view.buttonClaimLink.onClick.AddListener(() =>
			{
				//Application.OpenURL(BadgeManager.badgeList[index].claimLink);
				QRCodeManager.instance.ShowQRCode(true, BadgeManager.badgeList[index].claimLink);
			});
		}

		/*if (info.locked)
        {
			PopupManager.instance.ShowPopup(
				PopupMessage.InfoPopup("This badge is still lock. Play more to unlock this badge.", () =>
				{

				})
			);
		}
        else
        {
			view.ShowBadgePopup(badgeListView[idx], true);
			view.buttonClaimLink.onClick.AddListener(() =>
			{
				Application.OpenURL(BadgeManager.badgeList[idx].claimLink);
			});
		}*/
	}

	private void OnDestroy()
	{
		RemoveListener();
	}
}
