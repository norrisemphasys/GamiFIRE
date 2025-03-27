using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProfileUIController : BasicController
{
	private ProfileUIView view;

	private List<BadgeListView> badgeListView = new List<BadgeListView>();
	void Awake()
	{
		view = GetComponent<ProfileUIView>();
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

		if (gameManager.currentScene == SCENE_TYPE.PORT_SCENE)
			gameManager.playerController.SetPause(false);
	}

	public override void Initialize()
	{
		if (gameManager.currentScene == SCENE_TYPE.PORT_SCENE)
			gameManager.playerController.SetPause(true);

		User currentUser = UserManager.instance.currentUser;
		if (currentUser != null)
			view.SetUser(currentUser);

		ShowBadge();
	}

	public void ShowNextMenu()
	{
		uiController.Show(nextState);
	}

	void AddListener()
	{
		view.buttonClose.onClick.AddListener(OnClickClose);
		view.buttonBadgeClose.onClick.AddListener(OnClickCloseBadge);
	}

	void RemoveListener()
	{
		view.buttonClose.onClick.RemoveListener(OnClickClose);
		view.buttonBadgeClose.onClick.RemoveListener(OnClickCloseBadge);
	}

	void OnClickCloseBadge()
    {
		view.ShowBadgePopup(null, false);
		view.buttonClaimLink.onClick.RemoveAllListeners();
    }

	void OnClickClose()
    {
		switch (gameManager.currentScene)
		{
			case SCENE_TYPE.PORT_SCENE:
				OnClickDefault(UIState.PORT_INGAME);
				break;
			case SCENE_TYPE.ISLAND_SCENE:
				OnClickDefault(UIState.ISLAND_MENU);
				break;
		}
	}

	void ShowBadge()
    {
		badgeListView.Clear();

		foreach (Transform t in view.badgeContent.transform)
        {
			if (t != view.badgeContent.transform)
				GameObject.Destroy(t.gameObject);
        }

		for(int i = 0; i < BadgeManager.badgeList.Count; i++)
        {
			GameObject prefab = GameObject.Instantiate(view.prefabBadgeview);
			prefab.SetActive(true);
			prefab.transform.SetParent(view.badgeContent.transform);

			prefab.transform.localPosition = Vector3.zero;
			prefab.transform.localScale = Vector3.one;
			prefab.transform.localRotation = Quaternion.identity;

			BadgeListView bView = prefab.GetComponent<BadgeListView>();
			bView.SetBadgeData(BadgeManager.badgeList[i]);

			badgeListView.Add(bView);

			AddButtonListener(i, prefab.GetComponent<Button>(), OnClickBadgeView);
        }
    }

	void OnClickBadgeView(int idx)
    {
		view.ShowBadgePopup(badgeListView[idx], true);
		view.buttonClaimLink.onClick.AddListener(() => 
		{
			Application.OpenURL(BadgeManager.badgeList[idx].claimLink);
		});
    }

	private void OnDestroy()
	{
		RemoveListener();
	}
}
