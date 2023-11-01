using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class Loader
{
    private class LoadingMonoBehaviour: MonoBehaviour { }

    private static Action onLoaderCallback;
    private static AsyncOperation loadingAsyncOperation;

    public static void Load(int sceneIndex) 
    {
        onLoaderCallback = () =>
        {
            GameObject loadingObject = new GameObject("Loading Game Object");
            loadingObject.AddComponent<LoadingMonoBehaviour>().StartCoroutine(LoadSceneAsync(sceneIndex));
            LoadSceneAsync(sceneIndex);
        };

        int loadingSceneIndex = 0;
        SceneManager.LoadScene(loadingSceneIndex);
    }  

    private static IEnumerator LoadSceneAsync(int sceneIndex)
    {
        yield return null;

        loadingAsyncOperation =  SceneManager.LoadSceneAsync(sceneIndex);

        while (!loadingAsyncOperation.isDone)
        {
            yield return null;
        }
    }

    public static float GetLoadingProgress()
    {
        if (loadingAsyncOperation != null)
        {
            return loadingAsyncOperation.progress;
        }

        return 0f;  
    }

    public static void LoaderCallback()
    {
        if (onLoaderCallback != null)
        {
            onLoaderCallback();
            onLoaderCallback = null;
        }
    }
}
