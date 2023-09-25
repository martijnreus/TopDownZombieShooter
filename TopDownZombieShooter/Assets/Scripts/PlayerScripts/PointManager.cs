using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointManager : MonoBehaviour
{
    private PlayerShoot playerShoot;

    [SerializeField] private int currentPointAmount;

    private void Awake()
    {
        playerShoot = FindObjectOfType<PlayerShoot>();
    }

    private void Start()
    {
        playerShoot.OnHitZombieAction += PlayerShoot_OnHitZombieAction;

        //currentPointAmount = 0;
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
