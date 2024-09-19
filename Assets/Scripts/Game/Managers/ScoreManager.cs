using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoSingleton<ScoreManager>
{
    private UserManager userManager;
    public override void Init()
    {
        base.Init();

        userManager = UserManager.instance;
    }


}
