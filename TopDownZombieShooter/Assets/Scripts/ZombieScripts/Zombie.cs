using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Zombie : MonoBehaviour
{
    [SerializeField] private float walkSpeed;
    [SerializeField] private float attackRange;
    [SerializeField] private float attackSpeed;
    [SerializeField] private int damageAmount;
    [SerializeField] private int health;
    [SerializeField] private Transform attackPoint;

    private Player player;
    private State state;
    private float attackTimer;

    public HealthSystem healthSystem;
    private WaveManager waveManager;

    private NavMeshAgent agent;

    private enum State
    {
        spawning,
        walking,
        attacking,
    }

    private void Awake()
    {
        player = FindObjectOfType<Player>();
        waveManager = FindObjectOfType<WaveManager>();
    }

    private void Start()
    {
        healthSystem = new HealthSystem(health);
        healthSystem.OnDead += Die;

        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;

        state = State.spawning;
    }

    private void Die(object sender, EventArgs e)
    {
        waveManager.activeZombies.Remove(gameObject);
        Destroy(gameObject);
    }

    private void Update()
    {
        switch (state)
        {
            case State.spawning:
                state = State.walking;
                break;

            case State.walking:
                //Check if the player is in attack range
                float distance = (attackPoint.position - player.transform.position).magnitude;
                if (distance < attackRange)
                {
                    state = State.attacking;
                    agent.isStopped = true;
                    break;
                }

                agent.isStopped = false;

                RotateSprite();
                agent.SetDestination(player.transform.position);
                break;

            case State.attacking:
                //TODO play attack animation and damage aan het einde als de player nog in de buurt is
                attackTimer += Time.deltaTime;
                if (attackTimer >= attackSpeed)
                {
                    Attack();
                    attackTimer = 0;
                    state = State.walking;    
                }

                break;
        }
    }

    private void Attack()
    {
        //TODO check if the player is still in range and hit if it is
        player.healthSystem.Damage(damageAmount);
    }

    private void RotateSprite()
    {
        if (agent.velocity.x > 0.02)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
        else if (agent.velocity.y < 0.02)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
    }
}