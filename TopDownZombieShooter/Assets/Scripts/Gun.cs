using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField] GunSO gunSO;

    private int currentAmmo;

    private GameInput gameInput;

    private void Awake()
    {
        gameInput = FindObjectOfType<GameInput>();
    }

    private void Start()
    {
        currentAmmo = gunSO.bulletCapacity;
        gameInput.OnShootAction += GameInput_OnShootAction;
    }

    private void Update()
    {
        //TODO move to input class and handle with event
        if (Input.GetKeyDown(KeyCode.R))
        {
            ReloadGun();
        }
    }

    private void GameInput_OnShootAction(object sender, System.EventArgs e)
    {
        currentAmmo--;
        Debug.Log("Ammo left: " + currentAmmo);
    }

    private void ReloadGun()
    {
        currentAmmo = gunSO.bulletCapacity;
        Debug.Log("Reloaded");
    }

    public GunSO GetCurrentGun()
    {
        return gunSO;
    }
}
