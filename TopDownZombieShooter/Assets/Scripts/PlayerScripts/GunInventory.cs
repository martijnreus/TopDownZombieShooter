using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunInventory : MonoBehaviour
{
    [SerializeField] GunSO starterWeaponSO;
    [SerializeField] SpriteRenderer gunSpriteRenderer;

    private Gun primaryWeapon;
    private Gun secondaryWeapon;
    private Gun currentWeapon;

    private GameInput gameInput;

    private bool isReloading;

    private Coroutine doReload;

    private void Awake()
    {
        gameInput = FindObjectOfType<GameInput>();
    }

    private void Start()
    {
        gameInput.OnReloadAction += OnReloadAcion;
        gameInput.OnSwitchAction += OnSwitchAction;
        primaryWeapon = new Gun(starterWeaponSO);
        currentWeapon = primaryWeapon;

        UpdateGunSprite();
    }

    private void OnSwitchAction(object sender, EventArgs e)
    {
        SwitchGun();
    }

    private void OnReloadAcion(object sender, EventArgs e)
    {
        //TODO make it impossible to shoot during the reload
        //TODO make the waitforseconds here
        //currentWeapon.ReloadGun();
        if (!isReloading)
        {
            doReload = StartCoroutine(DoReload());
        }
    }

    private IEnumerator DoReload()
    {
        isReloading = true;
        yield return new WaitForSeconds(currentWeapon.GetGunSO().reloadTime);
        currentWeapon.ReloadGun();
        isReloading = false;
    }

    public void CancelReload()
    {
        isReloading = false;
        StopCoroutine(doReload);
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

        UpdateGunSprite();
    }

    public bool HaveGun(GunSO gunSO)
    {
        if (gunSO == primaryWeapon.GetGunSO() || (secondaryWeapon != null && gunSO == secondaryWeapon.GetGunSO()))
        {
            return true;
        }
        return false;
    }

    public void AddGun(GunSO newGunSO)
    {
        if (currentWeapon == primaryWeapon)
        {
            if (secondaryWeapon == null)
            {
                secondaryWeapon = new Gun(newGunSO);
                currentWeapon = secondaryWeapon;
            }
            else
            {
                primaryWeapon = new Gun(newGunSO);
                currentWeapon = primaryWeapon;
            }
        }
        else
        {
            if (primaryWeapon == null)
            {
                primaryWeapon = new Gun(newGunSO);
                currentWeapon = primaryWeapon;
            }
            else
            {
                secondaryWeapon = new Gun(newGunSO);
                currentWeapon = secondaryWeapon;
            }
        }

        UpdateGunSprite();
    }

    private void UpdateGunSprite()
    {
        gunSpriteRenderer.sprite = currentWeapon.GetGunSO().gunSprite;
    }

    public Gun GetCurrentGun()
    {
        return currentWeapon;
    }

    public bool GetIsReloading()
    {
        return isReloading;
    }
}
