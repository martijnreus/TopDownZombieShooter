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

    private HealthSystem playerHealthSystem;

    private float zoomAmount = 10f;
    private float zoomDuration = 5f;

    private float fadeDuration = 4f;

    private void Start()
    {
        playerHealthSystem = FindObjectOfType<Player>().GetHealthSystem();
        playerHealthSystem.OnDead += PlayerHealthSystem_OnDead;
    }

    private void PlayerHealthSystem_OnDead(object sender, System.EventArgs e)
    {
        cinemachineVirtualCamera.gameObject.SetActive(false);
        ZoomOut();
        FadeToBlack();
    }

    private void ZoomOut()
    {
        Camera.main.DOOrthoSize(zoomAmount, zoomDuration);
    }

    private void FadeToBlack()
    {
        Color color = backGround.color;
        backGround.DOColor(new Color(color.r, color.g, color.b, 1f), fadeDuration).SetDelay(2f);
    }
}
