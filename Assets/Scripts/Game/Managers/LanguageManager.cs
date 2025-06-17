using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LanguageManager : MonoSingleton<LanguageManager>
{
    [SerializeField] LanguageBank uiTextBank;
    [SerializeField] private LanguageType _currentLanguage = LanguageType.ENGLISH;
    public LanguageType currentLanguage { get { return _currentLanguage; } }

    public void SetCurrentLanguage(LanguageType lang) 
    { 
        _currentLanguage = lang;
        GameEvents.OnChangeLanguage?.Invoke();
    }

    public override void Init()
    {
        base.Init();

        if(uiTextBank != null)
            uiTextBank.ParseCSV();
    }

    public string GetUITranslatedText(string text)
    {
        if (_currentLanguage == LanguageType.ENGLISH)
            return text;

        //string id = TextUtil.GetUniqueTextToSpeechFilename(text +'\r');
        string id = TextUtil.GetUniqueTextToSpeechFilename(text);

        string translatedText = uiTextBank.GetTextByID(id, _currentLanguage);
#if UNITY_EDITOR
        //Debug.LogError("ID " + id + " Orig " + text + " Translated Text " + translatedText);
#endif

        if (string.IsNullOrEmpty(translatedText))
            return text;

        string cleanText = translatedText.Replace("\r", "");
        return cleanText;
    }

    private void OnDestroy()
    {
        GameEvents.OnChangeLanguage.RemoveAllListener();
    }
}
