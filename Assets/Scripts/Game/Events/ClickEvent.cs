using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickEvent : MonoBehaviour
{
    public virtual void OnEnter() { }
    public virtual void OnOver() { }
    public virtual void OnExit() { }
    public virtual void OnDown() { }
    public virtual void OnUp() {  }

    void OnMouseEnter()
    {
        OnEnter();
    }

    void OnMouseOver()
    {
        OnOver();
    }

    void OnMouseExit()
    {
        OnExit();
    }
    void OnMouseDown()
    {
        OnDown();
    }

    void OnMouseUp()
    {
        OnUp();
    }
}
