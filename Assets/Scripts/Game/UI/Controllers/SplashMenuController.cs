using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplashMenuController : BasicController
{
	private SplashMenuView view;
	void Awake()
	{
		view = GetComponent<SplashMenuView>();
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
		Audio.PlayBGMLogin();
		view.PlaySplashSequence(() =>
		{
			PopupManager.instance.ShowPopup(PopupMessage.CreatePopupData("INFORMATION", 
				"For the best possible experience we recommend you play the game in full screen view.",
				"FULL SCREEN", "CONTINUE", true, true, false, OnFullScreen, OnCancel));

			//OnClickDefault(UIState.LOGIN);
		});
	}

	void OnFullScreen()
    {
		Application.ExternalCall("GoToFullScreen");
		//Screen.fullScreen = true;
		//WebGLExternalManager.FullScreen();
		OnClickDefault(UIState.LOGIN);
	}

	void OnCancel()
    {
		OnClickDefault(UIState.LOGIN);
	}

	public void ShowNextMenu()
	{
		uiController.Show(nextState);
	}

	void AddListener()
	{

	}

	void RemoveListener()
	{

	}

	private void OnDestroy()
	{
		RemoveListener();
	}
}
