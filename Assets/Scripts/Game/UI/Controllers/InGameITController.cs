using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameITController : BasicController
{
	public float maxTravelTime;
	public int maxCoin;

	private InGameITView view;

	private float _traverTimer = 0f;
	private int _startTimer = 3;

	float timeSpeed = 1;
	bool startGame = false;
	int coinsCollected = 0;


	void Awake()
	{
		view = GetComponent<InGameITView>();
		view.Init();
	}

    private void Update()
    {
        if(startGame)
        {
			UpdateTimer();
		}
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
		if (gameManager.previousState == UIState.IT_PAUSE ||
			gameManager.previousState == UIState.IT_GAMEOVER)
			Time.timeScale = 0;
		else
        {
			coinsCollected = 0;
			Time.timeScale = 1;
		}

		ShowStartTimer();

		view.SetTimeTraveled(0, true);
		view.SetCoin(0, maxCoin);
	}

	public void ShowNextMenu()
	{
		uiController.Show(nextState);
	}

	void AddListener()
	{
		GameEvents.OnChangeTerrainSpeed.AddListener(OnChangeSpeed);
		GameEvents.OnCoinCollected.AddListener(OnCoinCollected);

		view.btnPause.onClick.AddListener(OnClickPause);
	}

	void RemoveListener()
	{
		GameEvents.OnChangeTerrainSpeed.RemoveListener(OnChangeSpeed);
		GameEvents.OnCoinCollected.RemoveListener(OnCoinCollected);

		view.btnPause.onClick.RemoveListener(OnClickPause);
	}

	void ShowStartTimer() 
	{
		StartCoroutine(ShowStartTimerEnum());
	}

	IEnumerator ShowStartTimerEnum()
    {
		view.ShowTimer(true);
		view.ShowTimer(3);
		yield return new WaitForSecondsRealtime(1f);
		view.ShowTimer(2);
		yield return new WaitForSecondsRealtime(1f);
		view.ShowTimer(1);
		yield return new WaitForSecondsRealtime(1f);

		// Start Game.
		StartGame();

	}

	void StartGame()
    {
		Time.timeScale = 1;

		gameManager.terrainController.StartSpawnObstacle(true);
		view.ShowTimer(false);
		startGame = true;
	}

	void OnClickPause()
	{
		OnClickDefault(UIState.IT_PAUSE);
	}

	void UpdateTimer()
    {
		_traverTimer += Time.deltaTime;

		view.SetTimeTraveled(Mathf.Abs(_traverTimer));

		float normalizeDistance = _traverTimer / maxTravelTime;
		view.SetTimeTravelSlider(normalizeDistance);

		if(normalizeDistance >= 1)
        {
			// End timer
			Debug.Log("End Game");

			if(coinsCollected >= maxCoin)
            {
				// Win
				gameManager.WinITMode = true;
				OnClickDefault(UIState.IT_GAMEOVER);
			}
            else
            {
				// Lose
				gameManager.WinITMode = false;
				OnClickDefault(UIState.IT_GAMEOVER);
			}

			startGame = false;
        }
    }

	void OnChangeSpeed(float speed)
    {
		timeSpeed = speed == 50 ? 1 : 0.6f;
    }

	void OnCoinCollected(int coin)
    {
		coinsCollected += coin;

		if(coinsCollected <= 0)
        {
			coinsCollected = 0;

			// End Game. Lose
			gameManager.WinITMode = false;

			OnClickDefault(UIState.IT_GAMEOVER);
		}

		view.SetCoin(coinsCollected, maxCoin);
    }

    private void OnDestroy()
    {
		RemoveListener();
    }
}
