using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunWallBuy : MonoBehaviour, IInteractable
{
    [SerializeField] private GunSO gunSO;

    private GunInventory gunInventory;
    private PointManager pointManager;

    private void Awake()
    {
        gunInventory = FindObjectOfType<GunInventory>();
        pointManager = FindObjectOfType<PointManager>();
    }

    public void Interact()
    {
        // If the player dont have the gun already buy it
        if (!gunInventory.HaveGun(gunSO) && pointManager.GetCurrentPointAmount() >= gunSO.gunPrice)
        {
            gunInventory.AddGun(gunSO);
            pointManager.RemovePoints(gunSO.gunPrice);
        }
        else if (gunInventory.GetCurrentGun().GetGunSO() == gunSO && pointManager.GetCurrentPointAmount() >= gunSO.ammoPrice)
        {
            gunInventory.GetCurrentGun().RefillAmmo();
            pointManager.RemovePoints(gunSO.ammoPrice);
        }
    }

    public string GetInteractText()
    {
        if (!gunInventory.HaveGun(gunSO))
        {
            return "Press E to buy " + gunSO.gunName + " [Cost: " + gunSO.gunPrice + "]";
        }
        else
        {
            return "Press E to buy " + gunSO.gunName + " ammo [Cost: " + gunSO.ammoPrice + "]";
        }      
    }
}
