using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MGIGTwoController : BasicController
{
	[SerializeField] MGInputController inputController;

	private MGIGTwoView view;

	private bool _startState = false;
	private bool _startTimer = false;
	private bool _fastReady = false;

	private int _coinsCollected = 0;
	private int _lifeRemoved = 0;
	private int _fastSpeed = 1;
	private float _scaleSpeed = 1;
	private int _eggCount = 3;
	private int _eggCounter = 0;
	private float _delaySpawn = 1f;

	private float _scaleCounter = 0f;

	void Awake()
	{
		view = GetComponent<MGIGTwoView>();
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

		gameManager.miniGameController.mgDropController.StopDrop();
	}

	public override void Initialize()
	{
		Audio.PlayBGMMGTwo();
		ScoreManager.instance.ResetTempScore();

		uiController = gameManager.miniGameController.uiController;

		view.SetPoints(0);
		view.LifeRemoved(-1);
		

		_startState = true;
		_startTimer = true;
		_fastReady = false;

		_coinsCollected = 0;
		_lifeRemoved = -1;
		_fastSpeed = 1;
		_eggCount = 3;
		_eggCounter = 3;
		_delaySpawn = 1f;
		_scaleSpeed = 1;
		_scaleCounter = 0f;

		view.SetTextFast(_fastSpeed);
		ShowFastPanel();
	}

	public void ShowNextMenu()
	{
		uiController.Show(nextState);
	}

	void AddListener()
	{
		GameEvents.OnGameOverMiniGame.AddListener(OnGameOver);
		GameEvents.OnDropCollected.AddListener(DropCollected);
		GameEvents.OnLifeRemove.AddListener(OnLifeRemoved);
	}

	void RemoveListener()
	{
		GameEvents.OnGameOverMiniGame.RemoveListener(OnGameOver);
		GameEvents.OnDropCollected.RemoveListener(DropCollected);
		GameEvents.OnLifeRemove.RemoveListener(OnLifeRemoved);
	}

	void UpdateFastSpeed()
    {
		_fastSpeed++;
		_scaleCounter += 0.1f;
		_scaleSpeed = 1 + _scaleCounter;

		_eggCounter = _eggCount * _fastSpeed;
		_delaySpawn = 1f - _scaleCounter;

		if (_fastSpeed <= 5 && !_fastReady)
			_fastReady = true;
		else
			_fastReady = true;

		view.SetTextFast(_fastSpeed);
	}

	void ShowFastPanel()
    {
		view.ShowFastPanel(true, 0, () =>
		{
			view.ShowFastPanel(false, 2, () =>
			{
				gameManager.miniGameController.GetMiniGame<MGTwo>(MiniGameType.MG_TWO)
					.DropEgg(_eggCounter, _delaySpawn, _scaleSpeed, UpdateFastSpeed);
			});
		});
	}

	void OnGameOver(bool win)
	{
		gameManager.WinIslandMode = win;
		OnClickDefault(UIState.MGGO_MENU);
	}

	void DropCollected(int coin, bool last)
    {
		_coinsCollected += coin;
		view.SetPoints(_coinsCollected);

		ScoreManager.instance.AddScore(1);

		if (last)
        {
			if(_fastSpeed >= 8)
				OnGameOver(true);
			else
				ShowFastPanel();
		}	
	}

	void OnLifeRemoved(int life, bool last) 
	{
		_lifeRemoved += life;

		if(_lifeRemoved >= 2)
        {
			OnGameOver(false);
        }
		else
        {
			view.LifeRemoved(_lifeRemoved);

			if (last)
				ShowFastPanel();
		}
	}

	private void OnDestroy()
	{
		RemoveListener();
	}
}
