using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PauzeGame : MonoBehaviour
{
    private bool isPaused;
    [SerializeField] private GameObject pauseCanvas;
    [SerializeField] private MonoBehaviour[] scrpitsToDisable;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                Play();       
            }
            else
            {
                Pauze();
            }
        }
    }

    public void Pauze()
    {
        isPaused = true;
        DisableScripts();
        pauseCanvas.SetActive(true);
        Time.timeScale = 0;
    }

    public void Play()
    {
        isPaused = false;
        EnableScripts();
        pauseCanvas.SetActive(false);
        Time.timeScale = 1;
    }

    private void EnableScripts()
    {
        foreach (MonoBehaviour script in scrpitsToDisable)
        {
            script.enabled = true;
        }
    }

    private void DisableScripts()
    {
        foreach (MonoBehaviour script in scrpitsToDisable)
        {
            script.enabled = false;
        }
    }
}
