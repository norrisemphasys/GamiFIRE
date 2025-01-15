using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class LeaderboardController : BasicController
{
	private LeaderboardView view;
	private List<User> userList = new List<User>();

	[SerializeField] private bool useSampleData = false;

	void Awake()
	{
		view = GetComponent<LeaderboardView>();
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
		gameManager.playerController.SetPause(false);
		RemoveListener();
		view.Hide(ShowNextMenu);
	}

	public override void Initialize()
	{
		gameManager.playerController.SetPause(true);
		Audio.PlaySFXPortal();

		UpdateLeaderboard();
	}

	public void ShowNextMenu()
	{
		uiController.Show(nextState);
	}

	void AddListener()
	{
		view.buttonUpdate.onClick.AddListener(OnClickUpdate);
		view.buttonBack.onClick.AddListener(OnClickBack);
	}

	void RemoveListener()
	{
		view.buttonUpdate.onClick.RemoveListener(OnClickUpdate);
		view.buttonBack.onClick.RemoveListener(OnClickBack);
	}

	void UpdateLeaderboard()
    {
		LoadingManager.instance.ShowLoader(true);

		if (useSampleData)
		{
			CreateSampleUserData();

			ArrangeUserList();
			PopuplateLeaderboardList();
			SetUserRank();

			LoadingManager.instance.ShowLoader(false);
		}
		else
		{
			userList.Clear();

			DBManager.GetAllUsersByToken((users) =>
			{
				foreach (User user in users)
					userList.Add(user);

				Utils.Delay(this, () => 
				{
					ArrangeUserList();
					PopuplateLeaderboardList();
					SetUserRank();

					LoadingManager.instance.ShowLoader(false);
				}, 1f);
			});
		}
	}

	void OnClickUpdate()
    {
		UpdateLeaderboard();
	}

	void OnClickBack()
	{
		OnClickDefault(UIState.PORT_INGAME);
	}

	private void OnDestroy()
	{
		RemoveListener();
	}

	void ArrangeUserList()
    {
		userList = userList.OrderByDescending((x)=> x.Score).ToList();
    }

	void PopuplateLeaderboardList()
    {
		foreach(Transform t in view.parent)
        {
			if (t != view.parent)
				Destroy(t.gameObject);
        }

		for(int i = 0; i < userList.Count; i++)
        {
			GameObject list = GameObject.Instantiate(view.leaderboardPrefab);
			list.transform.SetParent(view.parent);
			list.transform.localScale = Vector3.one;

			LeaderboardListView boardView = list.GetComponent<LeaderboardListView>();
			boardView.SetUp(i, userList[i]);

			list.SetActive(true);
		}
    }

	void SetUserRank()
    {
		User currentUser = UserManager.instance.currentUser;

		if(currentUser != null)
        {
			User user = userList.Find( (user)=> user.ID == currentUser.ID);
			int idx = userList.IndexOf(user);
			view.SetupRank(idx, currentUser);
		}
    }

	void CreateSampleUserData()
    {
		userList.Clear();
		int userCount = UnityEngine.Random.Range(10, 20);

		for(int i = 0; i < userCount; i++)
        {
			User newUser = new User
			{
				ID = System.Guid.NewGuid().ToString(),
				Username = "Test User " + i.ToString(),
				Email = "test" + i.ToString() + "@test.com",
				Password = Utils.GetMD5Hash("1234567890" + i.ToString()),
				isAnExistingAccount = true,

				JobType = UnityEngine.Random.Range(0, 3),
				Gender = UnityEngine.Random.Range(0, 2),
				Coin = 100,
				Score = UnityEngine.Random.Range(100, 1000),
				GrowthPoint = 100,
				InnovationPoint = 100,
				CurrencyPoint = 100,
				SatisfactionPoint = 100,
				Costume = ""
			};

			userList.Add(newUser);
		}

		User currentUser = UserManager.instance.currentUser;
		if (currentUser != null)
			userList.Add(currentUser);
		else
        {
			UserManager.instance.CreateAndEditUserData();
			currentUser = UserManager.instance.currentUser;
			userList.Add(currentUser);
		}
	}

}
