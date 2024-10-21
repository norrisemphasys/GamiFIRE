using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlusOneEffect : MonoBehaviour
{
    [SerializeField] Color[] colors;
    [SerializeField] SpriteRenderer sprRenderer;

    public void PickUp(PickUpType type, Vector3 pos)
    {
        transform.DOKill();
        sprRenderer.DOKill();

        int idx = (int)type;
        sprRenderer.DOFade(1, 0);
        sprRenderer.color = colors[idx];

        SetPosition(pos);
        Animate();
    }

    public void SetPosition(Vector3 pos)
    {
        transform.position = pos;
    }

    public void Animate()
    {
        transform.DOMoveY(2f, 1f);
        sprRenderer.DOFade(0, 1f);
    }
}
