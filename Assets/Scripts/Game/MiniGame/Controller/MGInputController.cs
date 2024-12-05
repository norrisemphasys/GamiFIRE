using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MGInputController : MonoBehaviour
{
    public void UpdateInput()
    {
        if (Input.GetKeyDown(KeyCode.A))
            GameEvents.OnPressA.Invoke(MiniGame.PlatformType.RED);

        if (Input.GetKeyDown(KeyCode.S))
            GameEvents.OnPressA.Invoke(MiniGame.PlatformType.YELLOW);

        if (Input.GetKeyDown(KeyCode.D))
            GameEvents.OnPressA.Invoke(MiniGame.PlatformType.GREEN);

        if (Input.GetKey(KeyCode.LeftArrow))
            GameEvents.OnPressLeft.Invoke(MiniGame.Direction.LEFT);

        if (Input.GetKey(KeyCode.RightArrow))
            GameEvents.OnPressRight.Invoke(MiniGame.Direction.RIGHT);

        if (Input.GetKeyUp(KeyCode.LeftArrow))
            GameEvents.OnReleaseLeft.Invoke(MiniGame.Direction.LEFT);

        if (Input.GetKeyUp(KeyCode.RightArrow))
            GameEvents.OnReleaseRight.Invoke(MiniGame.Direction.RIGHT);

        if (Input.GetKeyDown(KeyCode.UpArrow))
            GameEvents.OnPressUp.Invoke(MiniGame.Direction.UP);

        if (Input.GetKeyDown(KeyCode.DownArrow))
            GameEvents.OnPressUp.Invoke(MiniGame.Direction.DOWN);

        GameEvents.OnMouseMove.Invoke(Input.mousePosition);
    }
}
