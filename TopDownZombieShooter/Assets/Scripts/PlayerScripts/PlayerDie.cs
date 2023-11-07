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
    //private WaveManager waveManager;

    private Rigidbody2D playerBody;
    private bool isDead;

    private void Awake()
    {
        player = GetComponent<Player>();
        playerBody = GetComponent<Rigidbody2D>();

        //waveManager = FindObjectOfType<WaveManager>();
    }

    private void Start()
    {   
        deathScreen.SetActive(false);
        healthSystem = player.GetHealthSystem();
        healthSystem.OnDead += Die;
    }

    private void Die(object sender, EventArgs e)
    {
        if (isDead) 
        {
            return;
        }

        isDead = true;
        deathScreen.SetActive(true);

        // make sure the player stop moving
        playerBody.velocity = Vector3.zero;

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
