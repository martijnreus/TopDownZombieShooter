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

    private GunInventory gunInventory;
    private PlayerShoot playerShoot;

    private void Awake()
    {
        gunInventory = FindObjectOfType<GunInventory>();
        playerShoot = FindObjectOfType<PlayerShoot>();
    }

    private void Update()
    {
        UpdateBulletText();
        UpdateWeaponText();
        UpdateWeaponImage();
        UpdateReloadSlider();
        UpdateShootSlider();
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
            Debug.Log("last shot" + (Time.time - playerShoot.GetTimeLastShot()));
            shootSlider.value = 1 - (timeBetweenShots - (Time.time - playerShoot.GetTimeLastShot())) / timeBetweenShots;
            Debug.Log(shootSlider.value);
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
