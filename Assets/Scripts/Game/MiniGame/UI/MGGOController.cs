using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MGGOController : BasicController
{
	private MGGOView view;

	void Awake()
	{
		view = GetComponent<MGGOView>();
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
		uiController = gameManager.miniGameController.uiController;

		MiniGameType type = gameManager.miniGameController.CurrentType;

		if (type == MiniGameType.MG_ONE)
			Audio.StopBGMMGOne();
		else if (type == MiniGameType.MG_TWO)
			Audio.StopBGMMGTwo();
		else if (type == MiniGameType.MG_THREE)
			Audio.StopBGMMGThree();

		if (gameManager.WinIslandMode)
        {
			Audio.PlaySFXMGWin();
			view.SetMessage(type == MiniGameType.MG_ONE ? "Great job! You've finish the game without falling into the river." : "You're an amazing catcher!");
		}
        else
        {
			Audio.PlaySFXMGLose();
			view.SetMessage(type == MiniGameType.MG_ONE ? "Too bad you fell into the river! Better luck next time." : "You missed your chance! Try again.");
		}

		view.UpdateUserPoints(ScoreManager.instance.tempScore);
		ScoreManager.instance.ResetTempScore();

		Cursor.visible = true;
	}

	public void ShowNextMenu()
	{
		Debug.LogError("Show next menu " + nextState);
		gameManager.uiController.Show(nextState);
		gameManager.miniGameController.UnLoad(gameManager.miniGameController.CurrentType);
	}

	void AddListener()
	{
		view.buttonBack.onClick.AddListener(OnClickBack);
	}

	void RemoveListener()
	{
		view.buttonBack.onClick.RemoveListener(OnClickBack);
	}

	void OnClickBack() 
	{
		OnClickDefault(UIState.ISLAND_MENU);
	}
	private void OnDestroy()
	{
		RemoveListener();
	}
}
