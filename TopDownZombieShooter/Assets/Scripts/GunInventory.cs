using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunInventory : MonoBehaviour
{
    [SerializeField] private Gun primaryWeapon;
    [SerializeField] private Gun secondaryWeapon;

    private Gun currentWeapon;

    //TODO make 2 slots for weapons and move the ammo logic to the gun class
    //TODO make variable for the gun you are holding
    //TODO make a way to switch weapons

    private GameInput gameInput;

    private void Awake()
    {
        gameInput = FindObjectOfType<GameInput>();
    }

    private void Start()
    {
        currentWeapon = primaryWeapon;
    }

    private void Update()
    {
        //TODO move to input class and handle with event
        if (Input.GetKeyDown(KeyCode.R))
        {
            currentWeapon.ReloadGun();
        }

        if (Input.GetKeyDown(KeyCode.T))
        {
            SwitchGun();
        }
    }

    private void SwitchGun()
    {
        if (currentWeapon == primaryWeapon)
        {
            currentWeapon = secondaryWeapon;
        }
        else
        {
            currentWeapon = primaryWeapon;
        }
    }

    public Gun GetCurrentGun()
    {
        return currentWeapon;
    }
}
