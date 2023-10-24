using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointManager : MonoBehaviour
{
    private PlayerShoot playerShoot;
    private PlayerMelee playerMelee;

    [SerializeField] private int currentPointAmount;

    private void Awake()
    {
        playerShoot = FindObjectOfType<PlayerShoot>();
        playerMelee = FindObjectOfType<PlayerMelee>();
    }

    private void Start()
    {
        playerShoot.OnHitZombieAction += PlayerShoot_OnHitZombieAction;
        playerMelee.OnMeleeZombieAction += PlayerMelee_OnMeleeZombieAction;

        //currentPointAmount = 0;
    }

    private void PlayerMelee_OnMeleeZombieAction(object sender, Zombie target)
    {
        if (!target.healthSystem.IsDead())
        {
            currentPointAmount += 10;
        }
        else
        {
            currentPointAmount += 130;
        }
    }

    //TODO it is better to put this logic in the playerShoot script 
    private void PlayerShoot_OnHitZombieAction(object sender, Zombie target)
    {
        if (!target.healthSystem.IsDead())
        {
            currentPointAmount += 10;
        }
        else
        {
            currentPointAmount += 50;
        }
    }

    public void RemovePoints(int amount)
    {
        currentPointAmount -= amount;
    }

    public int GetCurrentPointAmount()
    {
        return currentPointAmount;
    }
}
