using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameIntroController : BasicController
{
	private GameIntroView view;

	private int _currentIndex = 0;
	private int _maxStep = 4;

	void Awake()
	{
		view = GetComponent<GameIntroView>();
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

		/*if(gameManager.currentScene == SCENE_TYPE.PORT_SCENE)
			gameManager.playerController.SetPause(false);*/
	}

	public override void Initialize()
	{
		if (gameManager.currentScene == SCENE_TYPE.PORT_SCENE)
        {
			Audio.PlayBGMPort();
			Audio.PlayBGMSea();
		}

		_maxStep = view.goTextInfo.Length;

		UpdateTutorial();

		if (gameManager.currentScene == SCENE_TYPE.PORT_SCENE)
			gameManager.playerController.SetPause(true);
	}

	public void ShowNextMenu()
	{
		uiController.Show(nextState);
	}

	void AddListener()
	{
		view.btnClose.onClick.AddListener(OnClickClose);
		view.btnPrevious.onClick.AddListener(OnClickPrevious);
		view.btnNext.onClick.AddListener(OnClickNext);
	}

	void RemoveListener()
	{
		view.btnClose.onClick.RemoveListener(OnClickClose);
		view.btnPrevious.onClick.RemoveListener(OnClickPrevious);
		view.btnNext.onClick.RemoveListener(OnClickNext);
	}

	void OnClickClose()
	{
		if (gameManager.currentScene == SCENE_TYPE.LOGIN_SCENE)
			OnClickDefault(UIState.LOGIN);
		else if (gameManager.currentScene == SCENE_TYPE.PORT_SCENE)
			OnClickDefault(UIState.PORT_TUTORIAL_MENU);
	}

	void OnClickPrevious()
	{
		_currentIndex--;
		UpdateTutorial();
	}

	void OnClickNext()
	{
		_currentIndex++;
		UpdateTutorial();
	}

	void UpdateTutorial()
	{
		if (_currentIndex <= 0)
			_currentIndex = 0;

		if (_currentIndex >= _maxStep)
			_currentIndex = _maxStep - 1;

		view.ShowTutorial(_currentIndex);

		view.btnPrevious.interactable = _currentIndex != 0;
		view.btnNext.interactable = _currentIndex < _maxStep - 1;
	}
}
