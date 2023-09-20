using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI currentAmmoText;
    [SerializeField] TextMeshProUGUI weaponText;
    [SerializeField] TextMeshProUGUI totalAmmoText;
    [SerializeField] Image gunImage;

    private GunInventory gunInventory;

    private void Awake()
    {
        gunInventory = FindObjectOfType<GunInventory>();
    }

    private void Update()
    {
        UpdateBulletText();
        UpdateWeaponText();
        UpdateWeaponImage();
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
