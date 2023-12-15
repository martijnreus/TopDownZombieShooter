using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class DieCameraAnimation : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera cinemachineVirtualCamera;
    [SerializeField] private Image backGround;
    [SerializeField] private CanvasGroup deathScreen;
    [SerializeField] private CanvasGroup winScreen;

    private HealthSystem playerHealthSystem;
    private Player player;

    private float zoomAmount = 10f;
    private float zoomDuration = 5f;

    private float fadeDuration = 2.5f;

    private void Start()
    {
        player = FindObjectOfType<Player>();
        playerHealthSystem = player.GetHealthSystem();
        playerHealthSystem.OnDead += PlayerHealthSystem_OnDead;
    }

    private void PlayerHealthSystem_OnDead(object sender, System.EventArgs e)
    {
        ZoomOut();
        FadeToBlack();
        StartCoroutine(ShowGameOverScreen());
    }

    public void ZoomOut()
    {
        cinemachineVirtualCamera.gameObject.SetActive(false);
        Camera.main.DOOrthoSize(zoomAmount, zoomDuration);
    }

    public void FadeToBlack()
    {
        Color color = backGround.color;
        backGround.DOColor(new Color(color.r, color.g, color.b, 1f), fadeDuration).SetDelay(2.5f);
    }

    private IEnumerator ShowGameOverScreen()
    {
        deathScreen.DOFade(1f, 1f).SetDelay(3.5f);
        deathScreen.blocksRaycasts = true;
        yield return new WaitForSeconds(4f);
        player.transform.position = new Vector3(1000, 0, 0);
    }

    public void ShowWinScreen()
    {
        winScreen.DOFade(1f, 1f).SetDelay(3.5f);
        winScreen.blocksRaycasts = true;
        player.transform.position = new Vector3(1000, 0, 0);
    }
}
