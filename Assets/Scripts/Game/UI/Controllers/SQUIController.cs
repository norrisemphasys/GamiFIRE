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
	}

	public override void Initialize()
	{
		scoreManager = ScoreManager.instance;

		QuestionSO question = gameManager.sceneController.GetCurrentQuestion();

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
	}

	void RemoveListener()
	{
		for (int i = 0; i < view.btnAnswers.Length; i++)
			view.btnAnswers[i].onClick.RemoveAllListeners();
	}

	void OnClickAnswer(int idx)
    {
		Debug.Log("Answer IDX " + idx);

		QuestionSO question = gameManager.sceneController.GetCurrentQuestion();
		AnswerData data = question.answerData[idx];

		scoreManager.AddCurrencyPoint(data.moneyCurrencyPoints);
		scoreManager.AddGrowthPoint(data.growthPoint);
		scoreManager.AddInnovationPoint(data.innovationPoint);
		scoreManager.AddSatisfactionPoint(data.satisfactionPoint);

		OnClickDefault(UIState.ISLAND_MENU);
	}

	void OnClickContinue()
	{
		OnClickDefault(UIState.ISLAND_MENU);
	}
	private void OnDestroy()
	{
		RemoveListener();
	}
}
