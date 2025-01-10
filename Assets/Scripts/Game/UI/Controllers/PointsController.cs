using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointsController : BasicController
{
	private PointsView view;

	[SerializeField] string[] descriptions;
	[SerializeField] string[] titles;

	int selectedIndex = -1;

	void Awake()
	{
		view = GetComponent<PointsView>();
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
		view.ShowDescriptionPanel(false);
		view.ShowPointsPanel(true);
	}

	public void ShowNextMenu()
	{
		uiController.Show(nextState);
	}

	void AddListener()
	{
		view.buttonBack.onClick.AddListener(OnClickBack);
		view.buttonSelect.onClick.AddListener(OnClickSelect);

		for (int i = 0; i < view.buttonPoints.Length; i++)
			AddButtonListener(i, view.buttonPoints[i], OnClickPoints);
	}

	void RemoveListener()
	{
		view.buttonBack.onClick.RemoveListener(OnClickBack);
		view.buttonSelect.onClick.RemoveListener(OnClickSelect);

		for (int i = 0; i < view.buttonPoints.Length; i++)
			view.buttonPoints[i].onClick.RemoveAllListeners();
	}

	void OnClickBack()
    {
		view.ShowDescriptionPanel(false);
		view.ShowPointsPanel(true);

		selectedIndex = -1;
	}

	void OnClickSelect()
    {
		// Selected point

		OnClickDefault(UIState.ISLAND_TUTORIAL_MENU);
    }

	void OnClickPoints(int idx)
    {
		view.ShowDescriptionPanel(true);
		view.ShowPointsPanel(false);

		view.SetDescription(idx);
		view.SetDescription(descriptions[idx]);
		view.SetTitle(titles[idx]);

		selectedIndex = idx;
	}

	private void OnDestroy()
	{
		RemoveListener();
	}
}
