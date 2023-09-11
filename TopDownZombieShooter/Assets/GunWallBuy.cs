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
        //TODO the player cant have the same weapon twice
        // If the player dont have the gun already buy it
        if (!gunInventory.HaveGun(gun))
        {
            gunInventory.AddGun(gun);
        }
        else
        {
            //TODO refill ammo
        }
    }

    public string GetInteractText()
    {
        return "Buy gun for 200 points";
    }
}
