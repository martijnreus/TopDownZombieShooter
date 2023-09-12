using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField] private GunSO gunSO;

    private int totalAmmo;
    private int ammoInWeapon;

    // When gun is created set the ammo to max
    private void Start()
    {
        ammoInWeapon = gunSO.bulletCapacity;
        totalAmmo = gunSO.bulletCapacity * 9;
    }

    public void UseAmmo()
    {
        ammoInWeapon--;
    }

    public void ReloadGun()
    {
        totalAmmo -= (gunSO.bulletCapacity - ammoInWeapon);
        ammoInWeapon = gunSO.bulletCapacity;
    }

    public GunSO GetGunSO()
    {
        return gunSO;
    }

    public int GetAmmoInWeapon()
    {
        return ammoInWeapon;
    }

    public int GetTotalAmmo()
    {
        return totalAmmo;
    }
}
