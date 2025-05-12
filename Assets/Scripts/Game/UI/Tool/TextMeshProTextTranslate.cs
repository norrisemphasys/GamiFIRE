using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[RequireComponent(typeof(TextMeshProUGUI))]
public class TextMeshProTextTranslate : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI textMeshProText;
    [SerializeField] bool rebuild = false;

    private void Start()
    {
        GameEvents.OnChangeLanguage.AddListener(TranslateText);

        TranslateText();
    }

    void TranslateText()
    {
        textMeshProText = GetComponent<TextMeshProUGUI>();
        string currentText = textMeshProText.text;
        string translatedText = LanguageManager.instance.GetUITranslatedText(currentText);

        textMeshProText.text = translatedText;

        if (rebuild)
            textMeshProText.ForceMeshUpdate();
    }
}
