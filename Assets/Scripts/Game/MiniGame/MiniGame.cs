using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniGame : MonoBehaviour
{
    [SerializeField] MGPlayer player;
    [SerializeField] MGPlatformController platformController;

    int moveCounter = 0;

    void Awake()
    {
        
    }

    // Start is called before the first frame update
    void Start()
    {
        platformController.Init();

        Transform platform = platformController.platformList[0].transform;
        player.SetPlayerPosition(platform.position);

        moveCounter = 1;
    }

    // Update is called once per frame
    void Update()
    {
#if UNITY_EDITOR
        if (Input.GetKeyDown(KeyCode.M))
            MovePlayer(PlatformType.GREEN);
#endif
    }

    public void MovePlayer(PlatformType type)
    {
        Debug.Log("moveCounter " + moveCounter);
        moveCounter = Mathf.Clamp(moveCounter, 0, platformController.maxCountBeforeLooping + 1);
        MGPlatform mgPlatform = platformController.GetPlatformByIndex(moveCounter);
      
        if(type != mgPlatform.platformType)
        {
            // Game Over
            platformController.UpdatePlatformPositions();
            platformController.SpawnPlatform(true);
        }
        else
        {
            platformController.UpdatePlatformPositions();
            platformController.SpawnPlatform(true);
        }

        float sidePosition = platformController.GetXPositionByType(mgPlatform.platformType) * platformController.sideDistance;
        player.SetPlayerPosition(new Vector3(sidePosition, mgPlatform.transform.position.y, 0));

        player.MoveAnimation();
        mgPlatform.MoveAnimation();

        moveCounter++;
    }

    public enum PlatformType
    {
        RED,
        GREEN,
        YELLOW
    }
}
