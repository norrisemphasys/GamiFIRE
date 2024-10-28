using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverITController : BasicController
{
	private GameOverITView view;

	void Awake()
	{
		view = GetComponent<GameOverITView>();
		view.Init();
	}

	public override void OnEnter()
	{
		Debug.LogError("Game over controller");
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
		Time.timeScale = 0;
		view.Show(gameManager.WinITMode);

		Audio.StopBGMIslandTrip();

		if(gameManager.WinITMode)
        {
			Audio.PlaySFXGameOverWin();
        }
        else
        {
			Audio.PlaySFXGameOverLose();
        }
	}

	public void ShowNextMenu()
	{
		uiController.Show(nextState);
	}

	void AddListener()
	{
		view.btnNo.onClick.AddListener(OnClickHome);
		view.btnYes.onClick.AddListener(OnClickRestart);

		view.btnProceed.onClick.AddListener(OnClickPoceed);
	}

	void RemoveListener()
	{
		view.btnNo.onClick.RemoveListener(OnClickHome);
		view.btnYes.onClick.RemoveListener(OnClickRestart);

		view.btnProceed.onClick.RemoveListener(OnClickPoceed);
	}

	void OnClickPoceed()
	{
		LoadingManager.instance.FadeIn(() =>
		{
			UserManager.instance.SaveUser(()=> 
			{
				LoadIslandCutScene();
			});
		});
	}

	void OnClickRestart()
	{
		LoadingManager.instance.FadeIn(() =>
		{
			UserManager.instance.SaveUser(()=> 
			{
				ResetScene();
			});
		});
	}

	void ResetScene()
    {
		LoadSceneManager.instance.LoadSceneLevel(2,
		UnityEngine.SceneManagement.LoadSceneMode.Single,
		() =>
		{
			LoadingManager.instance.FadeOut();
		});
	}

	void LoadIslandCutScene()
    {
		LoadSceneManager.instance.LoadSceneLevel(4,
		UnityEngine.SceneManagement.LoadSceneMode.Single,
		() =>
		{
			LoadingManager.instance.FadeOut(null, 1f);
		});
	}


	void OnClickHome()
    {
		Time.timeScale = 1;
		LoadingManager.instance.FadeIn(() =>
		{
			LoadSceneManager.instance.LoadSceneLevel(1,
			UnityEngine.SceneManagement.LoadSceneMode.Single,
			() =>
			{
				LoadingManager.instance.FadeOut();
			});
		});
	}
}
