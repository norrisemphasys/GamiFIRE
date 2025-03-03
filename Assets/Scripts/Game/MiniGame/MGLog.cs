using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MGLog : MonoBehaviour
{
    public MGRoad.RoadSide side;

    private bool _startMove = false;
    private Rigidbody2D r2D;

    public float speed = 0.5f;
    private float _maxBounds = 4;

    public bool isSafe;
    [SerializeField] SpriteRenderer sprRenderer;

    private void Awake()
    {
        r2D = GetComponent<Rigidbody2D>();
        sprRenderer = GetComponentInChildren<SpriteRenderer>();
    }

    private void FixedUpdate()
    {
        if (_startMove)
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

    public void SetSafe(bool safe)
    {
        isSafe = safe;
        sprRenderer.enabled = safe;
    }

    public void Move()
    {
        _startMove = true;
    }

    public void Stop()
    {
        _startMove = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Player"))
        {
            if(isSafe)
            {
                MGPlayer player = collision.gameObject.GetComponent<MGPlayer>();
                if (player != null)
                    player.transformFollow = transform;
                Debug.LogError("Detect Player");
            }
            else
            {
                MGPlayer player = collision.gameObject.GetComponent<MGPlayer>();
                if (player != null && player.transformFollow == null)
                    GameEvents.OnGameOverMiniGame.Invoke(false);

                Debug.LogError("Player Defeated");
            }
        }
    }
}
