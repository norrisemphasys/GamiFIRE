using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;

public class GameInfoView : BasicView
{
    public Button buttonBack;
    public Transform content;
    public Toggle[] toggles;

    public Transform contentPrefab;

    [Header("Selected")]
    public ColorBlock selectedColor;
    [Header("Default")]
    public ColorBlock defaultColor;

    public void Init()
    {

    }
}
