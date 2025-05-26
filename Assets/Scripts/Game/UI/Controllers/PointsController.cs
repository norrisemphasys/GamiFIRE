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
		if (gameManager.SelectedPointIndex >= 0)
        {
			OnClickPoints(gameManager.SelectedPointIndex);
			view.buttonSelect.gameObject.SetActive(false);

			if (gameManager.previousState != UIState.SQ_MENU)
			{
				Debug.LogError("play audio 1");
				Audio.PlayBGMStudentIsland();
				Audio.PlayBGMSea();

			}
		}
        else
        {
			view.ShowDescriptionPanel(false);
			view.ShowPointsPanel(true);
			view.buttonSelect.gameObject.SetActive(true);
			Debug.LogError("play audio 2");
			Audio.PlayBGMStudentIsland();
			Audio.PlayBGMSea();

		}
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
		if (gameManager.SelectedPointIndex >= 0)
        {
			if (gameManager.previousState == UIState.SQ_MENU)
			{
				nextState = UIState.NONE;
				gameManager.SetGameState(UIState.SQ_MENU);
				OnExit();
			}
			else
			{
				OnClickDefault(UIState.ISLAND_MENU);
			}
		}
        else
        {
			view.ShowDescriptionPanel(false);
			view.ShowPointsPanel(true);

			selectedIndex = -1;
		}
	}

	void OnClickSelect()
    {
		// Selected point
		gameManager.SelectedPointIndex = selectedIndex;
		OnClickDefault(UIState.ISLAND_TUTORIAL_MENU);
    }

	void OnClickPoints(int idx)
    {
		view.ShowDescriptionPanel(true);
		view.ShowPointsPanel(false);

		view.SetDescription(idx);

		string descriptionTranslate = LanguageManager.instance.GetUITranslatedText(descriptions[idx]);
		string titleTranslate = LanguageManager.instance.GetUITranslatedText(titles[idx]);

		view.SetDescription(descriptionTranslate);
		view.SetTitle(titleTranslate);

		selectedIndex = idx;
	}

	private void OnDestroy()
	{
		RemoveListener();
	}
}
