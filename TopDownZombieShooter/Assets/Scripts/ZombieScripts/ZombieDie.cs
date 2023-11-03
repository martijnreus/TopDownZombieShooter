using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ZombieDie : MonoBehaviour
{
    [SerializeField] private CircleCollider2D circleCollider2D;
    [SerializeField] private CapsuleCollider2D capsuleCollider2D;

    private Zombie zombie;

    private void Start()
    {
        zombie = GetComponent<Zombie>();
    }

    private void Update()
    {
        if (zombie.GetState() == Zombie.State.despawning) 
        { 
            DisableHitboxes();
        }
    }

    private void DisableHitboxes()
    {
        circleCollider2D.enabled = false;
        capsuleCollider2D.enabled = false;
    }
}
