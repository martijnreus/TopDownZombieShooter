using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunWallBuy : MonoBehaviour, IInteractable
{
    [SerializeField] private GunSO gunSO;

    private GunInventory gunInventory;

    private void Awake()
    {
        gunInventory = FindObjectOfType<GunInventory>();
    }

    public void Interact()
    {
        // If the player dont have the gun already buy it
        if (!gunInventory.HaveGun(gunSO))
        {
            //TODO Substract points when buying
            gunInventory.AddGun(gunSO);
        }
        else if (gunInventory.GetCurrentGun().GetGunSO() == gunSO)
        {
            gunInventory.GetCurrentGun().RefillAmmo();
        }
    }

    public string GetInteractText()
    {
        return "Buy " + gunSO.gunName + " for 200 points";
    }
}
