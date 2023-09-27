using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun
{
    private GunSO gunSO;

    private int totalAmmo;
    private int ammoInWeapon;

    public Gun(GunSO gunSO)
    {
        this.gunSO = gunSO;
        ammoInWeapon = gunSO.bulletCapacity;
        RefillAmmo();   
    }

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
        totalAmmo = GetMaxAmmoAmount();
    }

    public int GetMaxAmmoAmount()
    {
        return gunSO.bulletCapacity * 4;
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
