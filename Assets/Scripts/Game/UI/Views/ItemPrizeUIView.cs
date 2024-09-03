using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ItemPrizeUIView : MonoBehaviour
{
    [SerializeField] Sprite[] sprBanners;
    [SerializeField] Image bannerImage;
    [SerializeField] Image iconImage;
    [SerializeField] TextMeshProUGUI textShortDescription;
    [SerializeField] TextMeshProUGUI textName;

    public void SetData(Sprite icon, string desc, string name, PrizeType type)
    {
        iconImage.sprite = icon;
        textShortDescription.text = desc;
        textName.text = name;

        iconImage.rectTransform.sizeDelta = new Vector2(icon.rect.size.x * 0.6f, icon.rect.size.y * 0.6f);

        SetBanner(type);
    }

    void SetBanner(PrizeType type)
    {
        int idx = (int)type;
        bannerImage.sprite = sprBanners[idx];
    }
}
