using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGamePortController : BasicController
{
	private InGamePortView view;

	void Awake()
	{
		view = GetComponent<InGamePortView>();
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

		view.ShowMiniMap(false);
		view.Hide(ShowNextMenu);
	}

	public override void Initialize()
	{
		User currentUser = UserManager.instance.currentUser;
		if (currentUser != null)
			view.UpdateUserPoints(currentUser);

		view.ShowMiniMap(true);

		GetMainLandBadge();
		ShowSubscribeOption();
	}

	public void ShowNextMenu()
	{
		if(nextState != UIState.NONE)
			uiController.Show(nextState);
	}

	void AddListener()
	{
		view.buttonInfo.onClick.AddListener(OnClickInfo);
		view.buttonProfile.onClick.AddListener(OnClickProfile);

		view.buttonVolume.onClick.AddListener(OnClickVolume);
	}

	void RemoveListener()
	{
		view.buttonInfo.onClick.RemoveListener(OnClickInfo);
		view.buttonProfile.onClick.RemoveListener(OnClickProfile);

		view.buttonVolume.onClick.RemoveListener(OnClickVolume);
	}

	void ShowSubscribeOption()
    {
		User user = UserManager.instance.currentUser;
		if(user != null && !user.IsNewsletterSubscriber)
        {
			bool showSubscription = Random.Range(0, 100) > 50;
			if(showSubscription)
            {
				PopupManager.instance.ShowPopup(PopupMessage.Subscribe("Subscribe to our newsletter to get the latest updates about GAMI.FI.RE", null, () =>
				{
					user.IsNewsletterSubscriber = true;
					PlayerPrefs.SetInt("NewsLetter", user.IsNewsletterSubscriber ? 1 : 0);
					UserManager.instance.SetCurrentUser(user);
				}));
			}
		}
	}

	void GetMainLandBadge()
    {
		int badgeCount = BadgeManager.badgeList.Count;

		if (badgeCount == 0)
		{
			LanguageType type = LanguageManager.instance.currentLanguage;
			string message = "";
			if (type == LanguageType.ENGLISH)
				message = "Congratulations! By joining this game you have obtained your first Badge! \n The Mainland Badge: Island Explorer";
			else
				message = "Congratulations! By joining this game you have obtained your first Badge! The Mainland Badge: Island Explorer";
			PopupManager.instance.ShowPopup(PopupMessage.ClaimPopup(message, () =>
			{
				OnClickClaimBadge();
			}));
		}
    }

	void OnClickVolume()
    {
		PopupManager.instance.ShowPopup(PopupMessage.VolumePopup("Press the button below to turn on or off the volume.",
			   () =>
			   {
					// Click Volume Off
					MundoSound.ToggleVolume(false);
			   },
			   () =>
			   {
					// Click Volume On
					MundoSound.ToggleVolume(true);
			   })
		   );
	}

	void OnClickClaimBadge()
    {
		LoadingManager.instance.ShowLoader(true);
		BadgeManager.GetBadge("67ed253c4dc5c989a8cf5cf5", (success) =>
		{
			if (success)
			{
				DBManager.GetAllUsersBadge(UserManager.instance.currentUser, (success) =>
				{
					LoadingManager.instance.ShowLoader(false);
					PopupManager.instance.ShowPopup(PopupMessage.InfoPopup("You have successfully claimed the badge.", () =>
					{

					}));
				});
			}
			else
			{
				LoadingManager.instance.ShowLoader(false);
				PopupManager.instance.ShowPopup(PopupMessage.ErrorPopup("There was an error in the server when claiming the badge.", () =>
				{
				}));
			}
		});
	}

	void OnClickInfo()
    {
		OnClickDefault(UIState.PORT_TUTORIAL_MENU);
    }

	void OnClickProfile()
    {
		OnClickDefault(UIState.PROFILE_MENU);
	}

	private void OnDestroy()
	{
		RemoveListener();
	}
}
