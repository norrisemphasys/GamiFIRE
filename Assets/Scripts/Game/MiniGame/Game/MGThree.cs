using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MGThree : MiniGame
{
    [SerializeField] MGPlayer player;
    [SerializeField] MGRoadController roadController;

    [SerializeField] Vector3 playerInitPosition;

    private int _moveCount = 0;
    private int _moveLimitCount = 0;

    private int _maxMoveLimit = 3;

    public override void OnEnter()
    {
        main.SetActive(true);

        gameManager.miniGameController.uiController.ShowCanvas(true);
        gameManager.sceneController.cameraController.SetCamera(CameraType.MINI_GAME);
        gameManager.miniGameController.uiController.Show(UIState.MGMM_MENU);

        roadController.Init();
        player.SetPlayerPosition(playerInitPosition);

        _moveCount = 0;
        _moveLimitCount = 0;

        AddListener();
    }

    public override void OnExit()
    {
        RemoveListener();

        roadController.ResetPool();
        main.SetActive(false);
        gameManager.miniGameController.uiController.ShowCanvas(false);
    }

    public override void AddListener()
    {
        GameEvents.OnPressLeft.AddListener(MovePlayer);
        GameEvents.OnPressRight.AddListener(MovePlayer);
        GameEvents.OnPressUp.AddListener(MovePlayer);
        GameEvents.OnPressDown.AddListener(MovePlayer);

        GameEvents.OnReleaseLeft.AddListener(OnReleaseMove);
        GameEvents.OnReleaseRight.AddListener(OnReleaseMove);
    }

    public override void RemoveListener()
    {
        GameEvents.OnPressLeft.RemoveListener(MovePlayer);
        GameEvents.OnPressRight.RemoveListener(MovePlayer);
        GameEvents.OnPressUp.RemoveListener(MovePlayer);
        GameEvents.OnPressDown.RemoveListener(MovePlayer);

        GameEvents.OnReleaseLeft.RemoveListener(OnReleaseMove);
        GameEvents.OnReleaseRight.RemoveListener(OnReleaseMove);
    }

    private void OnDestroy()
    {
        RemoveListener();
    }

    bool _moveOnce = false;

    void OnReleaseMove(MiniGame.Direction dir)
    {
        _moveOnce = false;
    }

    public void MovePlayer(MiniGame.Direction dir)
    {
        Debug.LogError("DIRECTION " + dir);
        Vector3 playerPos = player.transform.position;
        float moveOffset = 1.25f;

        bool isRiding = player.transformFollow != null;

        switch (dir)
        {
            case Direction.LEFT:

                if(isRiding)
                {
                    if(!_moveOnce)
                    {
                        playerPos.x -= 1;
                        _moveOnce = true;
                        player.transformFollow = null;
                    }
                }else
                    playerPos.x -= 0.005f;

                break;
            case Direction.RIGHT:

                if (isRiding)
                {
                    if (!_moveOnce)
                    {
                        playerPos.x += 1;
                        _moveOnce = true;
                        player.transformFollow = null;
                    }
                }
                else
                    playerPos.x += 0.005f;

                break;
            case Direction.UP:

                player.transformFollow = null;

                if(_moveLimitCount > _maxMoveLimit)
                {
                    roadController.MoveRoad(1);
                    roadController.SpawnRoad();
                }
                else
                    playerPos.y += moveOffset;

                _moveLimitCount++;

                if(_moveLimitCount > _moveCount)
                {
                    _moveCount++;
                    ScoreManager.instance.AddScore(1);
                    GameEvents.OnMovePlayerCount.Invoke(_moveCount);
                }

                break;

            case Direction.DOWN:

                player.transformFollow = null;
               
                if(_moveLimitCount > _maxMoveLimit)
                    roadController.MoveRoad(-1);
                else
                    playerPos.y -= moveOffset;

                _moveLimitCount--;

                playerPos.y = Mathf.Clamp(playerPos.y, -5.65f, 100f);

                break;
        }

        playerPos.x = Mathf.Clamp(playerPos.x, -3.125f, 3.125f);
        player.transform.position = playerPos;
    }
}
