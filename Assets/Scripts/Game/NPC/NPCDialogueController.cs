using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;

public class NPCDialogueController : MonoBehaviour
{
    [SerializeField] RectTransform dialoguePanel;
    [SerializeField] TextMeshProUGUI dialogueText;

    [SerializeField] string[] dialogueMessages;

    Transform dialogueTransform;

    [SerializeField] GameInfoSO[] gameInfos;
    string[] ext = { "is", "means", "implies", "pertains to" };

    private void Awake()
    {
        dialogueTransform = transform;
        Show(false, Vector3.zero);
    }

    public void Show(bool show, Vector3 pos, Transform parent = null) 
    {
        GameInfoSO so = gameInfos[Random.Range(0, gameInfos.Length)];

        if (parent != null)
            dialogueTransform.SetParent(parent);
        dialogueTransform.localPosition = pos;
        dialogueTransform.localRotation = Quaternion.Euler(new Vector3(0f, 180f, 0f));
        dialoguePanel.gameObject.SetActive(show);

        GameInfo info = so.infos[Random.Range(0, so.infos.Count)];

        string titleTranslate = LanguageManager.instance.GetUITranslatedText(info.Title);
        string descriptionTranslate = LanguageManager.instance.GetUITranslatedText(info.Content);
        string didyouKnowTranslate = LanguageManager.instance.GetUITranslatedText("Did you know that");
        string extensionTranslate = LanguageManager.instance.GetUITranslatedText(ext[Random.Range(0, ext.Length)]);

        string message = string.Format("{0} {1} {2} {3}", didyouKnowTranslate, titleTranslate, extensionTranslate, descriptionTranslate);

        dialogueText.text = message;//dialogueMessages[Random.Range(0, dialogueMessages.Length)];
    }
}
