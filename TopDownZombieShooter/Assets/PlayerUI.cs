using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI bulletText;
    [SerializeField] TextMeshProUGUI weaponText;

    private GunInventory gunInventory;

    private void Awake()
    {
        gunInventory = FindObjectOfType<GunInventory>();
    }

    private void Update()
    {
        UpdateBulletText();
        UpdateWeaponText();
    }

    private void UpdateBulletText()
    {
        int ammoInWeapon = gunInventory.GetCurrentGun().GetAmmoInWeapon();
        int totalAmmo = gunInventory.GetCurrentGun().GetTotalAmmo();
        bulletText.text = "Bullet: " + ammoInWeapon + "/" + totalAmmo;
    }

    private void UpdateWeaponText()
    {
        string gunName = gunInventory.GetCurrentGun().GetGunSO().gunName;
        weaponText.text = gunName;
    }
}
