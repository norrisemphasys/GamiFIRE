using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(LanguageBank))]
public class LanguageBankEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        var languageBank = (LanguageBank)target;

        if (GUILayout.Button("Parse CSV", GUILayout.Height(30)))
        {
            languageBank.ParseCSV();
        }

        if (GUILayout.Button("Clear", GUILayout.Height(30)))
        {
            languageBank.ClearDictionary();
        }
    }
}
