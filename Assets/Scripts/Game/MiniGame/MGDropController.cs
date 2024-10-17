using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MGDropController : MonoBehaviour
{
    [SerializeField] Vector2 xBounds;
    [SerializeField] float startYposition;
    [SerializeField] float speed;

    PoolManager poolManager;
    Coroutine coroutine;

    private void Start()
    {
        Init();
    }

    private void Update()
    {
#if UNITY_EDITOR
        if (Input.GetKeyDown(KeyCode.K))
            SpawnEgg();
#endif
    }

    public void Init()
    {
        poolManager = PoolManager.instance;
        coroutine = null;
    }

    public void ResetPool()
    {
        poolManager.ResetAllObjectList("Egg");
    }

    public void SpawnEgg(float gravityScale = 1, bool islast = false)
    {
        Audio.PlaySFXMGDrop();

        GameObject egg = poolManager.GetObject("Egg");
        MGDrop drop = egg.GetComponent<MGDrop>();

        drop.SetPosition(new Vector3(Random.Range(xBounds.x, xBounds.y), startYposition, 0));
        drop.ResetDrop();
        drop.SetGravityScale(gravityScale);
        drop.SetAsLastDrop(islast);
    }

    public void DeployEgg(int count, float delay, float scale, UnityAction callback = null)
    {
        if (coroutine == null)
            coroutine = StartCoroutine(DeployEggEnum(count, delay, scale, callback));
        else
            StopCoroutine(coroutine);
    }

    IEnumerator DeployEggEnum(int count, float delay, float scale, UnityAction callback = null)
    {
        for(int i = 0; i < count; i++)
        {
            yield return new WaitForSeconds(delay);
            bool islast = i >= count-1;
            SpawnEgg(scale, islast);
        }

        callback?.Invoke();
        coroutine = null;
    }
}
