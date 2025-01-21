using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    [SerializeField]
    private CustomItemSO[] avatarData;

    [SerializeField]
    private CustomItemSO[] headGearData;

    [SerializeField]
    private CustomItemSO[] skinColorData;

    [SerializeField]
    private CustomItemSO[] clotesStyleData;

    private int _currentAvatar = 0;
    private int _currentHeadGear = 0;
    private int _currentSkinColor = 0;
    private int _currentStyle = 0;

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