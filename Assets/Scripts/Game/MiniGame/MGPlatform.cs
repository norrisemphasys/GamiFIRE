using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MGPlatform : MonoBehaviour
{
    public MiniGame.PlatformType platformType;

    Transform platformTransform;
    Animator animaator;

    private void Awake()
    {
        platformTransform = transform;
        animaator = GetComponent<Animator>();
    }

    public void SetPosition(Vector3 pos)
    {
        platformTransform.position = new Vector3(pos.x, pos.y, pos.z);
    }

    public void MoveAnimation()
    {
        animaator.SetTrigger("Move");
    }
}
