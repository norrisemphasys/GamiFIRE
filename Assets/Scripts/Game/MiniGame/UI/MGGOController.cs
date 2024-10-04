using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MGGOController : BasicController
{
	private MGGOView view;

	void Awake()
	{
		view = GetComponent<MGGOView>();
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
		uiController = gameManager.miniGameController.uiController;
	}

	public void ShowNextMenu()
	{
		Debug.LogError("Show next menu " + nextState);
		gameManager.uiController.Show(nextState);
		gameManager.miniGameController.UnLoad(gameManager.miniGameController.CurrentType);
	}

	void AddListener()
	{
		view.buttonBack.onClick.AddListener(OnClickBack);
	}

	void RemoveListener()
	{
		view.buttonBack.onClick.RemoveListener(OnClickBack);
	}

	void OnClickBack() 
	{
		OnClickDefault(UIState.ISLAND_MENU);
	}
	private void OnDestroy()
	{
		RemoveListener();
	}
}
