using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDie : MonoBehaviour
{
    [SerializeField] private GameObject deathScreen;
    [SerializeField] private MonoBehaviour[] scriptsToDisable;

    private Player player;
    private HealthSystem healthSystem;

    private void Awake()
    {
        player = GetComponent<Player>();
    }

    private void Start()
    {   
        deathScreen.SetActive(false);
        healthSystem = player.GetHealthSystem();
        healthSystem.OnDead += Die;
    }

    private void Die(object sender, EventArgs e)
    {
        deathScreen.SetActive(true);
        DisableScripts(scriptsToDisable);
    }

    private void DisableScripts(MonoBehaviour[] scripts)
    {
        foreach (MonoBehaviour script in scripts)
        {
            script.enabled = false;
        }
    }
}
