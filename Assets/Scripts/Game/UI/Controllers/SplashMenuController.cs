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

		Test();
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
		OnClickDefault(UIState.GAME_INTRO_MENU);
	}

    void Test()
    {
        string totalScore = LanguageManager.instance.GetUITranslatedText("TOTAL SCORE:");
        string pointTranslate = LanguageManager.instance.GetUITranslatedText("CURRENCY");

        string stringPoint = string.Format("<size=60>{0}</size>", pointTranslate);
        string defaultText = "For this island you focused on {0} and you reached {1} Points!";

        string defaultTextTranslate = LanguageManager.instance.GetUITranslatedText(defaultText);

        string badText = "You have scored BAD, next time try to select answers that are more focused on {0} so you can achieve more points and get the open badge.";
        string badTextTranslate = LanguageManager.instance.GetUITranslatedText(badText);

        string averageText = "You have scored AVERAGE, next time try to select answers that are more focused on {0} so you can achieve more points and get the open badge.";
        string averageTextTranslate = LanguageManager.instance.GetUITranslatedText(averageText);

        Debug.Log(string.Format(defaultTextTranslate, stringPoint, 10));
        Debug.Log(string.Format(badTextTranslate, stringPoint));
        Debug.Log(string.Format(averageTextTranslate, stringPoint));
        Debug.Log(totalScore);
    }

    void OnCancel()
    {
		OnClickDefault(UIState.GAME_INTRO_MENU);
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
