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

    MGPlusOneController plusOneController;

    private void Awake()
    {
        if (r2D == null)
            r2D = GetComponent<Rigidbody2D>();
    }

    public void SetEffectController(MGPlusOneController controller)
    {
        plusOneController = controller;
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
        bool isPlayer = collision.tag.Equals("Player");

        if (isCup)
            Audio.PlaySFXMGCollect();

        if(isCup || isObstacle || isPlayer)
            gameObject.SetActive(false);

        if (isPlayer || isCup)
            AddScoreToUser();

        if(isCup)
            GameEvents.OnDropCollected?.Invoke(1, IsLast);

        if(isObstacle)
            GameEvents.OnLifeRemove?.Invoke(1, IsLast);
    }

    public void AddScoreToUser()
    {
        ScoreManager.instance.AddScore(1);

        switch (type)
        {
            case PickUpType.GROWTH:
                ScoreManager.instance.AddGrowthPoint(1, false);
                break;
            case PickUpType.INNOVATION:
                ScoreManager.instance.AddInnovationPoint(1, false);
                break;
            case PickUpType.SATISFACTION:
                ScoreManager.instance.AddSatisfactionPoint(1, false);
                break;
            case PickUpType.COIN:
                ScoreManager.instance.AddCurrencyPoint(1, false);
                break;
        }

        ShowPlusOneEffect();
    }

    void ShowPlusOneEffect()
    {
        if (plusOneController != null)
            plusOneController.PickUp(type, transform.position);
    }
}

public enum PickUpType
{
    GROWTH = 0,
    INNOVATION,
    SATISFACTION,
    COIN
}