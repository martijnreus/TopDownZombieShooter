using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunWallBuy : MonoBehaviour, IInteractable
{
    [SerializeField] private Gun gun;

    private GunInventory gunInventory;

    private void Awake()
    {
        gunInventory = FindObjectOfType<GunInventory>();
    }

    public void Interact(Transform interactorTransform)
    {
        // If the player dont have the gun already buy it
        if (!gunInventory.HaveGun(gun))
        {
            //TODO Substract points when buying
            gunInventory.AddGun(gun);
        }
        else if (gunInventory.GetCurrentGun() == gun)
        {
            gunInventory.GetCurrentGun().RefillAmmo();
        }
    }

    public string GetInteractText()
    {
        return "Buy " + gun.GetGunSO().gunName + " for 200 points";
    }
}
