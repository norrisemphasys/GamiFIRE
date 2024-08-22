using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseITController : BasicController
{
	private PauseITView view;
	void Awake()
	{
		view = GetComponent<PauseITView>();
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
	}

	public void ShowNextMenu()
	{
		uiController.Show(nextState);
	}

	void AddListener()
	{
		view.btnContinue.onClick.AddListener(OnClickContinue);
		view.btnRestart.onClick.AddListener(OnClickRestart);
		view.btnGiveup.onClick.AddListener(OnClickGiveup);
	}

	void RemoveListener()
	{
		view.btnContinue.onClick.RemoveListener(OnClickContinue);
		view.btnRestart.onClick.RemoveListener(OnClickRestart);
		view.btnGiveup.onClick.RemoveListener(OnClickGiveup);
	}

	void OnClickContinue()
	{
		OnClickDefault(UIState.IT_INGAME);
	}

	void OnClickRestart()
	{
		LoadingManager.instance.FadeIn(()=>
		{
			LoadSceneManager.instance.LoadSceneLevel(2,
				UnityEngine.SceneManagement.LoadSceneMode.Single,
				() =>
				{
					LoadingManager.instance.FadeOut();
				});
		});

	}

	void OnClickGiveup()
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
