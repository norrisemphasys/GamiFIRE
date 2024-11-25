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

		DBManager.AddEditUserLocalID(updatedUser, res =>
		{
			PopupManager.instance.ShowPopup(PopupMessage.InfoPopup("Your account has been updated.", () =>
			{
				LoadPortScene();
			}));

			UserManager.instance.SetCurrentUser(res);
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
}
