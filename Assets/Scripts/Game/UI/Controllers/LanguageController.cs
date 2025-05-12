using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LanguageController : BasicController
{
	private LanguageView view;
	void Awake()
	{
		view = GetComponent<LanguageView>();
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
		for (int i = 0; i < view.buttonLanguage.Length; i++)
		{
			bool focus = i == 0; // Temp Enlish default
			view.buttonLanguage[i].transform.Find("Focus").gameObject.SetActive(focus);
		}
	}

	public void ShowNextMenu()
	{
		uiController.Show(nextState);
	}

	void AddListener()
	{
		for(int i= 0; i < view.buttonLanguage.Length; i++)
			AddButtonListener(i, view.buttonLanguage[i], OnSelectLanguage);
	}

	void RemoveListener()
	{
		for (int i = 0; i < view.buttonLanguage.Length; i++)
			view.buttonLanguage[i].onClick.RemoveAllListeners();
	}

	void OnSelectLanguage(int idx)
    {
		LanguageType type = (LanguageType)idx + 1;

		for (int i = 0; i < view.buttonLanguage.Length; i++)
        {
			bool focus = i == idx;
			view.buttonLanguage[i].transform.Find("Focus").gameObject.SetActive(focus);
        }
		Debug.LogError("TYPE " + type + " IDX " + idx);
		LanguageManager.instance.SetCurrentLanguage(type);

		OnClickDefault(UIState.SPLASH_MENU);
	}

	private void OnDestroy()
	{
		RemoveListener();
	}
}
