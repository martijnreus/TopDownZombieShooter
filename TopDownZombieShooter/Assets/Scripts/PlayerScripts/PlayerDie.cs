using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDie : MonoBehaviour
{
    [SerializeField] private MonoBehaviour[] scriptsToDisable;
    [SerializeField] private GameObject gunSprite;

    private Player player;
    private HealthSystem healthSystem;

    private Rigidbody2D playerBody;
    private bool isDead;

    private Animator animator;

    private void Awake()
    {
        player = GetComponent<Player>();
        playerBody = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {   
        animator = GetComponentInChildren<Animator>();
        healthSystem = player.GetHealthSystem();
        healthSystem.OnDead += OnDie;
    }

    private void OnDie(object sender, EventArgs e)
    {
        Die();
    }

    public void Die()
    {
        if (isDead)
        {
            return;
        }

        isDead = true;
        gunSprite.SetActive(false);

        // make sure the player stop moving
        playerBody.velocity = Vector3.zero;

        DisableScripts(scriptsToDisable);

        // play the die animation
        animator.SetTrigger("isDead");
    }

    private void DisableScripts(MonoBehaviour[] scripts)
    {
        foreach (MonoBehaviour script in scripts)
        {
            script.enabled = false;
        }
    }
}
