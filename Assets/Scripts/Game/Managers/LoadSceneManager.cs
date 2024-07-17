//#define FOR_TESTING_PURPOSES

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;

public class LoadSceneManager : MonoSingleton<LoadSceneManager>
{ 
    string currentEnvironmentPath = string.Empty;
    string currentScenarioPath = string.Empty;

    #region OLD IMPLEMENTATION
    int loadedLevelBuildIndex = -1;

    IEnumerator LoadLevel(int levelBuildIndex, LoadSceneMode mode,
        UnityAction onLoadFinished = null)
    {
        enabled = false;
        if (loadedLevelBuildIndex > 0)
        {
            yield return SceneManager.UnloadSceneAsync(loadedLevelBuildIndex);
        }

        if (levelBuildIndex > 0)
        {
            yield return SceneManager.LoadSceneAsync(
                levelBuildIndex, mode
                );
        }

        SceneManager.SetActiveScene(
            SceneManager.GetSceneByBuildIndex(levelBuildIndex)
        );
        loadedLevelBuildIndex = levelBuildIndex;
        enabled = true;

        if (onLoadFinished != null)
        {
            onLoadFinished();
            onLoadFinished = null;
        }
    }

    public void LoadSceneLevel(int idx, LoadSceneMode mode,
        UnityAction onLoadFinished = null)
    {
        StartCoroutine(LoadLevel(idx, mode, onLoadFinished));
    }
    #endregion

    IEnumerator LoadScenarioEnum(string path, UnityAction onLoadFinished = null)
    {
        if (currentScenarioPath.Equals(path))
            yield return null;

        yield return SceneManager.LoadSceneAsync(path, LoadSceneMode.Additive);


        SceneManager.SetActiveScene(SceneManager.GetSceneByPath("Assets/" + path + ".unity"));
        LightProbes.TetrahedralizeAsync();

        if (onLoadFinished != null)
        {
            onLoadFinished();
            onLoadFinished = null;
        }

        currentScenarioPath = path;
    }

    IEnumerator LoadEnvironmentEnum(string path, UnityAction onLoadFinished = null) 
    {
        if (currentEnvironmentPath.Equals(path))
            yield break;

        yield return SceneManager.LoadSceneAsync(path, LoadSceneMode.Additive);

       
        if (onLoadFinished != null)
        {
            onLoadFinished();
            onLoadFinished = null;
        }

        currentEnvironmentPath = path;
    }

    public void UnloadEnvironmentAndScene(string envPath, string scenePath, UnityAction onLoadFinished = null)
    {
        StartCoroutine(UnloadEnvironmentAndSceneEnum(envPath, scenePath, onLoadFinished));
    }

    IEnumerator UnloadEnvironmentAndSceneEnum(string envPath, string scenePath, UnityAction onLoadFinished = null, bool reload = false)
    {
        Debug.Log("ENV " + currentEnvironmentPath + " SCENE " + currentScenarioPath);

        if (!currentEnvironmentPath.Equals(envPath) && currentEnvironmentPath != string.Empty)
            yield return SceneManager.UnloadSceneAsync(currentEnvironmentPath);

        if (!currentScenarioPath.Equals(scenePath) && currentScenarioPath != string.Empty || reload)
            yield return SceneManager.UnloadSceneAsync(currentScenarioPath);
    }

    IEnumerator LoadSceneEnum(string envPath, string scenePath, UnityAction onLoadFinished = null, bool reload = false)
    {
        yield return StartCoroutine(UnloadEnvironmentAndSceneEnum(envPath, scenePath, onLoadFinished, reload));
        yield return StartCoroutine(LoadEnvironmentEnum(envPath));
        yield return StartCoroutine(LoadScenarioEnum(scenePath, onLoadFinished));
    }

    IEnumerator LoadUnloadSceneEnum(string path, UnityAction onLoadFinished = null)
    {
        yield return SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene());
        yield return StartCoroutine(LoadScenarioEnum(path, onLoadFinished));
    }

    public void LoadScenario(string path, UnityAction onLoadFinished = null)
    {
        StartCoroutine(LoadScenarioEnum(path, onLoadFinished));
    }

    public void LoadEnvrionment(string path, UnityAction onLoadFinished = null)
    {
        StartCoroutine(LoadEnvironmentEnum(path, onLoadFinished));
    }

    public void LoadScene(string envPath, string scenePath, UnityAction onLoadFinished = null, bool reload = false)
    {
        StartCoroutine(LoadSceneEnum(envPath, scenePath, onLoadFinished, reload));
    }

    public void LoadUnloadScene(string path, UnityAction onLoadFinished = null)
    {
        StartCoroutine(LoadUnloadSceneEnum(path, onLoadFinished));
    }
}