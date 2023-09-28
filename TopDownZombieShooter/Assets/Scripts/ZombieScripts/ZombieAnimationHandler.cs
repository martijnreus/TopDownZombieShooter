using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieAnimationHandler : MonoBehaviour
{
    private Animator animator;

    private Zombie zombie;

    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();
        zombie = GetComponent<Zombie>();
    }

    private void Update()
    {
        if (animator == null) 
        {
            return;
        }

        UpdateAnimator();
    }

    private void UpdateAnimator()
    {
        animator.SetBool("isWalking", zombie.GetState() == Zombie.State.walking);
    }
}
