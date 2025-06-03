using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;

public class SpinnerResultView : MonoBehaviour
{
    [SerializeField] Sprite[] banners;

    [SerializeField] TextMeshProUGUI textName;
    [SerializeField] Image iconImage;
    [SerializeField] Image bannerImage;
    [SerializeField] TextMeshProUGUI textShortDescription;
    [SerializeField] TextMeshProUGUI textDescription;

    public void SetResultData(PrizeData data)
    {
        string shortDescriptionTranslate = LanguageManager.instance.GetUITranslatedText(data.shortDescription);
        string descriptionTranslate = LanguageManager.instance.GetUITranslatedText(data.description);
        string nameTranslate = LanguageManager.instance.GetUITranslatedText(data.name);

        iconImage.sprite = data.sprite;
        textShortDescription.text = shortDescriptionTranslate;
        textDescription.text = descriptionTranslate;
        textName.text = nameTranslate;

        iconImage.rectTransform.sizeDelta = new Vector2(data.sprite.rect.size.x * 1.2f, data.sprite.rect.size.y * 1.2f);

        SetBanner(data.type);
    }

    void SetBanner(PrizeType type)
    {
        int idx = (int)type;
        bannerImage.sprite = banners[idx];
    }
}
