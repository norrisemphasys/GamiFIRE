using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RollMenuController : BasicController
{
	private RollMenuView view;

	[SerializeField] private int loopCount = 30;
	private bool startRolling = false;
	void Awake()
	{
		view = GetComponent<RollMenuView>();
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
		OnClickRoll();
	}

	public void ShowNextMenu()
	{
		uiController.Show(nextState);
	}

	void AddListener()
	{
		view.btnRoll.onClick.AddListener(OnClickRoll);
	}

	void RemoveListener()
	{
		view.btnRoll.onClick.RemoveListener(OnClickRoll);
	}

	void OnClickRoll()
    {
		Debug.Log("ROLL");

		if (!startRolling)
			StartCoroutine(RollAnimationEnum());

		Audio.PlaySFXClick2();
    }

	IEnumerator RollAnimationEnum()
    {
		yield return new WaitForSeconds(0.2f);

		startRolling = true;
		view.StartLoading();
		int randIDX = 0;

		for (int i = 0; i < loopCount; i++)
        {
			Audio.PlaySFXRoll();

			float time = i / (float)loopCount;
			float easeTime = Utils.InBounce(time);

			Debug.Log("time " + easeTime);

			randIDX = Random.Range(0, 6);
			view.ShowDiceFace(randIDX);
			yield return new WaitForSeconds(easeTime);
        }

		view.StopLoading();
		gameManager.sceneController.MoveCounter = randIDX + 1;

		Audio.PlaySFXRollResult();

		yield return new WaitForSeconds(2f);
		startRolling = false;
		gameManager.sceneController.StartGame = true;

		OnClickDefault(UIState.ISLAND_MENU);
	}

    private void OnDestroy()
    {
		RemoveListener();
    }
}
