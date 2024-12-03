using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MGEnemy : MonoBehaviour
{
    public MGRoad.RoadSide side;
    [SerializeField] Sprite[] icons;
    [SerializeField] SpriteRenderer icon;

    private bool _startMove = false;
    private Rigidbody2D r2D;

    public float speed = 0.5f;
    private float _maxBounds = 4;

    private Animator animator;

    private void Awake()
    {
        r2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        
    }

    private void FixedUpdate()
    {
        if(_startMove)
        {
            Vector2 pos = r2D.position;

            pos.x += ((side == MGRoad.RoadSide.LEFT ? 1 : -1) * speed) * Time.fixedDeltaTime;
            float bounds = _maxBounds;

            if (pos.x > bounds)
                pos.x = -_maxBounds;
            else if (pos.x < -bounds)
                pos.x = _maxBounds;

            r2D.position = pos;
        }
    }

    public void SetInitPosition(Vector2 pos)
    {
        r2D.position = pos;
        r2D.velocity = Vector2.zero;
        r2D.angularVelocity = 0f;
    }

    public void Move()
    {
        _startMove = true;

        animator.Play("Idle");
    }

    public void SetIcon(int idx)
    {
        icon.sprite = icons[idx];
    }

    public void SetSide()
    {
        icon.flipX = side == MGRoad.RoadSide.RIGHT ? true : false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag.Equals("Player"))
        {
            GameEvents.OnGameOverMiniGame.Invoke(false);
            Debug.LogError("hit enemy");
        }
    }
}
