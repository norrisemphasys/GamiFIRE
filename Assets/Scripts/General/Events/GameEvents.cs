using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public static class GameEvents
{
    public static readonly Evt<float> OnChangeTerrainSpeed = new Evt<float>();
    public static readonly Evt<bool> OnStartIslandTripGame = new Evt<bool>();
    public static readonly Evt<int> OnCoinCollected = new Evt<int>();
    public static readonly Evt OnSelectObject = new Evt();

    public static readonly Evt<User> OnUpdateUser = new Evt<User>();


    //MINI GAME CONTROLLS

    public static readonly Evt<MiniGame.PlatformType> OnPressA = new Evt<MiniGame.PlatformType>();
    public static readonly Evt<MiniGame.PlatformType> OnPressS = new Evt<MiniGame.PlatformType>();
    public static readonly Evt<MiniGame.PlatformType> OnPressD = new Evt<MiniGame.PlatformType>();

    public static readonly Evt<Vector3> OnMouseMove = new Evt<Vector3>();

    public static readonly Evt<bool> OnGameOverMiniGame = new Evt<bool>();
    public static readonly Evt<int> OnMovePlayerCount = new Evt<int>();

    public static readonly Evt<int, bool> OnDropCollected = new Evt<int, bool>();
    public static readonly Evt<int, bool> OnLifeRemove = new Evt<int, bool>();
}

public class Evt
{
    private event Action _action = delegate { };
    public void Invoke() { _action?.Invoke(); }
    public void AddListener(Action listener) { _action += listener; }
    public void RemoveListener(Action listener) { _action -= listener; }
}

public class Evt<T>
{
    private event Action<T> _action = delegate { };
    public void Invoke(T param) { _action?.Invoke(param); }
    public void AddListener(Action<T> listener) { _action += listener; }
    public void RemoveListener(Action<T> listener) { _action -= listener; }
}

public class Evt<T,A>
{
    private event Action<T,A> _action = delegate { };
    public void Invoke(T param1, A param2) { _action?.Invoke(param1, param2); }
    public void AddListener(Action<T,A> listener) { _action += listener; }
    public void RemoveListener(Action<T,A> listener) { _action -= listener; }
}