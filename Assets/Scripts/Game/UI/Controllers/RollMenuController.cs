using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RollMenuController : BasicController
{
	private RollMenuView view;

	[SerializeField] private int loopCount = 30;
	private bool startRolling = false;

	int miniGameCount = 0;
	int spinnerCount = 0;

	public List<float> diceProbability = new List<float>();
	bool toggleCustomSpin = true;


	void Awake()
	{
		view = GetComponent<RollMenuView>();
		view.Init();

		toggleCustomSpin = true;
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

		// Limit the percentage appearance of 5 and 6 to 10% and the rest to 20%.
		randIDX = Utils.GetPrizeByProbability(diceProbability);
		view.ShowDiceFace(randIDX);

		int customIndex = RandomizeCustomIndex();
		if (customIndex >= 0 && !toggleCustomSpin)
        {
			randIDX = customIndex - 1;
			view.ShowDiceFace(randIDX);
		}

		int currentCellIndex = gameManager.sceneController.cellController.CurrentCellIndex;
		int maxCellCount = gameManager.sceneController.cellController.maxCellCount;

		int totalMoveCount = currentCellIndex + (randIDX + 1);

		if(totalMoveCount > maxCellCount)
        {
			int diff = maxCellCount - currentCellIndex;
			randIDX = (diff - 1);

			view.ShowDiceFace(randIDX);
		}

		view.StopLoading();
		gameManager.sceneController.MoveCounter = randIDX + 1;
		gameManager.sceneController.TotalMoves += gameManager.sceneController.MoveCounter;

		Audio.PlaySFXRollResult();

		yield return new WaitForSeconds(2f);
		startRolling = false;
		gameManager.sceneController.StartGame = true;

		OnClickDefault(UIState.ISLAND_MENU);

		toggleCustomSpin = !toggleCustomSpin;
	}

	

	int RandomizeCustomIndex()
    {
		int miniGameIndex = gameManager.sceneController.cellController.GetCellTypeIndex(CellType.MINIGAME);
		int spinnerIndex = gameManager.sceneController.cellController.GetCellTypeIndex(CellType.SPINNER);

		if(miniGameIndex > spinnerIndex)
        {
			if(spinnerCount < 1)
			{
				int idx = CustomSpinnerIndex();
				if(idx >= 0)
				{
					spinnerCount++;
					return idx;
                }
            }
            else
            {
				if (miniGameCount < 2)
				{
					int idx = CustomMiniGameIndex();
					if (idx >= 0)
					{
						miniGameCount++;
						return idx;
					}
				}
			}
        }
        else
        {
			if(miniGameCount < 2)
            {
				int idx = CustomMiniGameIndex();
				if (idx >= 0)
				{
					miniGameCount++;
					return idx;
				}
            }
            else
            {
				if (spinnerCount < 1)
				{
					int idx = CustomSpinnerIndex();
					if (idx >= 0)
					{
						spinnerCount++;
						return idx;
					}
				}
			}
        }

		return -1;
	}

    private void OnDestroy()
    {
		RemoveListener();
    }

	int CustomMiniGameIndex()
    {
		int currentCellIndex = gameManager.sceneController.cellController.CurrentCellIndex;
		int miniGameIndex = gameManager.sceneController.cellController.GetCellTypeIndex(CellType.MINIGAME);

		if (miniGameIndex > 0)
		{
			int diff = miniGameIndex - currentCellIndex;
			Debug.LogError("MINIGAME DIFF " + diff);
			if (diff > 0 && diff < 6)
				return diff;
		}

		return -1;
	}

	int CustomSpinnerIndex()
    {
		int currentCellIndex = gameManager.sceneController.cellController.CurrentCellIndex;
		int spinnerIndex = gameManager.sceneController.cellController.GetCellTypeIndex(CellType.SPINNER);

		if(spinnerIndex > 0)
        {
			int diff = spinnerIndex - currentCellIndex;
			Debug.LogError("SPINNER DIFF " + diff);
			if(diff > 0 && diff < 6)
				return diff;
		}

		return -1;
	}
}
