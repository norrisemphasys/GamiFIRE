using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomizationController : BasicController
{
	private CustomizationView view;

	private PlayerCustomizer _customizer;

	private int _avatarIndex = 0;
	private int _headGearIndex = 0;
	private int _skinIndex = 0;
	private int _styleIndex = 0;

	private bool _isAvatar = true;
	private bool _isHeadGear = false;
	private bool _isSkin = false;
	private bool _isStyle = false;

	void Awake()
	{
		view = GetComponent<CustomizationView>();
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
		gameManager.characterController.Save();

		gameManager.playerController.SetPause(false);
		RemoveListener();
		view.Hide(ShowNextMenu);

		UserManager.instance.SaveUser(() =>
		{
			// Update User to server
		});
	}

	public override void Initialize()
	{
		gameManager.playerController.SetPause(true);
		Audio.PlaySFXPortal();

		_customizer = gameManager.characterController.GetPlayerCustomizer();

		_customizer.SetAvatar(_avatarIndex);
		_customizer.SetHeadGear(_headGearIndex);
		_customizer.SetSkinColor(_skinIndex);
		_customizer.SetStyle(_styleIndex);

		view.toggleAvatar.isOn = true;
	}

	public void ShowNextMenu()
	{
		uiController.Show(nextState);
	}

	void AddListener()
	{
		view.buttonBack.onClick.AddListener(OnClickBack);

		view.buttonBuy.onClick.AddListener(OnClickBuy);
		view.buttonNext.onClick.AddListener(OnClickNext);
		view.buttonPrev.onClick.AddListener(OnClickPrev);
		view.buttonSelect.onClick.AddListener(OnClickSelect);

		view.toggleAvatar.onValueChanged.AddListener(OnToggleAvatar);
		view.toggleHeadGear.onValueChanged.AddListener(OnTogglHeadGear);
		view.toggleSkinColor.onValueChanged.AddListener(OnToggleSkin);
		view.toggleClothes.onValueChanged.AddListener(OnToggleStyle);
	}

	void RemoveListener()
	{
		view.buttonBack.onClick.RemoveListener(OnClickBack);

		view.buttonBuy.onClick.RemoveListener(OnClickBuy);
		view.buttonNext.onClick.RemoveListener(OnClickNext);
		view.buttonPrev.onClick.RemoveListener(OnClickPrev);
		view.buttonSelect.onClick.RemoveListener(OnClickSelect);

		view.toggleAvatar.onValueChanged.RemoveListener(OnToggleAvatar);
		view.toggleHeadGear.onValueChanged.RemoveListener(OnTogglHeadGear);
		view.toggleSkinColor.onValueChanged.RemoveListener(OnToggleSkin);
		view.toggleClothes.onValueChanged.RemoveListener(OnToggleStyle);
	}

	void OnToggleAvatar(bool isOn)
	{
		_isAvatar = isOn;
		view.toggleAvatar.colors = isOn ? view.selectedColorBlock : view.defaultColorBlock;

		UpdateCustomizer();
	}

	void OnTogglHeadGear(bool isOn)
	{
		_isHeadGear = isOn;
		view.toggleHeadGear.colors = isOn ? view.selectedColorBlock : view.defaultColorBlock;

		UpdateCustomizer();
	}

	void OnToggleSkin(bool isOn)
	{
		_isSkin = isOn;
		view.toggleSkinColor.colors = isOn ? view.selectedColorBlock : view.defaultColorBlock;

		UpdateCustomizer();
	}

	void OnToggleStyle(bool isOn)
	{
		_isStyle = isOn;
		view.toggleClothes.colors = isOn ? view.selectedColorBlock : view.defaultColorBlock;

		UpdateCustomizer();
	}

	void OnClickBuy()
	{

	}

	void OnClickNext()
	{
		if (_isAvatar)
			_avatarIndex++;
		if (_isHeadGear)
			_headGearIndex++;
		if (_isSkin)
			_skinIndex++;
		if (_isStyle)
			_styleIndex++;

		UpdateCustomizer();
	}

	void OnClickPrev()
	{
		if (_isAvatar)
			_avatarIndex--;
		if (_isHeadGear)
			_headGearIndex--;
		if (_isSkin)
			_skinIndex--;
		if (_isStyle)
			_styleIndex--;

		if (_avatarIndex < 0)
			_avatarIndex = _customizer.avatarCount;

		if (_headGearIndex < 0)
			_headGearIndex = _customizer.headGearCount;

		if (_skinIndex < 0)
			_skinIndex = _customizer.skinCount;

		if (_styleIndex < 0)
			_styleIndex = _customizer.styleCount;

		UpdateCustomizer();
	}

	void OnClickSelect()
	{

	}

	void UpdateCustomizer()
	{
		if (_isAvatar)
        {
			_customizer.SetAvatar(_avatarIndex);
			UpdateButtons(_customizer.CurrentAvatarData);
		}
			
		if (_isHeadGear)
        {
			_customizer.SetHeadGear(_headGearIndex);
			UpdateButtons(_customizer.CurrentHeadGearData);
		}
			
		if (_isSkin)
        {
			_customizer.SetSkinColor(_skinIndex);
			UpdateButtons(_customizer.CurrentSkinData);
		}
			
		if (_isStyle)
        {
			_customizer.SetStyle(_styleIndex);
			UpdateButtons(_customizer.CurrentStyleData);
		}
	}

	void UpdateButtons(CustomItemSO data)
	{
		view.buttonBuy.gameObject.SetActive(data.isLock);
		if (data.isLock)
        {
			view.buttonSelect.gameObject.SetActive(false);
			view.equippedGO.SetActive(false);
		}
        else
        {
			view.buttonSelect.gameObject.SetActive(!data.isSelected);
			view.equippedGO.SetActive(data.isSelected);
		}
	}

	void OnClickBack()
    {
		OnClickDefault(UIState.PORT_INGAME);
    }

	private void OnDestroy()
	{
		RemoveListener();
	}
}
