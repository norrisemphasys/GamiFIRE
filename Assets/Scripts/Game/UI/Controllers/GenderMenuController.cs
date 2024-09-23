using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenderMenuController : BasicController
{
	private GenderMenuView view;

	void Awake()
	{
		view = GetComponent<GenderMenuView>();
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

	}

	public void ShowNextMenu()
	{
		uiController.Show(nextState);
	}
	void AddListener()
	{
		for (int i = 0; i < view.buttonSelect.Length; i++)
			AddButtonListener(i, view.buttonSelect[i], OnClickSelect);
	}

	void RemoveListener()
	{
		for (int i = 0; i < view.buttonSelect.Length; i++)
			view.buttonSelect[i].onClick.RemoveAllListeners();
	}

	void OnClickSelect(int idx)
	{
		UserManager.instance.SetGender(idx);
		OnClickDefault(UIState.JOB_MENU);
	}
}
