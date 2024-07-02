using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public static class GameEvents
{
    public static readonly Evt OnSelectObject = new Evt();
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