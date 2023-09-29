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

    public enum State
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
        health = CalculateZombieHealth();
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
                if (isInAttackRange())
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
                //TODO Change this timer to an animation event so it is on the same moment as the hit
                /*
                attackTimer += Time.deltaTime;
                if (attackTimer >= attackSpeed)
                {
                    Attack();
                    attackTimer = 0;
                    state = State.walking;    
                }
                */

                break;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }

    public void Attack()
    {
        if (isInAttackRange())
        {
            player.healthSystem.Damage(damageAmount);
        }

        state = State.walking;
    }

    private bool isInAttackRange()
    {
        float distance = (attackPoint.position - player.transform.position).magnitude;
        if (distance < attackRange)
        {
            return true;
        }

        return false;
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
    
    private int CalculateZombieHealth()
    {
        int health = 150 + Mathf.Min((waveManager.GetCurrentWave() - 1), 8) * 100;
        health = (int)Mathf.Floor(health * Mathf.Pow(1.1f, Mathf.Max(waveManager.GetCurrentWave() - 9, 0)));

        return health;
    }

    public State GetState()
    {
        return state;
    }
}
