using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;

public class LanguageView : BasicView
{
    public Button buttonClose;
    public Button[] buttonLanguage;

    public void Init()
    {

    }
}

public enum LanguageType
{
    ENGLISH = 1,
    SLOVENIAN,
    GREEK,
    ITALIAN,
    DEUTCH
}