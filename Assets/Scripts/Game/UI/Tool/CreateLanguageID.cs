using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateLanguageID : MonoBehaviour
{
    [SerializeField] TextAsset csvFile;
    public static List<string> textToSpeech = new List<string>();

    public string inpuText;
    public string outputText;

    private void Start()
    {
        CreateID();
    }

    public void CreateID()
    {
        textToSpeech.Clear();
        string[] data = csvFile.text.Split('\n');

        foreach (string rows in data)
        {
            //string[] row = rows.Split(new string[] { ",*" }, StringSplitOptions.None);

            string text = rows;//.Replace("\r", "");
            string id = TextUtil.GetUniqueTextToSpeechFilename(text);
           
            //string file = string.Format("NAME : {0} FILENAME : {1}", text, filename);
            //string file = string.Format("{0} ", text);
            string file = string.Format("{0},{1}", id, text);
            textToSpeech.Add(file);
        }

        TextUtil.WriteToTextArray(textToSpeech.ToArray(), "UITextID");
    }

    public void CreateTextID()
    {
        outputText = TextUtil.GetUniqueTextToSpeechFilename(inpuText);
    }
}
