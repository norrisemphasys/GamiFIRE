using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniGame : MonoBehaviour
{
    public GameObject main;
    public GameManager gameManager;

    private void Awake()
    {
        gameManager = GameManager.instance;
    }

    public virtual void OnEnter() { }
    public virtual void OnExit() { }
    public virtual void AddListener() { }
    public virtual void RemoveListener() { }
    public virtual void OnUpdate() { }

    public enum PlatformType
    {
        RED,
        GREEN,
        YELLOW
    }

    public enum Direction
    {
        LEFT,
        RIGHT,
        UP,
        DOWN
    }
}
