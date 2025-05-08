using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(TextGetter))]
public class TextGetterEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        var info = (TextGetter)target;

        if (GUILayout.Button("Get Text", GUILayout.Height(30)))
        {
            info.GetAllTextMeshPro();
        }

        if (GUILayout.Button("Add Text Translate Script", GUILayout.Height(30)))
        {
            info.AddTextTranslate();
        }
    }
}
