using UnityEngine;
using System.Collections;

public class BasicController : MonoBehaviour
{
	public UIState state;
	public UIState nextState;

	protected UIController uiController;
	protected GameManager gameManager;

    private void Awake()
    {
		InitializeManagers();
	}

    void Start()
	{
		InitializeManagers();
	}

	void InitializeManagers()
    {
		if (gameManager == null)
			gameManager = GameManager.instance;
	}

	public virtual void Initialize()
	{

	}

	public virtual void OnEnter()
	{
		InitializeManagers();
		gameManager.SetGameState(state);
	}

	public virtual void OnExit()
	{

	}

	protected void OnClickDefault(UIState _state, float time = 1)
	{
		nextState = _state;
		uiController.Hide(state);
	}

	public void ShowNextState(UIState _state)
	{
		nextState = _state;

		uiController.Hide(state);
		uiController.Show(nextState);
	}
}
