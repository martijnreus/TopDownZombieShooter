using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunWallBuy : MonoBehaviour, IInteractable
{
    [SerializeField] private GunSO gunSO;
    [SerializeField] private int gunCost;
    [SerializeField] private int ammoCost;

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
        if (!gunInventory.HaveGun(gunSO) && pointManager.GetCurrentPointAmount() >= gunCost)
        {
            gunInventory.AddGun(gunSO);
            pointManager.RemovePoints(gunCost);
        }
        else if (gunInventory.GetCurrentGun().GetGunSO() == gunSO && pointManager.GetCurrentPointAmount() >= ammoCost)
        {
            gunInventory.GetCurrentGun().RefillAmmo();
            pointManager.RemovePoints(ammoCost);
        }
    }

    public string GetInteractText()
    {
        return "Buy " + gunSO.gunName + " for " + gunSO.gunPrice + " points";
    }
}
