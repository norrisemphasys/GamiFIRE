using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] BasicBehaviour basicBehavior;
    [SerializeField] MoveBehaviour moveBehavior;
    [SerializeField] AimBehaviourBasic aimBehavior;

    [SerializeField] ThirdPersonOrbitCamBasic thirdPersonController;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetPause(bool pause)
    {
        basicBehavior.pause = pause;
        moveBehavior.pause = pause;
        aimBehavior.SetPause(pause);
        thirdPersonController.pause = pause;
    }
}
