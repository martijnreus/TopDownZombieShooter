using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointManager : MonoBehaviour
{
    private PlayerShoot playerShoot;

    private int currentPointAmount;

    private void Awake()
    {
        playerShoot = FindObjectOfType<PlayerShoot>();
    }

    private void Start()
    {
        playerShoot.OnHitZombieAction += PlayerShoot_OnHitZombieAction;

        currentPointAmount = 0;
    }

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

    public int GetCurrentPointAmount()
    {
        return currentPointAmount;
    }
}
