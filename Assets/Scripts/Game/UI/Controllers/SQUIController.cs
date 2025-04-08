using System.Linq;
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
		gameManager.sceneController.UpdateQuestionCounter();
		RemoveListener();
		view.Hide(ShowNextMenu);
		Audio.StopBGMScenarioQuestion();
	}

	public override void Initialize()
	{
		Audio.StopBGMStudentIsland();
		Audio.PlayBGMScenarioQuestion();

		scoreManager = ScoreManager.instance;

		QuestionSO question = gameManager.sceneController.GetCurrentQuestion();

		view.SetTextTitle(question.questionTitle);
		view.SetTextQuestion(question.question);
		view.SetPoint(gameManager.SelectedPointIndex);

		question.answerData.Shuffle();

		for (int i = 0; i < view.btnAnswers.Length; i++)
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
		view.buttonInfo.onClick.AddListener(OnClickInfo);
		view.buttonPointInfo.onClick.AddListener(OnClickPointInfo);
	}

	void RemoveListener()
	{
		for (int i = 0; i < view.btnAnswers.Length; i++)
			view.btnAnswers[i].onClick.RemoveAllListeners();

		view.buttonCollect.onClick.RemoveListener(OnClickContinue);
		view.buttonInfo.onClick.RemoveListener(OnClickInfo);
		view.buttonPointInfo.onClick.RemoveListener(OnClickPointInfo);
	}

	void OnClickInfo()
    {
		gameManager.uiController.Show(UIState.GAMEINFO_MENU);
    }

	void OnClickPointInfo()
    {
		gameManager.uiController.Show(UIState.POINTS_MENU);
	}

	void OnClickAnswer(int idx)
    {
		Debug.Log("Answer IDX " + idx);
		int sidx = gameManager.SelectedPointIndex;

		view.ShowScorePopup(true, sidx);

		QuestionSO question = gameManager.sceneController.GetCurrentQuestion();
		AnswerData data = question.answerData[idx];

		scoreManager.AddCurrencyPoint(data.moneyCurrencyPoints);
		scoreManager.AddGrowthPoint(data.growthPoint);
		scoreManager.AddInnovationPoint(data.innovationPoint);
		scoreManager.AddSatisfactionPoint(data.satisfactionPoint);

		view.SetPoints(data.growthPoint, data.innovationPoint, data.satisfactionPoint, data.moneyCurrencyPoints);
		gameManager.sceneController.questionController.currentQuestionCount++;

		if(sidx == 0 && data.growthPoint == 4)
        {
			scoreManager.AddCoin(10);
			PopupManager.instance.ShowNotification("Congratualtions! You got an extra 10 coins for choosing the right answer!");
        }
		else if(sidx == 1 && data.satisfactionPoint == 4)
        {
			scoreManager.AddCoin(10);
			PopupManager.instance.ShowNotification("Congratualtions! You got an extra 10 coins for choosing the right answer!");
		}
		else if (sidx == 2 && data.innovationPoint == 4)
		{
			scoreManager.AddCoin(10);
			PopupManager.instance.ShowNotification("Congratualtions! You got an extra 10 coins for choosing the right answer!");
		}
		else if (sidx == 3 && data.moneyCurrencyPoints == 4)
		{
			scoreManager.AddCoin(10);
			PopupManager.instance.ShowNotification("Congratualtions! You got an extra 10 coins for choosing the right answer!");
		}

		Audio.PlaySFXStageClick();
	}

	void OnClickContinue()
	{
		view.ShowScorePopup(false);
		OnClickDefault(UIState.ISLAND_MENU);

		Audio.PlaySFXCoin();
	}
	private void OnDestroy()
	{
		RemoveListener();
	}
}
