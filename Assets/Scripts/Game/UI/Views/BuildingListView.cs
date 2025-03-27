using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;

public class BuildingListView : MonoBehaviour
{
    public BuildingData data;

    [SerializeField] TextMeshProUGUI textPrice;
    [SerializeField] TextMeshProUGUI textName;

    [SerializeField] Button buttonBuy;
    [SerializeField] Button buttonSelect;

    private GameObject lockGO;

    private void Awake()
    {
        lockGO = transform.Find("Lock").gameObject;
    }

    private void Start()
    {
        UpdateView();
        AddListener();
    }

    void UpdateView()
    {
        textPrice.text = data.Price.ToString();
        textName.text = data.Name + "\n" + "<#1eb7fd><size=28>" + data.Description;

        buttonBuy.gameObject.SetActive(data.IsLock);
        buttonSelect.gameObject.SetActive(!data.IsLock);

        if (!data.IsLock)
            buttonSelect.gameObject.SetActive(!data.IsSelected);

        lockGO.SetActive(data.IsLock);
    }

    void AddListener()
    {
        buttonBuy.onClick.AddListener(OnClickBuy);
        buttonSelect.onClick.AddListener(OnClickSelect);
    }

    void RemoveListener()
    {
        buttonBuy.onClick.RemoveListener(OnClickBuy);
        buttonSelect.onClick.RemoveListener(OnClickSelect);
    }

    void OnClickBuy()
    {
        User user = UserManager.instance.currentUser;

        if (user != null)
        {
            if(user.Coin >= data.Price)
            {
                data.IsLock = false;
                user.Coin -= data.Price;

                GameEvents.OnCoinUpdate.Invoke(user.Coin);
                UserManager.instance.currentUser = user;

                UpdateView();
            }
            else
            {
                PopupManager.instance.ShowPopup(
                    PopupMessage.InfoPopup("You don't have enough coins to buy this item. Play more to unlock this building.", () =>
                    {
                       
                    })
                );
            }
        }
    }

    void OnClickSelect()
    {
        data.IsSelected = true;
        data.building.SetActive(true);

        GameManager.instance.sceneController.environmentController.hasBuildingSelected = true;
        GameManager.instance.sceneController.environmentController.selectedBuildingData.Add(data);

        UpdateView();
    }

    public void EnableButton(bool enable)
    {
        buttonBuy.interactable = enable;
    }

    private void OnDestroy()
    {
        RemoveListener();
    }
}

[System.Serializable]
public class BuildingData
{
    public int Id;
    public string Name;
    public string Description;
    public bool IsLock;
    public int Price;

    public bool IsSelected;

    public GameObject building;
}