using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[RequireComponent(typeof(TextMeshProUGUI))]
public class TextMeshProTextTranslate : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI textMeshProText;

    private void Start()
    {
        textMeshProText = GetComponent<TextMeshProUGUI>();
        string currentText = textMeshProText.text;
        string translatedText = LanguageManager.instance.GetUITranslatedText(currentText);

        textMeshProText.text = translatedText;
    }
}
