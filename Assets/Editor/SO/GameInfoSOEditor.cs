using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(GameInfoSO))]
public class GameInfoSOEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        var info = (GameInfoSO)target;

        if (GUILayout.Button("Load Data", GUILayout.Height(30)))
        {
            info.LoadText();
        }
    }
}
