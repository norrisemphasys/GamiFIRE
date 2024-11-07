using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinMenuController : BasicController
{
	private SpinMenuView view;

	[SerializeField] PrizeData[] prizeData;

	[SerializeField] int sectionCount = 8;
	[SerializeField] int spinSmoothness = 10;
	[SerializeField] int cycle = 5;

	List<float> _prizePercentage = new List<float>();

	bool _isSpinning;
	float _totalAngle;
	float _zRotation = 0;
	int _randomValue;
	int _prevIndex = 0;

	void Awake()
	{
		view = GetComponent<SpinMenuView>();
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
		_isSpinning = false;
		_totalAngle = 360 / sectionCount;

		view.EnableSpinButton(true);
		view.ShowParticleSelected(false);

		InitializePrizePercentage();
		InitializeDataInfo();
	}

	public void ShowNextMenu()
	{
		uiController.Show(nextState);
	}

	void AddListener()
	{
		view.btnSpin.onClick.AddListener(OnClickSpin);
	}

	void RemoveListener()
	{
		view.btnSpin.onClick.RemoveListener(OnClickSpin);
	}

	void InitializePrizePercentage()
    {
		_prizePercentage.Clear();

		for (int i = 0; i < prizeData.Length; i++)
			_prizePercentage.Add(prizeData[i].percentage);
    }

	void InitializeDataInfo()
    {
		for (int i = 0; i < prizeData.Length; i++)
			view.SetPrizeData(i, prizeData[i]);

		view.SetResultData(prizeData[Random.Range(0, prizeData.Length)]);
	}

	private void OnClickSpin()
	{
		Audio.PlaySFXClick2();

		if (!_isSpinning)
			StartCoroutine(Spin());
	}

	private IEnumerator Spin()
	{
		float showDelay = 2.5f;

		view.EnableSpinButton(false);
		view.ShowParticleSelected(false);

		_isSpinning = true;

		_prizePercentage.Clear();
		for (int x = 0; x < prizeData.Length; x++)
			_prizePercentage.Add(prizeData[x].percentage);

		int itemIndex = Utils.GetPrizeByProbability(_prizePercentage);
		_randomValue = ((sectionCount * cycle) * spinSmoothness) +
			((itemIndex * spinSmoothness) - (_prevIndex * spinSmoothness));
		float increment = 0;
		float audioTime = 10;
		for (int i = 0; i < _randomValue; i++)
		{
			float angle = _totalAngle / spinSmoothness;

			_zRotation += angle;
			view.SetSpinnerRotation(_zRotation);

			float at = i % audioTime;

			if (at == 0)
				Audio.PlaySFXSpin();

			float time = (float)i / (float)_randomValue;
			float interval = Mathf.Lerp(0.01f, 0.1f, time * time);
			float angleSpeed = Mathf.Lerp(1f, 0.5f, time);

			increment += angleSpeed;
			float sin = Mathf.Sin(increment);

			view.SetArrowRotation(sin * 5f);
			view.SetResultData(prizeData[Random.Range(0, prizeData.Length)]);
			yield return new WaitForSeconds(interval);
		}

		view.SetArrowRotation(0f);

		_prevIndex = itemIndex;
		_isSpinning = false;

		PrizeData data = prizeData[itemIndex];

		view.SetResultData(data);
		view.ShowParticleSelected(true);

		Audio.PlaySFXSpinResult();

		yield return new WaitForSeconds(showDelay);

		ScoreManager.instance.SetBonus(data.prize, data.type);

		if (data.type == PrizeType.SCENARIO_QUESTION)
        {
			gameManager.sceneController.HasPrize = false;
			OnClickDefault(UIState.SQ_MENU);
		}
		else if(data.type == PrizeType.REPEAT_SPIN)
        {
			gameManager.sceneController.HasPrize = false;
			view.EnableSpinButton(true);
		}
		else
        {
			gameManager.sceneController.HasPrize = true;
			gameManager.sceneController.currentPrizeData = data;
			Debug.LogError("sdfsdfsdfsdsdf");
			// Temporary
			OnClickDefault(UIState.ISLAND_MENU);
		}

		yield return null;
	}
	private void OnDestroy()
	{
		RemoveListener();
	}
}

[System.Serializable]
public class PrizeData
{
	public PrizeType type;

	public string name;
	public string description;
	public string shortDescription;
	public float percentage;
	public Sprite sprite;

	public float prize;
}

public enum PrizeType
{
	COIN_BOOSTER,
	SCENARIO_QUESTION,
	GROWTH_POINT,
	INNOVATION_POINT,
	SATISFACTION_POINT,
	REPEAT_SPIN,
	NONE
}