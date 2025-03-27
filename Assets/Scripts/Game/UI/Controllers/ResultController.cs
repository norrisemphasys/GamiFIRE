using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResultController : BasicController
{
	private ResultView view;
	void Awake()
	{
		view = GetComponent<ResultView>();
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
		Audio.StopBGMStudentIsland();
		Audio.PlaySFXResult();

		User currentUser = UserManager.instance.currentUser;
		if (currentUser != null)
        {
			view.UpdateUserPoints(currentUser);

			int idx = gameManager.SelectedPointIndex;
			int totalScore = 0;
			int currentScore = currentUser.Score;

			if (idx == 0)
				totalScore = currentUser.GrowthPoint + currentScore;
			else if (idx == 1)
				totalScore = currentUser.SatisfactionPoint + currentScore;
			else if (idx == 2)
				totalScore = currentUser.InnovationPoint + currentScore;
			else if (idx == 3)
				totalScore = currentUser.CurrencyPoint + currentScore;

			currentUser.Score = totalScore;

			// Slider 
			Debug.LogError("c1 " + gameManager.sceneController.QuestionCounter + " c2 " + gameManager.sceneController.questionController.currentQuestionCount);
			int totalAnsweredQuestion = gameManager.sceneController.QuestionCounter;
			float totalPoints = totalAnsweredQuestion * 4;

			float growthPercentage = Mathf.Clamp(currentUser.GrowthPoint, 0, totalPoints) / totalPoints;
			float satisfactionPercentage = Mathf.Clamp(currentUser.SatisfactionPoint, 0, totalPoints) / totalPoints;
			float innovationPercentage = Mathf.Clamp(currentUser.InnovationPoint, 0, totalPoints) / totalPoints;
			float currencyPercentage = Mathf.Clamp(currentUser.CurrencyPoint, 0 , totalPoints) / totalPoints;

			view.UpdateSliders(growthPercentage, satisfactionPercentage, 
				innovationPercentage, currencyPercentage);

			view.UpdateScore(currentUser.Score);
			view.ShowSelected(idx);

			if (idx == 0)
				view.ShowTextResult("GROWTH POINT", currentUser.GrowthPoint, growthPercentage * 100);
			else if (idx == 1)
				view.ShowTextResult("SATISFACTION POINT", currentUser.SatisfactionPoint, satisfactionPercentage * 100);
			else if (idx == 2)
				view.ShowTextResult("INNOVATION POINT", currentUser.InnovationPoint, innovationPercentage * 100);
			else if (idx == 3)
				view.ShowTextResult("CURRENCY POINT", currentUser.CurrencyPoint, currencyPercentage * 100);
		}
	}

	public void ShowNextMenu()
	{
		uiController.Show(nextState);
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
		LoadingManager.instance.FadeIn(() =>
		{
			LoadSceneManager.instance.LoadSceneLevel(1,
				UnityEngine.SceneManagement.LoadSceneMode.Single,
				() =>
				{
					LoadingManager.instance.FadeOut();
				});
		});
	}
	private void OnDestroy()
	{
		RemoveListener();
	}
}
