using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(CreateLanguageID))]
public class CreateLanguageIDEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        var languageBank = (CreateLanguageID)target;

        if (GUILayout.Button("Parse CSV", GUILayout.Height(30)))
        {
            languageBank.CreateID();
        }

        GUILayout.Label("Select an object in the hierarchy view");

        EditorGUILayout.TextField("Input text to create ID: ", languageBank.inpuText);

        GUILayout.Label("Select an object in the hierarchy view");

        EditorGUILayout.TextField("Output text to create ID: ", languageBank.outputText);

        if (GUILayout.Button("Create ID", GUILayout.Height(30)))
        {
            languageBank.CreateTextID();
        }

        this.Repaint();

    }
}
