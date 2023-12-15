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
    [SerializeField] private AudioClip attackSound;

    private Vector3 attackOffset = new Vector3(0, 1f, 0);

    private float deathTime;
    private float despawnDelay = 2f;
    private float timeOfSpawning;

    private Player player;
    private State state;

    public HealthSystem healthSystem;
    private WaveManager waveManager;
    private ScoreKeeper scoreManager;

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
        scoreManager = FindObjectOfType<ScoreKeeper>();
    }

    private void Start()
    {
        health = CalculateZombieHealth();
        healthSystem = new HealthSystem(health);
        healthSystem.OnDead += Die;

        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;

        walkSpeed = UnityEngine.Random.Range(1f, 1f + Mathf.Min((waveManager.GetCurrentWave() - 1), 5) * 0.2f);
        agent.speed = walkSpeed;

        timeOfSpawning = Time.time;

        state = State.spawning;
    }

    private void Die(object sender, EventArgs e)
    {
        waveManager.activeZombies.Remove(gameObject);
        deathTime = Time.time;

        scoreManager.AddKill();

        state = State.despawning;
    }

    private void Update()
    {
        switch (state)
        {
            case State.spawning:
                float spawnDuration = 0.5f;
                if (Time.time - timeOfSpawning > spawnDuration)
                {
                    state = State.walking;
                }
                break;

            case State.walking:
                // check if the player is in attack range
                if (playerIsInRange(attackRange))
                {
                    state = State.attacking;
                    if (agent.isOnNavMesh)
                    {
                        agent.isStopped = true;
                    }
                    break;
                }

                if (agent.isOnNavMesh)
                {
                    agent.isStopped = false;
                }   

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
        // Play attack sound effect
        SoundManager.PlaySound(attackSound, SoundManager.soundEffectVolume, 0.1f);

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
