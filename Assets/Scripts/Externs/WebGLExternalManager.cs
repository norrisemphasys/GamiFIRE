using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.InteropServices;

public class WebGLExternalManager : MonoSingleton<WebGLExternalManager>
{
    [DllImport("__Internal")]
    private static extern void GoToFullScreen();
    [DllImport("__Internal")]
    private static extern void HasFullscreen();

    public static void FullScreen()
    {
        GoToFullScreen();
    }
}
