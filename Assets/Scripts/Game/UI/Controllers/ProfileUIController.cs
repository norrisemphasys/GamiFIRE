using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProfileUIController : BasicController
{
	private ProfileUIView view;
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

		User currentUser = UserManager.instance.currentUser;
		if (currentUser != null)
			view.SetUser(currentUser);
	}

	public void ShowNextMenu()
	{
		uiController.Show(nextState);
	}

	void AddListener()
	{
		view.buttonClose.onClick.AddListener(OnClickClose);
	}

	void RemoveListener()
	{
		view.buttonClose.onClick.RemoveListener(OnClickClose);
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

	private void OnDestroy()
	{
		RemoveListener();
	}
}
