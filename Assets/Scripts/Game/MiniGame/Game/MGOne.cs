using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MGOne : MiniGame
{
    [SerializeField] MGPlayer player;
    [SerializeField] MGPlatformController platformController;
   

    private int _initialMoveCounter = 0;
    private int _moveCounter = 0;

    public override void OnEnter()
    {
        main.SetActive(true);

        gameManager.miniGameController.uiController.ShowCanvas(true);
        gameManager.sceneController.cameraController.SetCamera(CameraType.MINI_GAME);
        gameManager.miniGameController.uiController.Show(UIState.MGMM_MENU);

        platformController.Init();

        Transform platform = platformController.platformList[0].transform;
        player.SetPlayerPosition(platform.position);

        _initialMoveCounter = 1;
        _moveCounter = 0;

        AddListener();
    }

    public override void OnExit()
    {
        RemoveListener(); 

        platformController.ResetPlatforms();
        main.SetActive(false);
        gameManager.miniGameController.uiController.ShowCanvas(false);
    }


    // Update is called once per frame
    void Update()
    {
#if UNITY_EDITOR
        if (Input.GetKeyDown(KeyCode.M))
            MovePlayer(PlatformType.GREEN);
#endif
    }

    public override void AddListener()
    {
        GameEvents.OnPressA.AddListener(MovePlayer);
        GameEvents.OnPressS.AddListener(MovePlayer);
        GameEvents.OnPressD.AddListener(MovePlayer);
    }

    public override void RemoveListener()
    {
        GameEvents.OnPressA.RemoveListener(MovePlayer);
        GameEvents.OnPressS.RemoveListener(MovePlayer);
        GameEvents.OnPressD.RemoveListener(MovePlayer);
    }

    public void MovePlayer(PlatformType type)
    {
        Debug.Log("moveCounter " + _initialMoveCounter);
        _initialMoveCounter = Mathf.Clamp(_initialMoveCounter, 0, platformController.maxCountBeforeLooping + 1);
        MGPlatform mgPlatform = platformController.GetPlatformByIndex(_initialMoveCounter);

        if (type != mgPlatform.platformType)
        {
            // Game Over
            Debug.Log("GAME OVER");
            GameEvents.OnGameOverMiniGame.Invoke(false);
        }
        else
        {
            Audio.PlaySFXMGJump();

            platformController.UpdatePlatformPositions();
            platformController.SpawnPlatform(true);

            ScoreManager.instance.AddScore(1);
        }

        float sidePosition = platformController.GetXPositionByType(type) * platformController.sideDistance;
        player.SetPlayerPosition(new Vector3(sidePosition, mgPlatform.transform.position.y, 0));

        player.MoveAnimation();
        mgPlatform.MoveAnimation();

        _initialMoveCounter++;
        _moveCounter++;

        GameEvents.OnMovePlayerCount?.Invoke(_moveCounter);
    }

    private void OnDestroy()
    {
        RemoveListener();
    }
}
