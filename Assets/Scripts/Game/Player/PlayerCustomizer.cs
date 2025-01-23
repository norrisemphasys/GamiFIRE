using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PlayerCustomizer : MonoBehaviour
{
    [Header("Objects")]

    [SerializeField]
    private GameObject[] avatars;

    [SerializeField]
    private GameObject[] headGears;

    [SerializeField]
    private SkinColor[] skinColors;

    [SerializeField]
    private Clothes[] clotheStyles;

    [Header("Data")]
    [Space(5)]

    public CustomItemSO[] avatarData;
    public CustomItemSO[] headGearData;
    public CustomItemSO[] skinColorData;
    public CustomItemSO[] clotesStyleData;

    public int currentAvatarIndex => _currentAvatar;
    public int currentHeadGearIndex => _currentHeadGear;
    public int currentSkinIndex => _currentSkinColor;
    public int currentStyleIndex => _currentStyle;

    private int _currentAvatar = 0;
    private int _currentHeadGear = 0;
    private int _currentSkinColor = 0;
    private int _currentStyle = 0;

    public int avatarCount => avatars.Length;
    public int headGearCount => headGears.Length;
    public int skinCount => skinColors.Length;
    public int styleCount => clotheStyles.Length;

    public CustomItemSO CurrentAvatarData => avatarData[_currentAvatar];
    public CustomItemSO CurrentHeadGearData => headGearData[_currentHeadGear];
    public CustomItemSO CurrentSkinData => skinColorData[_currentSkinColor];
    public CustomItemSO CurrentStyleData => clotesStyleData[_currentStyle];

    public void SetAvatar(int idx)
    {
        int id = idx % avatars.Length;

        for (int i = 0; i < avatars.Length; i++)
            avatars[i].SetActive(i == id);

        _currentAvatar = id;
    }

    public void SetHeadGear(int idx)
    {
        int id = idx % headGears.Length;

        for (int i = 0; i < headGears.Length; i++)
            headGears[i].SetActive(i == id);

        _currentHeadGear = id;
    }

    public void SetSkinColor(int idx)
    {
        int id = idx % 3;

        Renderer renderer = avatars[_currentAvatar].GetComponent<Renderer>();
        renderer.sharedMaterial = skinColors[_currentStyle].colors[id];

        _currentSkinColor = id;
    }

    public void SetStyle(int idx)
    {
        int id = idx % 4;

        Renderer renderer = avatars[_currentAvatar].GetComponent<Renderer>();
        renderer.sharedMaterial = clotheStyles[_currentSkinColor].styles[id];

        _currentStyle = id;
    }

    public void LoadPlayerCostume()
    {
        int aIDX = avatarData.ToList().FindIndex(x => x.isSelected);
        int hIDX = headGearData.ToList().FindIndex(x => x.isSelected);
        int cIDX = skinColorData.ToList().FindIndex(x => x.isSelected);
        int sIDX = clotesStyleData.ToList().FindIndex(x => x.isSelected);

        Debug.LogError("aIDX " + aIDX + " hIDX " + hIDX + " cIDX " + cIDX + " sIDX " + sIDX);

        SetAvatar(aIDX);
        SetHeadGear(hIDX);
        SetSkinColor(cIDX);
        SetStyle(sIDX);
    }

    public void ResetData(CustomItemSO[] data)
    {
        for(int i = 0; i < data.Length; i++)
            data[i].isSelected = false;
    }
}

[System.Serializable]
public struct SkinColor
{
    public Material[] colors;
}

[System.Serializable]
public struct Clothes
{
    public Material[] styles;
}