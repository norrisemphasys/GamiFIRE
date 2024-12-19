using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IslandTutorialController : BasicController
{
	private IslandTutorialView view;

	private int _currentIndex = 0;
	private int _maxStep = 3;

	void Awake()
	{
		view = GetComponent<IslandTutorialView>();
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
		_maxStep = view.goImageTutorial.Length;
		UpdateTutorial();
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
		OnClickDefault(UIState.ISLAND_MENU);
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
