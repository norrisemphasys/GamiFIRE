using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MGTwo : MiniGame
{
    [SerializeField] MGPlayer player;
    [SerializeField] MGDropController controller;
    public override void OnEnter()
    {
        main.SetActive(true);

        gameManager.miniGameController.uiController.ShowCanvas(true);
        gameManager.sceneController.cameraController.SetCamera(CameraType.MINI_GAME);
        gameManager.miniGameController.uiController.Show(UIState.MGMM_MENU);

        controller.Init();

        AddListener();
    }

    public override void OnExit()
    {
        RemoveListener();

        main.SetActive(false);

        controller.ResetPool();
        gameManager.miniGameController.uiController.ShowCanvas(false);
    }

    public override void AddListener()
    {
        GameEvents.OnMouseMove.AddListener(MovePlayer);
    }

    public override void RemoveListener()
    {
        GameEvents.OnMouseMove.RemoveListener(MovePlayer);
    }

    public void DropEgg(int count, float delay, float scale, UnityAction callback = null)
    {
        controller.DeployEgg(count, delay, scale, callback);
    }

    void MovePlayer(Vector3 pos)
    {
        Vector3 newPosition = gameManager.sceneController.cameraController
            .minigameCamera.ScreenToWorldPoint(pos);
        newPosition.y = 0;
        newPosition.z = 0;
        player.SetPlayerPosition(newPosition);
    }

    private void OnDestroy()
    {
        RemoveListener();
    }
}
