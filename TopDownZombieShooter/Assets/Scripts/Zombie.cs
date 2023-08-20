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

    private Player player;
    private State state;
    private float attackTimer;

    public HealthSystem healthSystem;

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
                float distance = (transform.position - player.transform.position).magnitude;
                if (distance < attackRange)
                {
                    state = State.attacking;
                    agent.isStopped = true;
                    break;
                }

                Debug.Log(GetComponent<Rigidbody2D>().velocity);
                agent.SetDestination(player.transform.position);
                break;

            case State.attacking:
                attackTimer += Time.deltaTime;
                if (attackTimer >= attackSpeed)
                {
                    Attack();
                    attackTimer = 0;
                    state = State.walking;
                    agent.isStopped = false;
                }

                break;
        }
    }

    private void Attack()
    {
        //TODO check if the player is still in range and hit if it is
        player.healthSystem.Damage(damageAmount);
    }
}
