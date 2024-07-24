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
	}

	public void ShowNextMenu()
	{
		uiController.Show(nextState);
	}

	void AddListener()
	{
		view.btnBack.onClick.AddListener(OnClickBack);
	}

	void RemoveListener()
	{
		view.btnBack.onClick.RemoveListener(OnClickBack);
	}

	void OnClickBack()
	{
		OnExit();
	}

	void OnClickResume()
	{

	}
}
