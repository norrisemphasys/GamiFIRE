using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Events;

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

		uiController = gameManager.uiController;
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
		Debug.LogError("go name " + uiController.gameObject.name);
		uiController.Hide(state);
	}

	protected void OnClickDefault(UIController controller, UIState _state)
    {
		nextState = _state;
		Debug.LogError("go name " + controller.gameObject.name);
		controller.Hide(state);
	}
	protected void AddButtonListener(int idx, Button button, 
		UnityAction<int> callback = null)
    {
		button.onClick.AddListener(() =>
		{
			callback?.Invoke(idx);
		});
    }

	public void ShowNextState(UIState _state)
	{
		nextState = _state;

		uiController.Hide(state);
		uiController.Show(nextState);
	}
}
