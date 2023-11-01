using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonController : MonoBehaviour
{
    public enum Scene
    {
        GameScene,
        MenuScene,
        LoadingScene
    }

    public void LoadScene(int sceneIndex)
    {
        Loader.Load(sceneIndex);
    }

    public void ReloadScene() 
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
