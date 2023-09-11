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
    }

    public void UseAmmo()
    {
        ammoInWeapon--;
        Debug.Log("Ammo left: " + ammoInWeapon);
    }

    public void ReloadGun()
    {
        ammoInWeapon = gunSO.bulletCapacity;
        Debug.Log("Reloaded");
    }

    public GunSO GetGunSO()
    {
        return gunSO;
    }

    public int GetAmmoInWeapon()
    {
        return ammoInWeapon;
    }
}
