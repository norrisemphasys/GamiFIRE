using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JobMenuController : BasicController
{
	private JobMenuView view;

	void Awake()
	{
		view = GetComponent<JobMenuView>();
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
	}

	public override void Initialize()
	{

	}

	public void ShowNextMenu()
	{
		uiController.Show(nextState);
	}

	void AddListener()
	{
		for (int i = 0; i < view.buttonSelect.Length; i++)
			AddButtonListener(i, view.buttonSelect[i], OnClickSelect);
	}

	void RemoveListener()
	{
		for (int i = 0; i < view.buttonSelect.Length; i++)
			view.buttonSelect[i].onClick.RemoveAllListeners();
	}

	void OnClickSelect(int idx)
    {
		UserManager.instance.SetJobType(idx);
		User updatedUser = UserManager.instance.currentUser;
		updatedUser.isAnExistingAccount = true;

		LoadingManager.instance.ShowLoader(true);

		JobType professionType = (JobType)idx;

		DBManager.AddEditUserLocalID(updatedUser, res =>
		{
			PopupManager.instance.ShowPopup(PopupMessage.OptionPopup("You have selected the Profession of " + UserManager.GetJobName(professionType), 
				"OK", "Select Other Profession", ()=> 
				{
					UserManager.instance.SetCurrentUser(res);
					BadgeManager.CreateCredentialRequest(res);

                    if (res.HasBadge) 
					{
						DBManager.GetAllUsersBadge(res, (success) => 
						{
							LoadPortScene();
						});
                    }
                    else
                    {
						DBManager.CreateUserBadge((userbadge) =>
						{
							// Temporary getting of badge.
							// BadgeManager.GetBadge("");

							if (!UserManager.instance.currentUser.HasBadge)
								UserManager.instance.currentUser.HasBadge = true;

							UserManager.instance.SaveUser(() => 
							{
								LoadPortScene();
							});
						});
					}				
				}));

			LoadingManager.instance.ShowLoader(false);
		});
	}

	void LoadPortScene()
    {
		LoadingManager.instance.ShowLoader(true);

		LoadSceneManager.instance.LoadSceneLevel(1,
		UnityEngine.SceneManagement.LoadSceneMode.Single,
		() =>
		{
			LoadingManager.instance.ShowLoader(false);
		});
	}

	private void OnDestroy()
	{
		RemoveListener();
	}
}
