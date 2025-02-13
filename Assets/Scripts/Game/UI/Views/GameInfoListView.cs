using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameInfoListView : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI textTitle;
    [SerializeField] TextMeshProUGUI textContent;

    public void SetData(string title, string content)
    {
        textTitle.text = title;
        textContent.text = content;
    }
}
