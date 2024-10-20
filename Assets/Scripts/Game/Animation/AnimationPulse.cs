using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class AnimationPulse : MonoBehaviour
{
    public void StartPulse(bool start)
    {
        transform.DOKill();
        transform.localScale = Vector3.one;
        if (start)
            transform.DOScale(1.2f, 0.3f).SetLoops(-1);
        gameObject.SetActive(start);
    }
}
