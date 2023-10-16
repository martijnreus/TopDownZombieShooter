using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

public class PlayerUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI currentAmmoText;
    [SerializeField] TextMeshProUGUI weaponText;
    [SerializeField] TextMeshProUGUI totalAmmoText;
    [SerializeField] Image gunImage;
    [SerializeField] Slider reloadSlider;
    [SerializeField] Slider shootSlider;
    [SerializeField] TextMeshProUGUI pointsText;
    [SerializeField] TextMeshProUGUI waveText;
    [SerializeField] Slider healthSlider;
    [SerializeField] TextMeshProUGUI healthText;

    private GunInventory gunInventory;
    private PlayerShoot playerShoot;
    private PointManager pointManager;
    private WaveManager waveManager;
    private Player player;

    private void Awake()
    {
        gunInventory = FindObjectOfType<GunInventory>();
        playerShoot = FindObjectOfType<PlayerShoot>();
        pointManager = FindObjectOfType<PointManager>();
        waveManager = FindObjectOfType<WaveManager>();
        player = FindObjectOfType<Player>();
    }

    private void Update()
    {
        UpdateBulletText();
        UpdateWeaponText();
        UpdateWeaponImage();
        UpdateReloadSlider();
        UpdateShootSlider();
        UpdatePointsText();
        UpdateWaveText();
        UpdateHealthSlider();
        UpdateHealthtext();
    }

    private void UpdateHealthtext()
    {
        healthText.text = player.GetHealthSystem().GetHealth().ToString();
    }

    private void UpdateHealthSlider()
    {
        healthSlider.value = player.GetHealthSystem().GetHealthNormalized();
    }

    private void UpdateWaveText()
    {
        waveText.text = "Wave: " + waveManager.GetCurrentWave();
    }

    private void UpdatePointsText()
    {
        pointsText.text = "Points: " + pointManager.GetCurrentPointAmount();
    }

    private void UpdateReloadSlider()
    {
        if (gunInventory.GetIsReloading())
        {
            reloadSlider.gameObject.SetActive(true);
            reloadSlider.value = gunInventory.GetCompletionAmount();
        }
        else
        {
            reloadSlider.gameObject.SetActive(false);
        }
            
    }

    private void UpdateShootSlider()
    {

        if (Time.time - playerShoot.GetTimeLastShot() < gunInventory.GetCurrentGun().GetGunSO().timeBetweenShots)
        {
            shootSlider.gameObject.SetActive(true);
            float timeBetweenShots = gunInventory.GetCurrentGun().GetGunSO().timeBetweenShots;
            shootSlider.value = 1 - (timeBetweenShots - (Time.time - playerShoot.GetTimeLastShot())) / timeBetweenShots;
        }
        else
        {
            shootSlider.gameObject.SetActive(false);
        }
        
    }

    private void UpdateWeaponImage()
    {
        gunImage.sprite = gunInventory.GetCurrentGun().GetGunSO().gunUISprite;
    }

    private void UpdateBulletText()
    {
        int currentAmmo = gunInventory.GetCurrentGun().GetAmmoInWeapon();
        int totalAmmo = gunInventory.GetCurrentGun().GetTotalAmmo();

        currentAmmoText.text = currentAmmo.ToString();
        totalAmmoText.text = totalAmmo.ToString();
    }

    private void UpdateWeaponText()
    {
        string gunName = gunInventory.GetCurrentGun().GetGunSO().gunName;
        weaponText.text = gunName;
    }
}
