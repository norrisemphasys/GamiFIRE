using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;

public class RollMenuView : BasicView
{
    public Button btnRoll;

    [SerializeField] GameObject[] diceFaceGO;
    [SerializeField] RectTransform rectCircle;

    public void Init()
    {
        ShowDiceFace(Random.Range(0, diceFaceGO.Length), true);
    }

    public void ShowDiceFace(int idx, bool init = false) 
    {
        for (int i = 0; i < diceFaceGO.Length; i++)
        {
            diceFaceGO[i].SetActive(false);
            diceFaceGO[i].transform.localScale = Vector3.one;
            diceFaceGO[i].transform.DOKill();
        }

        diceFaceGO[idx].SetActive(true);

        if(!init)
            diceFaceGO[idx].transform.DOScale(Vector3.one * 1.2f, 0.1f);
    }

    public void StartLoading()
    {
        rectCircle.DORotate(Vector3.forward * -360f, 1f, RotateMode.FastBeyond360)
                .SetLoops(-1).SetUpdate(true);
    }

    public void StopLoading()
    {
        rectCircle.DOKill();
        rectCircle.rotation = Quaternion.identity;
    }
}
