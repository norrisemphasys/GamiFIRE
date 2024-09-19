using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MGMMController : BasicController
{
	private MGMMView view;

	void Awake()
	{
		view = GetComponent<MGMMView>();
		view.Init();
	}

	public override void OnEnter()
	{
		base.OnEnter();
		view.Show();

		Initialize();
		AddListener();

		Debug.LogError("On Enter MGMM");
	}

	public override void OnExit()
	{
		Debug.LogError("On Exit");
		RemoveListener();
		view.Hide(ShowNextMenu);
	}

	public override void Initialize()
	{
		uiController = gameManager.miniGameController.uiController;
	}

	public void ShowNextMenu()
	{
		Debug.LogError("Show Next Menu " + uiController.name);
		gameManager.miniGameController.uiController.Show(nextState);
	}

	void AddListener()
	{
		view.buttonPlay.onClick.AddListener(OnClickPlay);
	}

	void RemoveListener()
	{
		view.buttonPlay.onClick.RemoveListener(OnClickPlay);
	}

	void OnClickPlay()
    {
		OnClickDefault(gameManager.miniGameController.uiController, UIState.MGIGONE_MENU);
		Debug.LogError("On Click play");
    }
}
