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
		Audio.StopBGMIslandTrip();

		if(gameManager.currentScene == SCENE_TYPE.ISLAND_TRIP_SCENE)
			view.SetIslandName("ISLAND TRIP");
		else if(gameManager.currentScene == SCENE_TYPE.ISLAND_SCENE)
        {
			string IslandName = UserManager.GetJobName(gameManager.IslandType);
			view.SetIslandName(IslandName + " ISLAND");
		}
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
        switch (gameManager.currentScene)
        {
			case SCENE_TYPE.LOGIN_SCENE:
				break;
			case SCENE_TYPE.PORT_SCENE:
				break;
			case SCENE_TYPE.ISLAND_TRIP_SCENE:
				OnClickDefault(UIState.IT_INGAME);
				break;
			case SCENE_TYPE.ISLAND_SCENE:
				OnClickDefault(UIState.ISLAND_MENU);
				break;
        }
	}

	void OnClickRestart()
	{
		int loadIndex = 0;
		switch (gameManager.currentScene)
		{
			case SCENE_TYPE.LOGIN_SCENE:
				break;
			case SCENE_TYPE.PORT_SCENE:
				break;
			case SCENE_TYPE.ISLAND_TRIP_SCENE:
				loadIndex = 2;
				break;
			case SCENE_TYPE.ISLAND_SCENE:
				loadIndex = 3;
				break;
		}

		LoadingManager.instance.FadeIn(()=>
		{
			LoadSceneManager.instance.LoadSceneLevel(loadIndex,
				UnityEngine.SceneManagement.LoadSceneMode.Single,
				() =>
				{
					LoadingManager.instance.FadeOut();
				});
		});

	}

	void OnClickGiveup()
    {
		int loadIndex = 0;
		switch (gameManager.currentScene)
		{
			case SCENE_TYPE.LOGIN_SCENE:
				break;
			case SCENE_TYPE.PORT_SCENE:
				break;
			case SCENE_TYPE.ISLAND_TRIP_SCENE:
				loadIndex = 1;
				break;
			case SCENE_TYPE.ISLAND_SCENE:
				loadIndex = 1;
				break;
		}

		Time.timeScale = 1;
		LoadingManager.instance.FadeIn(() =>
		{
			LoadSceneManager.instance.LoadSceneLevel(loadIndex,
				UnityEngine.SceneManagement.LoadSceneMode.Single,
				() =>
				{
					LoadingManager.instance.FadeOut();
				});
		});
	}
	private void OnDestroy()
	{
		RemoveListener();
	}
}
