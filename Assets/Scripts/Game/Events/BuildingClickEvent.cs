using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingClickEvent : ClickEvent
{
    [SerializeField] Renderer meshRenderer;

    private void Awake()
    {
        meshRenderer = GetComponent<Renderer>();
    }

    public override void OnEnter()
    {
        base.OnEnter();
    }

    public override void OnExit()
    {
        base.OnExit();
    }

    public override void OnDown()
    {
        base.OnDown();
    }
}
