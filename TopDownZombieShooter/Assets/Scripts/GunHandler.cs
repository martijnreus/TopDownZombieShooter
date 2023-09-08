using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunHandler : MonoBehaviour
{
    [SerializeField] GunSO gunSO;

    private int totalAmmo;
    private int ammoInWeapon;

    private GameInput gameInput;

    private void Awake()
    {
        gameInput = FindObjectOfType<GameInput>();
    }

    private void Start()
    {
        ammoInWeapon = gunSO.bulletCapacity;
    }

    private void Update()
    {
        //TODO move to input class and handle with event
        if (Input.GetKeyDown(KeyCode.R))
        {
            ReloadGun();
        }
    }

    public void UseAmmo()
    {
        ammoInWeapon--;
        Debug.Log("Ammo left: " + ammoInWeapon);
    }

    private void ReloadGun()
    {
        ammoInWeapon = gunSO.bulletCapacity;
        Debug.Log("Reloaded");
    }

    public GunSO GetCurrentGun()
    {
        return gunSO;
    }

    public int GetAmmoInWeapon()
    {
        return ammoInWeapon;
    }
}
