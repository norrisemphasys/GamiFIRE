using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MGTController : BasicController
{
	private MGTView view;

	private int _maxStep = 3;
	private int _type = -1;

	private int _currentStep = 0;

	void Awake()
	{
		view = GetComponent<MGTView>();
		view.Init();
	}

	public override void OnEnter()
	{
		base.OnEnter();
		view.Show();

		Initialize();
		AddListener();

	}

	public override void OnExit()
	{
		RemoveListener();
		view.Hide(ShowNextMenu);
	}

	public override void Initialize()
	{
		_type = (int)gameManager.miniGameController.CurrentType - 1;
		_currentStep = 0;

		view.SetMGTView(_type);
		UpdateView();
	}

	public void ShowNextMenu()
	{
		gameManager.miniGameController.uiController.Show(nextState);
	}

	void AddListener()
	{
		view.buttonNext.onClick.AddListener(OnClickNext);
		view.buttonPrev.onClick.AddListener(OnClickPrev);

		view.buttonClose.onClick.AddListener(OnClikClose);
	}

	void RemoveListener()
	{
		view.buttonNext.onClick.RemoveListener(OnClickNext);
		view.buttonPrev.onClick.RemoveListener(OnClickPrev);

		view.buttonClose.onClick.RemoveListener(OnClikClose);
	}

	void OnClickNext()
    {
		_currentStep++;

		if (_currentStep >= _maxStep)
			_currentStep = _maxStep - 1;

		UpdateView();
	}

	void OnClickPrev()
    {
		_currentStep--;

		if (_currentStep < 0)
			_currentStep = 0;

		UpdateView();
	}

	void OnClikClose()
    {
		switch (gameManager.miniGameController.CurrentType)
		{
			case MiniGameType.MG_ONE:
				OnClickDefault(gameManager.miniGameController.uiController, UIState.MGIGONE_MENU);
				break;
			case MiniGameType.MG_TWO:
				Cursor.visible = false;
				OnClickDefault(gameManager.miniGameController.uiController, UIState.MGIGTWO_MENU);
				break;
			case MiniGameType.MG_THREE:
				OnClickDefault(gameManager.miniGameController.uiController, UIState.MGIGTHREE_MENU);
				break;
		}
	}

	void UpdateView()
    {
		view.ShowStep(_type, _currentStep);

		view.buttonNext.interactable = _currentStep < _maxStep - 1;
		view.buttonPrev.interactable = _currentStep > 0; 
    }

	private void OnDestroy()
	{
		RemoveListener();
	}
}
