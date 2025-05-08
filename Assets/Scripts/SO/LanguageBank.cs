using AYellowpaper.SerializedCollections;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "SO/Language/Bank", order = 1)]
public class LanguageBank : ScriptableObject
{
    [SerializeField] TextAsset csvFile;
    [SerializedDictionary("ID", "Languages")]
    public SerializedDictionary<string, SerializedDictionary<LanguageType, Language>> translationDictionary =
        new SerializedDictionary<string, SerializedDictionary<LanguageType, Language>>();

    public void ParseCSV()
    {
        ClearDictionary();
#if UNITY_EDITOR
        Debug.Log("Parse CSV");
#endif
        string[] data = csvFile.text.Split('\n');

        foreach (string rows in data)
        {
            string[] row = rows.Split(new string[] { ",*" }, StringSplitOptions.None);

            string idx = row[0];
            SerializedDictionary<LanguageType, Language> translation =
                new SerializedDictionary<LanguageType, Language>();

            for (int i = 0; i < row.Length; i++)
            {
                if (i != 0)
                {
                    Language language = new Language
                    {
                        text = row[i]
                    };

                    LanguageType type = (LanguageType)i;
                    translation.Add(type, language);
#if UNITY_EDITOR
                    Debug.Log("idx " + idx + " type " + type + " text " + language.text);
#endif
                }
            }

            if(!translationDictionary.ContainsKey(idx))
                translationDictionary.Add(idx, translation);
        }
    }

    public void ClearDictionary()
    {
        translationDictionary.Clear();
    }

    public string GetTextByID(string id, LanguageType type)
    {
        Debug.LogError("ID : " + id);
        string text = null;
        if(translationDictionary.ContainsKey(id))
        {
            var translations = translationDictionary[id];
            if(translations.ContainsKey(type))
            {
                Language lang = translations[type];
                text = lang.text;
            }
        }
        return text;
    }
}


[Serializable]
public class Language
{
    public string text;
}
