using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MGPlusOneController : MonoBehaviour
{
    PoolManager poolManager;
    public void Init()
    {
        poolManager = PoolManager.instance;
    }

    public void PickUp(PickUpType type, Vector3 pos)
    {
        GameObject plusone = poolManager.GetObject("PlusOne");
        PlusOneEffect effect = plusone.GetComponent<PlusOneEffect>();

        if(effect != null)
            effect.PickUp(type, pos);
    }

    public void ResetPlusOne()
    {
        poolManager.ResetAllObjectList("PlusOne");
    }
}
