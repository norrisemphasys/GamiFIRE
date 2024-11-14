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

    private void Awake()
    {
        dialogueTransform = transform;
        Show(false, Vector3.zero);
    }

    public void Show(bool show, Vector3 pos, Transform parent = null) 
    {
        if(parent != null)
            dialogueTransform.SetParent(parent);
        dialogueTransform.localPosition = pos;
        dialogueTransform.localRotation = Quaternion.Euler(new Vector3(0f, 180f, 0f));
        dialoguePanel.gameObject.SetActive(show);

        dialogueText.text = dialogueMessages[Random.Range(0, dialogueMessages.Length)];
    }
}
