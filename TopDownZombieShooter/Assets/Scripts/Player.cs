using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private int health;

    public HealthSystem healthSystem;

    private void Start()
    {
        healthSystem = new HealthSystem(health);
        healthSystem.OnDead += Die;
    }

    private void Die(object sender, EventArgs e)
    {
        Debug.Log("you died!");
    }
}
