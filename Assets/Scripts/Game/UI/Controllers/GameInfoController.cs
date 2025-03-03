using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInfoController : BasicController
{
	public GameInfoSO[] gameInfos;

	private GameInfoView view;
	private List<GameInfoListView> listView = new List<GameInfoListView>();

	void Awake()
	{
		view = GetComponent<GameInfoView>();
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
		if(gameManager.playerController != null)
			gameManager.playerController.SetPause(false);
		RemoveListener();
		view.Hide(ShowNextMenu);
	}

	public override void Initialize()
	{
		if (gameManager.playerController != null)
        {
			gameManager.playerController.SetPause(true);
			Audio.PlaySFXPortal();
		}
			
		view.toggles[0].isOn = true;
		OnToggle(true, 0);
		Debug.LogError("init game info");
	}

	public void ShowNextMenu()
	{
		uiController.Show(nextState);
	}

	void AddListener()
	{
		for(int i = 0; i < view.toggles.Length; i++)
			AddToggleListener(i, view.toggles[i], OnToggle);

		view.buttonBack.onClick.AddListener(OnClickBack);
	}

	void RemoveListener()
	{
		for (int i = 0; i < view.toggles.Length; i++)
			view.toggles[i].onValueChanged.RemoveAllListeners();

		view.buttonBack.onClick.RemoveListener(OnClickBack);
	}

	void OnClickBack()
	{
		switch (gameManager.currentScene)
		{
			case SCENE_TYPE.PORT_SCENE:
				OnClickDefault(UIState.PORT_INGAME);
				break;
			case SCENE_TYPE.ISLAND_SCENE:
				if (gameManager.previousState == UIState.SQ_MENU)
				{
					nextState = UIState.NONE;
					gameManager.SetGameState(UIState.SQ_MENU);
					OnExit();
				}
                else
                {
					OnClickDefault(UIState.ISLAND_MENU);
				}
			break;
		}
    }

	void OnToggle(bool ison, int idx) 
	{
		Debug.LogError("ison " + ison + " idx " + idx);

		if (ison)
			UpdateGameInfo(idx);

		view.toggles[idx].colors = ison ? view.selectedColor : view.defaultColor;
	}

	void UpdateGameInfo(int idx)
    {
		listView.Clear();
		foreach (Transform t in view.content)
        {
			if (t != view.content)
            {
				listView.Add(t.GetComponent<GameInfoListView>());
				t.gameObject.SetActive(false);
            }
		}

		GameInfo[] infos = gameInfos[idx].infos.ToArray();
		for (int i = 0; i < infos.Length; i++)
        {
			if(i < listView.Count)
            {
				listView[i].gameObject.SetActive(true);
				listView[i].SetData(infos[i].Title, infos[i].Content);
            }
            else
            {
				Transform infoT = GameObject.Instantiate(view.contentPrefab);
				infoT.SetParent(view.content);
				infoT.gameObject.SetActive(true);

				infoT.localScale = Vector3.one;
				infoT.rotation = Quaternion.identity;

				infoT.GetComponent<GameInfoListView>().SetData(infos[i].Title, infos[i].Content);
			}
        }
    }

	private void OnDestroy()
	{
		RemoveListener();
	}
}
