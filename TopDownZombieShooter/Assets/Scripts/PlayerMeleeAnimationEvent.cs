using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMeleeAnimationEvent : MonoBehaviour
{
    [SerializeField] private GameObject playerWeapon;

    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void OnStartMeleeAttack()
    {
        playerWeapon.gameObject.SetActive(false);
    }

    public void OnEndMeleeAttack()
    {
        animator.SetBool("isMeleeing", false);
        playerWeapon.gameObject.SetActive(true);
    }
}
