using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;

public class MGTIView : MonoBehaviour
{
    public RectTransform panel;
    public GameObject[] steps;

    public void ShowStep(int idx)
    {
        for(int i = 0; i < steps.Length; i++)
            steps[i].SetActive(i == idx);
    }
}
