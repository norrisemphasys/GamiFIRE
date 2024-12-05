using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MGIGThreeController : BasicController
{
	[SerializeField] MGInputController inputController;
	private MGIGThreeView view;

	private bool _startState = false;
	int _maxPlayerCount = 80;

	void Awake()
	{
		view = GetComponent<MGIGThreeView>();
		view.Init();
	}

	private void Update()
	{
		if (!_startState)
			return;

		inputController.UpdateInput();
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
		_startState = false;
	}

	public override void Initialize()
	{
		uiController = gameManager.miniGameController.uiController;

		_startState = true;
		view.SetPoints(0);
	}

	public void ShowNextMenu()
	{
		uiController.Show(nextState);
	}

	void AddListener()
	{
		GameEvents.OnMovePlayerCount.AddListener(OnPlayerMoveCount);
		GameEvents.OnGameOverMiniGame.AddListener(OnGameOver);
	}

	void RemoveListener()
	{
		GameEvents.OnMovePlayerCount.RemoveListener(OnPlayerMoveCount);
		GameEvents.OnGameOverMiniGame.RemoveListener(OnGameOver);
	}

	private void OnDestroy()
	{
		RemoveListener();
	}

	void OnPlayerMoveCount(int count)
    {
		view.SetPoints(count);

		if (count >= _maxPlayerCount)
			OnGameOver(true);

	}

	void OnGameOver(bool win)
    {
		gameManager.WinIslandMode = win;
		OnClickDefault(UIState.MGGO_MENU);
	}
}
