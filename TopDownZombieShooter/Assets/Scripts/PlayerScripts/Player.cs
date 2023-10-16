using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private int health;
    [SerializeField] private float healDelay;
    [SerializeField] private float healRate;

    private float timeLastDamaged;
    private float timeLastHealed;

    public HealthSystem healthSystem;

    private PerkInventory perkInventory;

    private void Awake()
    {
        healthSystem = new HealthSystem(health);
        healthSystem.OnDamaged += OnDamaged;

        perkInventory = GetComponent<PerkInventory>();
        perkInventory.OnPerkAdd += PerkInventory_OnPerkAdd;
    }

    private void PerkInventory_OnPerkAdd(object sender, PerkSO perk)
    {
        if (perk.type == PerkSO.Type.Juggernog)
        {
            healthSystem.SetHealthMax(healthSystem.GetHealth() + 100, false);
            healthSystem.Heal(100);
        }
    }

    private void OnDamaged(object sender, EventArgs e)
    {
        timeLastDamaged = Time.time;
    }

    private void Update()
    {
        if (Time.time - timeLastDamaged > healDelay && Time.time - timeLastHealed > healRate)
        {
            timeLastHealed = Time.time;
            healthSystem.Heal(1);
        }
    }

    public HealthSystem GetHealthSystem()
    {
        return healthSystem;
    }
}
