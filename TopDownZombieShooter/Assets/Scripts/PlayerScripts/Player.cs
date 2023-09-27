using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private int health;

    public HealthSystem healthSystem;

    private void Awake()
    {
        healthSystem = new HealthSystem(health);
    }

    public HealthSystem GetHealthSystem()
    {
        return healthSystem;
    }
}
