using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextGetter : MonoBehaviour
{
    [SerializeField] string fileName;
    List<TextMeshProUGUI> allText = new List<TextMeshProUGUI>();
    List<string> savedText = new List<string>();

    public void GetAllTextMeshPro()
    {
        allText = Utils.GetAllChildComponents<TextMeshProUGUI>(transform);

        for (int i = 0; i < allText.Count; i++)
        {
            string id = TextUtil.GetUniqueTextToSpeechFilename(allText[i].text);
            string file = string.Format("{0},*{1}", id, allText[i].text);

            if(!savedText.Contains(file))
                savedText.Add(file);
        }

        TextUtil.WriteToTextArray(savedText.ToArray(), fileName);
        //Debug.Log(allText[i].text);
    }

    public void AddTextTranslate()
    {
        allText = Utils.GetAllChildComponents<TextMeshProUGUI>(transform);

        for (int i = 0; i < allText.Count; i++)
        {
            TextMeshProTextTranslate translate = allText[i].gameObject.GetComponent<TextMeshProTextTranslate>();
            if (translate == null)
                allText[i].gameObject.AddComponent<TextMeshProTextTranslate>();
        }
    }
}
