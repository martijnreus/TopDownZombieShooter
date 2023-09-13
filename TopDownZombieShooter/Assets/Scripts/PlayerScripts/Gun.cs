using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun
{
    //[SerializeField] private GunSO gunSO;
    private GunSO gunSO;

    private int totalAmmo;
    private int ammoInWeapon;

    public Gun(GunSO gunSO)
    {
        this.gunSO = gunSO;
        totalAmmo = gunSO.bulletCapacity * 4;
        ammoInWeapon = gunSO.bulletCapacity;
    }

    /*
    // When gun is created set the ammo to max
    private void Start()
    {
        ammoInWeapon = gunSO.bulletCapacity;
        RefillAmmo();
    }
    */

    public void UseAmmo()
    {
        ammoInWeapon--;
    }

    public void ReloadGun()
    {
        int bulletsToAdd = gunSO.bulletCapacity - ammoInWeapon;

        if (bulletsToAdd > totalAmmo)
        {
            bulletsToAdd = totalAmmo;
        }

        totalAmmo -= bulletsToAdd;
        ammoInWeapon += bulletsToAdd;
    }

    public void RefillAmmo()
    {
        totalAmmo = gunSO.bulletCapacity * 4;
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
