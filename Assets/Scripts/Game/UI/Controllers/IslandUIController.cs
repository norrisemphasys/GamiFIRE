using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IslandUIController : BasicController
{
	private IslandUIView view;
	private SceneController sceneController;

	[SerializeField] bool testScore = false;

	private bool showPlatformPanel = false;

	void Awake()
	{
		view = GetComponent<IslandUIView>();
		view.Init();
	}

	public override void OnEnter()
	{
		Debug.LogError("On enter island menu");
		base.OnEnter();
		view.Show();

		AddListener();
		Initialize();
	}

	public override void OnExit()
	{
		RemoveListener();
		view.Hide(ShowNextMenu);

		ScoreManager.instance.ResetTempScore();
	}

	public override void Initialize()
	{
		Audio.PlayBGMStudentIsland();
		Audio.PlayBGMSea();

		Time.timeScale = 1;

		sceneController = gameManager.sceneController;
		if (gameManager.previousState == UIState.ROLL_MENU)
        {
			view.SetDiceResult(gameManager.sceneController.MoveCounter);
			view.SetPulse(false);
		}
        else
        {
			sceneController.cameraController.SetCamera(CameraType.ISO);
			view.SetDiceResult(gameManager.sceneController.MoveCounter);
			view.SetPulse(true);

			gameManager.sceneController.environmentController.UpdateEnvironment();

			if(gameManager.sceneController.HasPrize)
            {
				view.ShowBoosterInfo(true,
					gameManager.sceneController.currentPrizeData);
            }
            else
            {
				view.ShowBoosterInfo(false,
					gameManager.sceneController.currentPrizeData);
			}
		}
            
		if(gameManager.sceneController.StartGame)
        {
			sceneController.cameraController.SetCamera(CameraType.THIRD_PERSON);
			sceneController.StartPlayerCellMove(()=> 
			{
				gameManager.sceneController.StartGame = false;
				sceneController.cameraController.SetCamera(CameraType.ISO);
				Debug.LogError("Total Moves " + sceneController.TotalMoves);
				if(sceneController.TotalMoves >= sceneController.cellController.maxCellCount)
					OnClickDefault(UIState.RESULT_MENU);
                else
					ShowPopup();
			});
        }

		if(testScore)
        {
			view.UpdateTestScore();
		}
        else
        {
			User currentUser = UserManager.instance.currentUser;
			if (currentUser != null)
				view.UpdateUserPoints(currentUser);
		}

		UserManager.instance.SaveUser(()=>
		{
			// Update User to server
		});

		if(!showPlatformPanel)
        {
			view.ShowPlatformPanel(true);
			showPlatformPanel = true;
		}
	}

	void ShowPopup()
    {
		string message = string.Empty;
		Cell currentCell = sceneController.cellController.GetCurrentCell();

		if (currentCell.Type == CellType.SCENARIO)
			message = "You landed on a <#FD9744>SCENARIO QUESTION";
		else if (currentCell.Type == CellType.SPINNER)
			message = "You landed on a <#1CB0F6>SPINNER";
		else if (currentCell.Type == CellType.MINIGAME)
			message = "You landed on a <#61708A>MINI GAME";

		PopupManager.instance.ShowPopup(
			PopupMessage.InfoPopup(message, () =>
			{
				ShowCellType(currentCell.Type);
			})
		);
    }

	void ShowCellType(CellType type)
    {

		switch (type)
		{
			case CellType.SCENARIO:
				OnClickDefault(UIState.SQ_MENU);
				break;
			case CellType.SPINNER:
				OnClickDefault(UIState.SPIN_MENU);
				break;
			case CellType.MINIGAME:
				// To Do:
				view.Hide();
				RemoveListener();

				//gameManager.miniGameController.Load(MiniGameType.MG_ONE);
				gameManager.miniGameController.RandomLoad();
				break;
		}
	}

	public void ShowNextMenu()
	{
		uiController.Show(nextState);
	}

	void AddListener()
	{
		view.buttonRoll.onClick.AddListener(OnClickRoll);
		view.buttonPause.onClick.AddListener(OnClickPause);

		view.buttonStart.onClick.AddListener(OnClickStart);
	}

	void RemoveListener()
	{
		view.buttonRoll.onClick.RemoveListener(OnClickRoll);
		view.buttonPause.onClick.RemoveListener(OnClickPause);

		view.buttonStart.onClick.RemoveListener(OnClickStart);
	}

	void OnClickStart()
    {
		view.ShowPlatformPanel(false);
    }

	void OnClickRoll()
    {
		OnClickDefault(UIState.ROLL_MENU);
    }

	void OnClickPause()
    {
		OnClickDefault(UIState.IT_PAUSE);
    }
	private void OnDestroy()
	{
		RemoveListener();
	}
}
