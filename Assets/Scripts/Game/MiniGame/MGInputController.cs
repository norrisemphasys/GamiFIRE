using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MGInputController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void UpdateInput()
    {
        if (Input.GetKeyDown(KeyCode.A))
            GameEvents.OnPressA.Invoke(MiniGame.PlatformType.RED);

        if (Input.GetKeyDown(KeyCode.S))
            GameEvents.OnPressA.Invoke(MiniGame.PlatformType.YELLOW);

        if (Input.GetKeyDown(KeyCode.D))
            GameEvents.OnPressA.Invoke(MiniGame.PlatformType.GREEN);
    }
}
