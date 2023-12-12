using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonController : MonoBehaviour
{
    [SerializeField] private AudioClip buttonSoundEffect;

    public void LoadScene(int sceneIndex)
    {
        PlayButtonSoundEffect();
        Loader.Load(sceneIndex);
    }

    public void ReloadScene() 
    {
        PlayButtonSoundEffect();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void PlayButtonSoundEffect()
    {
        SoundManager.PlaySound(buttonSoundEffect, SoundManager.soundEffectVolume);
    }
}
