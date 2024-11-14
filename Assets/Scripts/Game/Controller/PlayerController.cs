using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] BasicBehaviour basicBehavior;
    [SerializeField] MoveBehaviour moveBehavior;
    [SerializeField] AimBehaviourBasic aimBehavior;

    [SerializeField] ThirdPersonOrbitCamBasic thirdPersonController;
    public bool IsOccupied { get; set; }

    public void SetPause(bool pause)
    {
        basicBehavior.pause = pause;
        moveBehavior.pause = pause;
        aimBehavior.SetPause(pause);
        thirdPersonController.pause = pause;
    }
}
