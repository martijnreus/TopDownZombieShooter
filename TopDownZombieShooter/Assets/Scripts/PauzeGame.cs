using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using DG.Tweening;

public class PauzeGame : MonoBehaviour
{
    private bool isPaused;
    [SerializeField] private GameObject pauseCanvas;
    [SerializeField] private CanvasGroup pauseCanvasGroup;
    [SerializeField] private CanvasGroup gameHUDCanvasGroup;
    [SerializeField] private MonoBehaviour[] scriptsToDisable;

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
        Time.timeScale = 0;

        pauseCanvasGroup.alpha = 0f;
        pauseCanvasGroup.blocksRaycasts = true;

        pauseCanvas.transform.localPosition = new Vector3(0f, -1000f, 0f);
        pauseCanvas.transform.DOLocalMoveY(0f, 0.5f).SetEase(Ease.InOutBack).SetUpdate(true);

        pauseCanvasGroup.DOFade(1f, 0.5f).SetEase(Ease.InOutBack).SetUpdate(true);
        gameHUDCanvasGroup.DOFade(0f, 0.5f).SetEase(Ease.InOutBack).SetUpdate(true);
    }

    public void Play()
    {
        isPaused = false;
        EnableScripts();
        Time.timeScale = 1;

        pauseCanvasGroup.alpha = 1f;
        pauseCanvasGroup.blocksRaycasts = false;

        pauseCanvas.transform.localPosition = new Vector3(0f, 0f, 0f);
        pauseCanvas.transform.DOLocalMoveY(-1000f, 0.5f).SetEase(Ease.InOutBack);

        pauseCanvasGroup.DOFade(0f, 0.5f).SetEase(Ease.InOutBack);
        gameHUDCanvasGroup.DOFade(1f, 0.5f).SetEase(Ease.InOutBack);
    }

    private void EnableScripts()
    {
        foreach (MonoBehaviour script in scriptsToDisable)
        {
            script.enabled = true;
        }
    }

    private void DisableScripts()
    {
        foreach (MonoBehaviour script in scriptsToDisable)
        {
            script.enabled = false;
        }
    }
}
