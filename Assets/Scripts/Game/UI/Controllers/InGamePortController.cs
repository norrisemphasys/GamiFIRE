using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGamePortController : BasicController
{
	private InGamePortView view;

	void Awake()
	{
		view = GetComponent<InGamePortView>();
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
		User currentUser = UserManager.instance.currentUser;
		if (currentUser != null)
			view.UpdateUserPoints(currentUser);
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
