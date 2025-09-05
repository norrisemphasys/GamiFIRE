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
    [DllImport("__Internal")]
    private static extern void DownloadFile(string content, string filename);

    public void ExportCSV(string filename, string csvContent)
    {
#if UNITY_WEBGL && !UNITY_EDITOR
        // Using DllImport
        DownloadFile(csvContent, filename);
#else
        Debug.Log("content " + csvContent + " filename " + filename);
#endif
    }

    public static void FullScreen()
    {
        GoToFullScreen();
    }
}
