using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingUIController : BasicController
{
	private BuildingUIView view;
	void Awake()
	{
		view = GetComponent<BuildingUIView>();
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
			view.SetCoin(currentUser.CurrencyPoint);
	}

	public void ShowNextMenu()
	{
		uiController.Show(nextState);
	}

	void AddListener()
	{
		view.buttonClose.onClick.AddListener(OnClickClose);
		GameEvents.OnCoinUpdate.AddListener(OnCoinUpdated);
	}

	void RemoveListener()
	{
		view.buttonClose.onClick.RemoveListener(OnClickClose);
		GameEvents.OnCoinUpdate.RemoveListener(OnCoinUpdated);
	}

	void OnCoinUpdated(int coin)
    {
		view.SetCoin(coin);
    }

	void OnClickClose()
    {
		OnClickDefault(UIState.ISLAND_MENU);
    }

	private void OnDestroy()
	{
		RemoveListener();
	}
}
