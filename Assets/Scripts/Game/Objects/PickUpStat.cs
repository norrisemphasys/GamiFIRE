using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpStat : MonoBehaviour
{
    public bool useRigidBody = false;
    public PickUpType type;

    [SerializeField] GameObject[] powerups;
    private Rigidbody2D r2D;

    public bool IsLast = false;
    private void Awake()
    {
        if (r2D == null)
            r2D = GetComponent<Rigidbody2D>();
    }
    public void Setup(PickUpType type, bool useRigidBody, float gravityScale = 1)
    {
        this.useRigidBody = useRigidBody;
        this.type = type;

        if(r2D == null)
            r2D = GetComponent<Rigidbody2D>();

        if (useRigidBody)
            r2D.gravityScale = gravityScale;
        else
            r2D.gravityScale = 0;

        int idx = (int)type;
        for (int i = 0; i < powerups.Length; i++)
            powerups[i].SetActive(false);
        powerups[idx].SetActive(true);
    }
    public void SetPosition(Vector3 pos, bool useLocal = false)
    {
        if (useLocal)
            transform.localPosition = pos;
        else
            transform.position = pos;
    }
    public void ResetPickup()
    {
        r2D.velocity = Vector2.zero;
    }

    public void SetIfLast(bool last) { IsLast = last; }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        bool isCup = collision.tag.Equals("Cup");
        bool isObstacle = collision.tag.Equals("Obstacle");

        if(!isObstacle)
            Audio.PlaySFXMGCollect();

        if(isCup || isObstacle)
            gameObject.SetActive(false);

        if (collision.tag.Equals("Player") || isCup)
            AddScoreToUser();

        if(isCup)
            GameEvents.OnDropCollected?.Invoke(1, IsLast);

        if(isObstacle)
            GameEvents.OnLifeRemove?.Invoke(1, IsLast);
    }

    public void AddScoreToUser()
    {
        switch (type)
        {
            case PickUpType.GROWTH:
                ScoreManager.instance.AddGrowthPoint(1);
                break;
            case PickUpType.INNOVATION:
                ScoreManager.instance.AddInnovationPoint(1);
                break;
            case PickUpType.SATISFACTION:
                ScoreManager.instance.AddSatisfactionPoint(1);
                break;
            case PickUpType.COIN:
                ScoreManager.instance.AddCoin(1);
                break;
        }
    }
}

public enum PickUpType
{
    GROWTH,
    INNOVATION,
    SATISFACTION,
    COIN
}