using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;

public class PointsView : BasicView
{
    public Button buttonBack;
    public Button buttonSelect;

    public Button[] buttonPoints;
    public Color[] pointsColor;

    [SerializeField] GameObject pointsPanel;
    [SerializeField] GameObject descriptionPanel;

    [SerializeField] GameObject[] content;

    [SerializeField] TextMeshProUGUI textTitle;
    [SerializeField] TextMeshProUGUI textDescription;

    [SerializeField] Image[] frames;

    public void Init()
    {

    }

    public void ShowPointsPanel(bool show) { pointsPanel.SetActive(show); }
    public void ShowDescriptionPanel(bool show) { descriptionPanel.SetActive(show); }

    public void SetDescription(int idx)
    {
        for(int i = 0; i < frames.Length; i++)
            frames[i].color = pointsColor[idx];
        EnableContent(idx);
    }

    public void SetTitle(string title)
    {
        textTitle.text = title;
    }

    public void SetDescription(string desc)
    {
        textDescription.text = desc;
    }

    void EnableContent(int idx) 
    {
        for(int i = 0; i < content.Length; i++)
            content[i].SetActive(idx == i);
    }
}
