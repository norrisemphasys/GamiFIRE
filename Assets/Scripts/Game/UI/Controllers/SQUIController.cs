using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SQUIController : BasicController
{
	private SQUIView view;
	ScoreManager scoreManager;

	void Awake()
	{
		view = GetComponent<SQUIView>();
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

		gameManager.sceneController.UpdateQuestionCounter();
	}

	public override void Initialize()
	{
		scoreManager = ScoreManager.instance;

		QuestionSO question = gameManager.sceneController.GetCurrentQuestion();

		view.SetTextTitle(question.questionTitle);
		view.SetTextQuestion(question.question);

		for(int i = 0; i < view.btnAnswers.Length; i++)
        {
			AnswerView av = view.btnAnswers[i].GetComponent<AnswerView>();
			AnswerData data = question.answerData[i];

			av.SetData(data.answer, data.growthPoint, data.innovationPoint, 
				data.satisfactionPoint, data.moneyCurrencyPoints);
		}
	}

	public void ShowNextMenu()
	{
		uiController.Show(nextState);
	}

	void AddListener()
	{
		for(int i = 0; i < view.btnAnswers.Length; i++)
			AddButtonListener(i, view.btnAnswers[i], OnClickAnswer);

		view.buttonCollect.onClick.AddListener(OnClickContinue);
	}

	void RemoveListener()
	{
		for (int i = 0; i < view.btnAnswers.Length; i++)
			view.btnAnswers[i].onClick.RemoveAllListeners();

		view.buttonCollect.onClick.RemoveListener(OnClickContinue);
	}

	void OnClickAnswer(int idx)
    {
		Debug.Log("Answer IDX " + idx);

		view.ShowScorePopup(true);

		QuestionSO question = gameManager.sceneController.GetCurrentQuestion();
		AnswerData data = question.answerData[idx];

		scoreManager.AddCurrencyPoint(data.moneyCurrencyPoints);
		scoreManager.AddGrowthPoint(data.growthPoint);
		scoreManager.AddInnovationPoint(data.innovationPoint);
		scoreManager.AddSatisfactionPoint(data.satisfactionPoint);

		view.SetPoints(data.growthPoint, data.innovationPoint, data.satisfactionPoint, data.moneyCurrencyPoints);
	}

	void OnClickContinue()
	{
		view.ShowScorePopup(false);
		OnClickDefault(UIState.ISLAND_MENU);
	}
	private void OnDestroy()
	{
		RemoveListener();
	}
}
