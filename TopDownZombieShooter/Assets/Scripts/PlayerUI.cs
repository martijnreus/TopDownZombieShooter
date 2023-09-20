using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI currentAmmoText;
    [SerializeField] TextMeshProUGUI weaponText;
    [SerializeField] TextMeshProUGUI totalAmmoText;

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
