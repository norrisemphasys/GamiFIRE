using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResultController : BasicController
{
	private ResultView view;

	public BadgeInfoSO[] studentBadges;
	public BadgeInfoSO[] employeeBadges;
	public BadgeInfoSO[] farmerBadges;
	public BadgeInfoSO[] businessBadges;

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
			Debug.LogError("user not null");
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
			float currencyPercentage = Mathf.Clamp(currentUser.CurrencyPoint, 0, totalPoints) / totalPoints;

			view.UpdateSliders(growthPercentage, satisfactionPercentage,
				innovationPercentage, currencyPercentage);

			view.UpdateScore(currentUser.Score);
			view.ShowSelected(idx);

			if (idx == 0)
				view.ShowTextResult("GROWTH", currentUser.GrowthPoint, growthPercentage * 100);
			else if (idx == 1)
				view.ShowTextResult("SATISFACTION", currentUser.SatisfactionPoint, satisfactionPercentage * 100);
			else if (idx == 2)
				view.ShowTextResult("INNOVATION", currentUser.InnovationPoint, innovationPercentage * 100);
			else if (idx == 3)
				view.ShowTextResult("CURRENCY", currentUser.CurrencyPoint, currencyPercentage * 100);

			JobType islandType = gameManager.IslandType;
			bool hasGainBadge = false;
			BadgeInfoSO badge = null;

			switch (islandType)
			{
				case JobType.STUDENT:
					hasGainBadge = HasGainBadge(idx, growthPercentage,
						satisfactionPercentage, innovationPercentage, currencyPercentage);
					badge = studentBadges[idx];
					break;
				case JobType.PROFESSIONAL:
					hasGainBadge = HasGainBadge(idx, growthPercentage,
						satisfactionPercentage, innovationPercentage, currencyPercentage);
					badge = employeeBadges[idx];
					break;
				case JobType.AGRICULTRIST:
					hasGainBadge = HasGainBadge(idx, growthPercentage,
						satisfactionPercentage, innovationPercentage, currencyPercentage);
					badge = farmerBadges[idx];
					break;
				case JobType.BUSINESSMAN:
					hasGainBadge = HasGainBadge(idx, growthPercentage,
						satisfactionPercentage, innovationPercentage, currencyPercentage);
					badge = businessBadges[idx];
					break;
			}

			if (hasGainBadge && badge != null)
			{
				// Award Badge
				string message = "You obtained the " + badge.title + " Badge.";
				PopupManager.instance.ShowPopup(PopupMessage.ClaimPopup(message, () =>
				{
					OnClickClaimBadge(badge.badgeID);
				}));
            }
            else
            {
				ShowSurveyMenu();
			}
        }
        
	}

	void ShowSurveyMenu()
    {
		SurveySO surveySO = gameManager.surveyController.GetCurrentSurvey();
		Survey survey = DBManager.allUsersSurvey.Find((x) => x.id == surveySO.id);
		bool isSurveyExisting = survey != null;
		Debug.LogError("Show Survey "  + isSurveyExisting + " id " + surveySO.id);
		if (!isSurveyExisting)
			gameManager.uiController.Show(UIState.SURVEY_MENU);
	}

	void OnClickClaimBadge(string badgeID)
    {
		Debug.LogError("Claim Badge " + badgeID);
		LoadingManager.instance.ShowLoader(true);

		BadgeManager.GetBadge(badgeID, (success)=> 
		{
			if(success)
            {
				DBManager.GetAllUsersBadge(UserManager.instance.currentUser, (success) =>
				{
					LoadingManager.instance.ShowLoader(false);
					PopupManager.instance.ShowPopup(PopupMessage.InfoPopup("You have successfully claimed the badge", () =>
					{
						ShowSurveyMenu();
					}));
				});	
			}
            else
            {
				LoadingManager.instance.ShowLoader(false);
				PopupManager.instance.ShowPopup(PopupMessage.ErrorPopup("There was an error in the server when claiming the badge.", () =>
				{
					ShowSurveyMenu();
				}));
			}		
		});
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

	bool HasGainBadge(float pointPercentage)
    {
		float percentage = pointPercentage * 100;
		bool gainBadge = percentage >= 80;

		return gainBadge;
	}

	bool HasGainBadge(int idx, float growth, float satisfaction, float innovation, float currency)
    {
		if (idx == 0)
			return HasGainBadge(growth);
		else if (idx == 1)
			return HasGainBadge(satisfaction);
		else if (idx == 2)
			return HasGainBadge(innovation);
		else if (idx == 3)
			return HasGainBadge(currency);

		return false;
	}

	private void OnDestroy()
	{
		RemoveListener();
	}
}
