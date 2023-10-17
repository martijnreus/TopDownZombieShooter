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

    private bool isReloading;
    private float completionAmount;
    private float reloadMultiplier = 1f;

    private Coroutine doReload;
    private PerkInventory perkInventory;

    private void Awake()
    {
        perkInventory = GetComponent<PerkInventory>();
        perkInventory.OnPerkAdd += PerkInventory_OnPerkAdd;
    }

    private void Start()
    {
        GameInput.Instance.OnReloadAction += OnReloadAcion;
        GameInput.Instance.OnSwitchAction += OnSwitchAction;
        primaryWeapon = new Gun(starterWeaponSO);
        currentWeapon = primaryWeapon;

        UpdateGunSprite();
    }

    private void PerkInventory_OnPerkAdd(object sender, PerkSO perk)
    {
        if (perk.type == PerkSO.Type.SpeedCola)
        {
            reloadMultiplier = 0.5f;
        }
    }

    private void OnSwitchAction(object sender, EventArgs e)
    {
        SwitchGun();
    }

    private void OnReloadAcion(object sender, EventArgs e)
    {
        if (!isReloading && currentWeapon.GetGunSO().bulletCapacity != currentWeapon.GetAmmoInWeapon() && currentWeapon.GetTotalAmmo() != 0)
        {
            doReload = StartCoroutine(DoReload());
        }
    }

    private IEnumerator DoReload()
    {
        isReloading = true;
        float timeStartReloading = Time.time;

        completionAmount = 0;

        while(Time.time - timeStartReloading <= currentWeapon.GetGunSO().reloadTime * reloadMultiplier)
        {
            completionAmount = 1 - ((currentWeapon.GetGunSO().reloadTime - (Time.time - timeStartReloading) * (1 / reloadMultiplier)) 
                                / currentWeapon.GetGunSO().reloadTime);

            print(completionAmount);
            yield return new WaitForEndOfFrame();
        }

        completionAmount = 1;
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

    public float GetCompletionAmount()
    {
        return completionAmount;
    }
}
