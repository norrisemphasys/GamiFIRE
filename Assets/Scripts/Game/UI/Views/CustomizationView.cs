using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;

public class CustomizationView : BasicView
{
    public Button buttonBack;
    public Button buttonPrev;
    public Button buttonNext;
    public Button buttonBuy;
    public Button buttonSelect;

    public Toggle toggleAvatar;
    public Toggle toggleHeadGear;
    public Toggle toggleSkinColor;
    public Toggle toggleClothes;

    public GameObject equippedGO;

    [SerializeField]
    private TextMeshProUGUI textName;

    public void Init()
    {

    }
}
