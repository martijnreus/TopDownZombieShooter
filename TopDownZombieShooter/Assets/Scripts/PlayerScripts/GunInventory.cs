using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunInventory : MonoBehaviour
{
    [SerializeField] private Gun primaryWeapon;
    [SerializeField] private Gun secondaryWeapon;

    private Gun currentWeapon;

    private GameInput gameInput;

    private void Awake()
    {
        gameInput = FindObjectOfType<GameInput>();
    }

    private void Start()
    {
        gameInput.OnReloadAction += OnReloadAcion;
        gameInput.OnSwitchAction += OnSwitchAction;
        currentWeapon = primaryWeapon;
    }

    private void OnSwitchAction(object sender, EventArgs e)
    {
        SwitchGun();
    }

    private void OnReloadAcion(object sender, System.EventArgs e)
    {
        currentWeapon.ReloadGun();
    }

    private void SwitchGun()
    {
        if (currentWeapon == primaryWeapon && secondaryWeapon != null)
        {
            currentWeapon = secondaryWeapon;
        }
        else if (primaryWeapon != null)
        {
            currentWeapon = primaryWeapon;
        }
    }

    public Gun GetCurrentGun()
    {
        return currentWeapon;
    }

    public bool HaveGun(Gun gun)
    {
        if (gun == primaryWeapon || gun == secondaryWeapon)
        {
            return true;
        }
        return false;
    }

    public void AddGun(Gun newGun)
    {
        if (currentWeapon == primaryWeapon)
        {
            if (secondaryWeapon == null)
            {
                secondaryWeapon = newGun;
            }
            else
            {
                primaryWeapon = newGun;
            }
        }
        else
        {
            if (primaryWeapon == null)
            {
                primaryWeapon = newGun;
            }
            else
            {
                secondaryWeapon = newGun;
            }
        }
        
    }
}
