using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;


public class SpinMenuView : BasicView
{
    public Button btnSpin;

    [SerializeField] ItemPrizeUIView[] itemView;

    [SerializeField] RectTransform rectSpinner;
    [SerializeField] RectTransform rectArrow;

    [SerializeField] GameObject particleSelected;

    [Header("Info Properties")]

    [SerializeField] Sprite[] banners;

    [SerializeField] TextMeshProUGUI textName;
    [SerializeField] Image iconImage;
    [SerializeField] Image bannerImage;
    [SerializeField] TextMeshProUGUI textShortDescription;
    [SerializeField] TextMeshProUGUI textDescription;

    public void Init()
    {
        ShowParticleSelected(false);
    }

    public void ShowParticleSelected(bool show)
    {
        particleSelected.gameObject.SetActive(show);
    }

    public void SetSpinnerRotation(float rot)
    {
        rectSpinner.eulerAngles = new Vector3(0, 0, rot);
    }

    public void SetArrowRotation(float rot)
    {
        rectArrow.eulerAngles = new Vector3(0, 0, rot);
    }

    public void SetPrizeData(int idx, PrizeData data)
    {
        itemView[idx].SetData(data.sprite, data.shortDescription, data.name, data.type);
    }

    public void SetResultData(PrizeData data)
    {
        iconImage.sprite = data.sprite;
        textShortDescription.text = data.shortDescription;
        textDescription.text = data.description;
        textName.text = data.name;

        iconImage.rectTransform.sizeDelta = new Vector2(data.sprite.rect.size.x * 1.2f, data.sprite.rect.size.y * 1.2f);

        SetBanner(data.type);
    }

    public void EnableSpinButton(bool enable)
    {
        btnSpin.interactable = enable;
    }

    void SetBanner(PrizeType type)
    {
        int idx = (int)type;
        bannerImage.sprite = banners[idx];
    }
}
