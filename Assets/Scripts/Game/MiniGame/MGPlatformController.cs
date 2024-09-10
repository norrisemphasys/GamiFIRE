using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MGPlatformController : MonoBehaviour
{
    public List<MGPlatform> platformList = new List<MGPlatform>();
    public float forwardDistance;
    public float sideDistance;
    public int maxCountBeforeLooping = 2;
    PoolManager poolManager;

    int currentPlatformCount = 0;
    int initialCount = 10;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Init()
    {
        poolManager = PoolManager.instance;
        currentPlatformCount = 0;
        platformList.Clear();

        for(int i = 0; i < initialCount; i++)
            SpawnPlatform();
    }

    public GameObject SpawnPlatform(bool usePositionLastIndex = false)
    {
        int rand = Random.Range(0, 3);
        GameObject platform = null;

        if(rand == 0)
            platform = poolManager.GetObject("RedPlatform");
        else if(rand == 1)
            platform = poolManager.GetObject("YellowPlatform");
        else if (rand ==2 ) 
            platform = poolManager.GetObject("GreenPlatform");

        if (platform != null)
        {
            MGPlatform mgPlatform = platform.GetComponent<MGPlatform>();
            if (mgPlatform != null)
            {
                if(platformList.Count >= initialCount + maxCountBeforeLooping)
                {
                    //platformList[0].gameObject.SetActive(false);
                    platformList.Remove(platformList[0]);
                }

                float sidePosition = GetXPositionByType(mgPlatform.platformType);
                float forwardPosition = usePositionLastIndex ?
                    platformList[platformList.Count - 1].transform.position.y - forwardDistance :
                    -forwardDistance * currentPlatformCount;
                mgPlatform.SetPosition(new Vector3(sideDistance * sidePosition, forwardPosition, 0));
                platformList.Add(mgPlatform);
            }
                
            currentPlatformCount++;
        }

        return null;
    }

    public void UpdatePlatformPositions()
    {
        for(int i = 0; i < platformList.Count; i++)
        {
            MGPlatform mgPlatform = platformList[i];
            float sidePosition = GetXPositionByType(mgPlatform.platformType);
            float yPosition = mgPlatform.transform.position.y;
            yPosition += forwardDistance;
            mgPlatform.SetPosition(new Vector3(sideDistance * sidePosition, yPosition, 0));
        }
    }

    public float GetXPositionByType(MiniGame.PlatformType type)
    {
        if (type == MiniGame.PlatformType.GREEN)
            return 1;
        else if (type == MiniGame.PlatformType.RED)
            return -1;
        return 0;
    }

    public MGPlatform GetPlatformByIndex(int idx)
    {
        return platformList[idx] != null ? platformList[idx] : null;
    }
}


