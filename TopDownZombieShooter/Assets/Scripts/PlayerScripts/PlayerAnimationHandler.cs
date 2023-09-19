using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationHandler : MonoBehaviour
{
    private Animator animator;

    private PlayerController playerController;
    private PlayerShoot playerShoot;

    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();
        playerController = GetComponent<PlayerController>();
        playerShoot = GetComponent<PlayerShoot>();
    }

    private void Update()
    {
        UpdateAnimator();
    }

    private void UpdateAnimator()
    {
        animator.SetBool("isWalkingForwards", IsWalkingFoward());
        animator.SetBool("isWalking", playerController.GetMoveDirection() != Vector2.zero);
    }

    private bool IsWalkingFoward()
    {
        float moveDirection = playerController.GetMoveDirection().x;
        float aimDirection = playerShoot.GetAimDirection().x;

        if (aimDirection < 0 && moveDirection > 0 || aimDirection > 0 && moveDirection < 0)
        {
            return false;
        }
        return true;
    }
}
