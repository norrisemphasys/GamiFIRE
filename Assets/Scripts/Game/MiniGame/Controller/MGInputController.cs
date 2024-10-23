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

        GameEvents.OnMouseMove.Invoke(Input.mousePosition);
    }
}
