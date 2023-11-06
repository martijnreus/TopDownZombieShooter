using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Zombie : MonoBehaviour
{
    [SerializeField] private int health;
    [SerializeField] private float walkSpeed;
    [SerializeField] private float attackRange;
    [SerializeField] private int damageAmount;
    [SerializeField] private Transform attackPoint;

    private Vector3 attackOffset = new Vector3(0, 1f, 0);

    private float deathTime;
    private float despawnDelay = 2f;

    private Player player;
    private State state;

    public HealthSystem healthSystem;
    private WaveManager waveManager;

    private NavMeshAgent agent;

    public enum State
    {
        spawning,
        walking,
        attacking,
        idling,
        despawning,
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
        deathTime = Time.time;

        // This doesnt work yet
        //GameManager.instance.gameData.highestKillAmount++;
        GameManager.instance.gameData.totalKills++;

        state = State.despawning;
    }

    private void Update()
    {
        switch (state)
        {
            case State.spawning:
                state = State.walking;
                break;

            case State.walking:
                // check if the player is in attack range
                if (playerIsInRange(attackRange))
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
                break;

            case State.idling:
                break;

            case State.despawning:
                agent.enabled = false;
                if (Time.time - deathTime > despawnDelay) 
                {
                    Destroy(gameObject);
                }
                break;
        }
    }

    public void Attack()
    {
        if (playerIsInRange(attackRange * 1.7f))
        {
            player.healthSystem.Damage(damageAmount);
        }

        state = State.walking;
    }

    private bool playerIsInRange(float range)
    {
        float distance = (attackPoint.position - (player.transform.position + attackOffset)).magnitude;
        if (distance < range)
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

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
