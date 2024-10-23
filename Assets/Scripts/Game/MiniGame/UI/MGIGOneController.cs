using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MGIGOneController : BasicController
{
	[SerializeField] MGInputController inputController;

	private MGIGOneView view;

	private bool _startState = false;

	private bool _startTimer = false;
	private bool _isPlayerFinishedMoving = false;
	private float _timer;
	private float _maxTime;

	private int _maxPlayerMove = 20;

	void Awake()
	{
		view = GetComponent<MGIGOneView>();
		view.Init();
	}

    private void Update()
    {
		if (!_startState)
			return;

		inputController.UpdateInput();
		UpdateTimer();
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
		Audio.PlayBGMMGOne();
		ScoreManager.instance.ResetTempScore();

		uiController = gameManager.miniGameController.uiController;
		_startState = true;
		_startTimer = true;
		_isPlayerFinishedMoving = false;
		_timer = 0;
		_maxTime = 20f;
	}

	public void ShowNextMenu()
	{
		uiController.Show(nextState);
	}

	void AddListener()
	{
		GameEvents.OnPressA.AddListener(OnClickButtonType);
		GameEvents.OnPressS.AddListener(OnClickButtonType);
		GameEvents.OnPressD.AddListener(OnClickButtonType);

		GameEvents.OnGameOverMiniGame.AddListener(OnGameOver);
		GameEvents.OnMovePlayerCount.AddListener(OnPlayerMoveCount);

		view.buttonRed.onClick.AddListener(OnClickRed);
		view.buttonYellow.onClick.AddListener(OnClickYellow);
		view.buttonGreen.onClick.AddListener(OnClickGreen);
	}

	void RemoveListener()
	{
		GameEvents.OnPressA.RemoveListener(OnClickButtonType);
		GameEvents.OnPressS.RemoveListener(OnClickButtonType);
		GameEvents.OnPressD.RemoveListener(OnClickButtonType);

		GameEvents.OnGameOverMiniGame.RemoveListener(OnGameOver);
		GameEvents.OnMovePlayerCount.RemoveListener(OnPlayerMoveCount);

		view.buttonRed.onClick.RemoveListener(OnClickRed);
		view.buttonYellow.onClick.RemoveListener(OnClickYellow);
		view.buttonGreen.onClick.RemoveListener(OnClickGreen);
	}

	void UpdateTimer()
    {
		if(_startTimer)
        {
			_timer += Time.deltaTime;
			float reverseTimer = _maxTime - _timer;

			view.SetTimer(reverseTimer);

			if (reverseTimer <= 0)
            {
				// Check score
				GameEvents.OnGameOverMiniGame?.Invoke(false);
				_startTimer = false;
			}
		}
    }

	void OnPlayerMoveCount(int count)
    {
		Debug.LogError("Move count " + count);

		if(count >= _maxPlayerMove && _startTimer)
        {
			GameEvents.OnGameOverMiniGame?.Invoke(true);
			_isPlayerFinishedMoving = true;
			_startTimer = false;
		}
    }

	void OnClickButtonType(MiniGame.PlatformType type) 
	{
		Debug.LogError("TYPE " + type);
		switch(type)
        {
			case MiniGame.PlatformType.RED:
				view.ButtonPress(view.buttonRed);
				break;
			case MiniGame.PlatformType.YELLOW:
				view.ButtonPress(view.buttonYellow);
				break;
			case MiniGame.PlatformType.GREEN:
				view.ButtonPress(view.buttonGreen);
				break;
        }
	}

	void OnGameOver(bool win)
	{
		gameManager.WinIslandMode = win;
		OnClickDefault(UIState.MGGO_MENU);
	}

	void OnClickRed()
    {
		GameEvents.OnPressA.Invoke(MiniGame.PlatformType.RED);
    }

	void OnClickYellow()
	{
		GameEvents.OnPressS.Invoke(MiniGame.PlatformType.YELLOW);
	}

	void OnClickGreen()
	{
		GameEvents.OnPressD.Invoke(MiniGame.PlatformType.GREEN);
	}

	void OnClickContinue()
	{
		OnClickDefault(UIState.IT_INGAME);
	}
	private void OnDestroy()
	{
		RemoveListener();
	}
}
