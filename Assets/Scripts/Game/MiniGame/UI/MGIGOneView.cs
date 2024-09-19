using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;

public class MGIGOneView : BasicView
{
    public Button buttonRed;
    public Button buttonYellow;
    public Button buttonGreen;

    [SerializeField] TextMeshProUGUI textTimer;

    public void Init()
    {

    }

    public void SetTimer(float timer)
    {
        textTimer.text = string.Format("{0:#.##} " + "<size=70>s", timer);
    }

    public void ButtonPress(Button button)
    {
        StopCoroutine(EnableButton(button));
        button.interactable = false;
        StartCoroutine(EnableButton(button));  
    }

    IEnumerator EnableButton(Button button)
    {
        yield return new WaitForSeconds(0.05f);
        button.interactable = true;
    }
}
