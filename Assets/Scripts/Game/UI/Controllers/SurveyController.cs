using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Text;

public class SurveyController : BasicController
{
	public int currrentSurvey;

	private SurveyView view;
	public SurveySO[] surveySO;

	private SurveyQuestion[] surveyQuestions;

	private int _currentSurveyIndex = 0;
	private int _maxSurveyCount;

	private string _surveyAnswer;

	public SurveySO GetCurrentSurvey()
    {
		if (gameManager.currentScene == SCENE_TYPE.ISLAND_SCENE)
			currrentSurvey = (int)gameManager.IslandType + 1;
		else
			currrentSurvey = 0;

		Debug.LogError("currrentSurvey" + currrentSurvey);

		return surveySO[currrentSurvey];
	}

	void Awake()
	{
		view = GetComponent<SurveyView>();
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
		GetCurrentSurvey();

		_surveyAnswer = string.Empty;
		surveyQuestions = surveySO[currrentSurvey].surveyQuestion;
		UpdateSurveyView();
	}

	public void ShowNextMenu()
	{
		uiController.Show(nextState);
	}

	void AddListener()
	{
		
	}

	void RemoveListener()
	{
	
	}

	void NextSurvey()
    {
		_currentSurveyIndex++;
    }

	void PreviousSurvey()
    {
		_currentSurveyIndex--;
	}

	void UpdateSurveyView()
	{
		_maxSurveyCount = surveyQuestions.Length;

		string questionTranslate = LanguageManager.instance.GetUITranslatedText(surveyQuestions[_currentSurveyIndex].question);

		string questionText = string.Format("{0}. {1}", _currentSurveyIndex + 1, questionTranslate);
		view.textQuestion.text = questionText;
		int optionsCount = surveyQuestions[_currentSurveyIndex].options.Length;

		foreach (Transform t in view.rectContent)
        {
			if (t != view.rectContent.transform)
            {
				if(t.GetComponent<Toggle>() != null)
					t.GetComponent<Toggle>().onValueChanged.RemoveAllListeners();
				Destroy(t.gameObject);
			}	
        }

		for (int i = 0; i < optionsCount; i++)
        {
			GameObject goSurvey = Instantiate(view.togglePrefab);
			goSurvey.SetActive(true);

			Transform transformSurvey = goSurvey.transform;

			transformSurvey.SetParent(view.rectContent.transform);

			transformSurvey.localPosition = Vector3.zero;
			transformSurvey.localScale = Vector3.one;
			transformSurvey.rotation = Quaternion.identity;

			Toggle toggle = transformSurvey.GetComponent<Toggle>();
			toggle.isOn = false;

			string optionTranslate = LanguageManager.instance.GetUITranslatedText(surveyQuestions[_currentSurveyIndex].options[i]);
			toggle.GetComponentInChildren<TextMeshProUGUI>().text = optionTranslate;

			ToggleGroup toggleGroup = view.rectContent.GetComponent<ToggleGroup>();
			toggle.group = toggleGroup;

			AddToggleListener(i, toggle, OnClickToggle);
		}

		view.textPage.text = string.Format("{0} / {1}", _currentSurveyIndex + 1, _maxSurveyCount);
    }

	void EnableToggle(bool enable)
    {
		foreach (Transform t in view.rectContent)
		{
			if (t != view.rectContent.transform)
			{
				if (t.GetComponent<Toggle>() != null)
					t.GetComponent<Toggle>().interactable = enable;
			}
		}
	}

	void OnClickToggle(bool ison, int idx)
    {
		Utils.Delay(this, () => 
		{
			// Next survey
			Debug.LogError("is on" + ison + " idx " + idx);
			if(ison)
            {
				string comma = _currentSurveyIndex == _maxSurveyCount-1 ? "" : ",";
				_surveyAnswer += string.Format("{0}", idx) + comma;

				if (_currentSurveyIndex < _maxSurveyCount - 1)
                {
					EnableToggle(false);
					NextSurvey();
					UpdateSurveyView();
                }
                else
                {
					// Save survey
					SaveSurvey();
					Debug.LogError("Show Popup Finished survey " + _surveyAnswer);
                }
            }

		}, 0.5f);
    }

	void SaveSurvey()
	{
		LoadingManager.instance.ShowLoader(true);
		User currentUser = UserManager.instance.currentUser;
		if (currentUser != null)
        {
			Survey survey = new Survey
			{
				id = surveySO[currrentSurvey].id,
				title = surveySO[currrentSurvey].title,
				answer = _surveyAnswer,
			};

            DBManager.AddNewSurvey(currentUser, survey, (sucess) =>
            {
                if (sucess)
                {
                    /*SendSurveyEmail(currentUser, "norris@emphasyscentre.com");
					SendSurveyEmail(currentUser, "nicholas@emphasyscentre.com");*/

					LoadingManager.instance.ShowLoader(false);
                    PopupManager.instance.ShowPopup(PopupMessage.InfoPopup("Thank you for completing the survey¡ªwe really appreciate your time and input!", () =>
                    {
                        if (!DBManager.allUsersSurvey.Contains(survey))
                            DBManager.allUsersSurvey.Add(survey);
                        OnClickBack();
                    }));
                }
                else
                {
                    LoadingManager.instance.ShowLoader(false);
                    PopupManager.instance.ShowPopup(PopupMessage.ErrorPopup("There was an error in the server.", () =>
                    {
                        OnClickBack();
                    }));
                }
            });
        }
    }

	void SendSurveyEmail(User user, string email)
	{
		StringBuilder sb = new StringBuilder();

		SurveySO so = surveySO[currrentSurvey];
		string[] answers = _surveyAnswer.Split(',');

		sb.Append("Username:  " + user.Username);
		sb.Append("\n");
		sb.Append("Email: " + user.Email);
		sb.Append("\n\n");

		sb.Append("Survey Title: " + so.title);
		sb.Append("\n\n");

		for (int i = 0; i < so.surveyQuestion.Length; i++)
        {
			int answerIdx = int.Parse(answers[i]);

			string question = so.surveyQuestion[i].question;
			string answer = so.surveyQuestion[i].options[answerIdx];

			sb.Append("Question: " + question);
			sb.Append("\n");
			sb.Append("Answer: " + answer);
			sb.Append("\n");
		}

#if UNITY_EDITOR
		Debug.Log(sb.ToString());
#endif

#if UNITY_WEBGL && !UNITY_EDITOR
		EmailManager.instance.SendEmail(email, "GAMIFIRE Newsletter Info", sb.ToString());
#endif
	}

	void OnClickBack()
    {
		if (gameManager.currentScene == SCENE_TYPE.LOGIN_SCENE)
			OnClickDefault(UIState.GENDER_MENU);
		else if (gameManager.currentScene == SCENE_TYPE.ISLAND_SCENE)
		{
			if (gameManager.previousState == UIState.RESULT_MENU)
			{
				nextState = UIState.NONE;
				gameManager.SetGameState(UIState.RESULT_MENU);
				OnExit();
			}
		}
	}

	private void OnDestroy()
	{
		RemoveListener();
	}
}

[System.Serializable]
public struct SurveyQuestion
{
	public string question;
	public string[] options;
}