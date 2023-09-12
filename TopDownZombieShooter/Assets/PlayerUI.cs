using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.UI;
using TMPro;

public class PlayerUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI bulletText;

    private GunInventory gunInventory;

    private void Awake()
    {
        gunInventory = FindObjectOfType<GunInventory>();
    }

    private void Update()
    {
        UpdateBulletText();
    }

    private void UpdateBulletText()
    {
        int ammoInWeapon = gunInventory.GetCurrentGun().GetAmmoInWeapon();
        int totalAmmo = gunInventory.GetCurrentGun().GetTotalAmmo();
        bulletText.text = "Bullet: " + ammoInWeapon + "/" + totalAmmo;
    }
}
