using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IslandUIController : BasicController
{
	private IslandUIView view;
	void Awake()
	{
		view = GetComponent<IslandUIView>();
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

	}

	void RemoveListener()
	{

	}
}
