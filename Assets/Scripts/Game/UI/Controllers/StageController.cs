using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageController : BasicController
{
	private StageView view;

	void Awake()
	{
		view = GetComponent<StageView>();
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
		gameManager.playerController.SetPause(false);
		RemoveListener();
		view.Hide(ShowNextMenu);
	}

	public override void Initialize()
	{
		gameManager.playerController.SetPause(true);
		Audio.PlaySFXPortal();
	}

	public void ShowNextMenu()
	{
		uiController.Show(nextState);
	}

	void AddListener()
	{
		view.btnBack.onClick.AddListener(OnClickBack);

		for(int i = 0; i < view.btnIslands.Length; i++)
			AddButtonListener(i, view.btnIslands[i], ClickIsland);
	}

	void RemoveListener()
	{
		view.btnBack.onClick.RemoveListener(OnClickBack);

		for (int i = 0; i < view.btnIslands.Length; i++)
			view.btnIslands[i].onClick.RemoveAllListeners();
	}

	void OnClickBack()
	{
		OnClickDefault(UIState.PORT_INGAME);
	}

	void OnClickResume()
	{

	}

	void ClickIsland(int idx)
    {
		Audio.PlaySFXStageClick();
		Utils.Delay(this, LoadIslandScene, 1f);
	}

	void LoadIslandScene()
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
}
