using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MGPowerUpController : MonoBehaviour
{
    PoolManager poolManager;

    public void Init()
    {
        poolManager = PoolManager.instance;
    }
    public void SpawnPowerUp(Vector3 pos, PickUpType type, bool useRigidBody, float gravityScale, Transform parent = null, bool islast = false)
    {
        GameObject powerup = poolManager.GetObject("PowerUp");
        PickUpStat ps = powerup.GetComponent<PickUpStat>();

        if (parent != null)
        {
            powerup.transform.SetParent(parent);
            ps.SetPosition(pos, true);
        }
        else
            ps.SetPosition(pos);

        ps.Setup(type, useRigidBody, gravityScale);
        ps.SetIfLast(islast);
    }
    public void ResetPowerUp()
    {
        poolManager.ResetAllObjectList("PowerUp");
    }
}
