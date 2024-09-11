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
			LoadSceneManager.instance.LoadSceneLevel(3,
				UnityEngine.SceneManagement.LoadSceneMode.Single,
				() =>
				{
					LoadingManager.instance.FadeOut();
				});
		});
	}

	void OnClickRestart()
	{
		LoadingManager.instance.FadeIn(() =>
		{
			LoadSceneManager.instance.LoadSceneLevel(2,
				UnityEngine.SceneManagement.LoadSceneMode.Single,
				() =>
				{
					LoadingManager.instance.FadeOut();
				});
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
