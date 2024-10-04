using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MGDrop : MonoBehaviour
{
    public bool IsLastDrop;

    Rigidbody2D rigidBody2D;
    Transform dropTransform;

    private void Awake()
    {
        rigidBody2D = GetComponent<Rigidbody2D>();
        dropTransform = transform;

        SetAsLastDrop(false);
    }

    public void ResetDrop()
    {
        rigidBody2D.velocity = Vector2.zero;
        rigidBody2D.angularVelocity = 0f;
        rigidBody2D.rotation = 0f;

        SetAsLastDrop(false);
    }

    public void SetGravityScale(float scale)
    {
        rigidBody2D.gravityScale = scale;
    }

    public void SetPosition(Vector3 pos)
    {
        dropTransform.position = pos;
    }

    public void SetAsLastDrop(bool isLast)
    {
        IsLastDrop = isLast;
    }
}
