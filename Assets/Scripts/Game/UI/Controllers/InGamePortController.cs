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

		view.ShowMiniMap(false);
		view.Hide(ShowNextMenu);
	}

	public override void Initialize()
	{
		User currentUser = UserManager.instance.currentUser;
		if (currentUser != null)
			view.UpdateUserPoints(currentUser);

		view.ShowMiniMap(true);
	}

	public void ShowNextMenu()
	{
		if(nextState != UIState.NONE)
			uiController.Show(nextState);
	}

	void AddListener()
	{
		view.buttonInfo.onClick.AddListener(OnClickInfo);
		view.buttonProfile.onClick.AddListener(OnClickProfile);
	}

	void RemoveListener()
	{
		view.buttonInfo.onClick.RemoveListener(OnClickInfo);
		view.buttonProfile.onClick.RemoveListener(OnClickProfile);
	}

	void OnClickInfo()
    {
		OnClickDefault(UIState.PORT_TUTORIAL_MENU);
    }

	void OnClickProfile()
    {
		OnClickDefault(UIState.PROFILE_MENU);
	}

	private void OnDestroy()
	{
		RemoveListener();
	}
}
