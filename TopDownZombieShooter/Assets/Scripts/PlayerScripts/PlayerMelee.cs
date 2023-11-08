using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using System;

public class PlayerMelee : MonoBehaviour
{
    [SerializeField] private Transform meleePoint;
    [SerializeField] private float meleeRange;
    [SerializeField] private int meleeDamage;
    [SerializeField] private float meleeSpeed;
    [SerializeField] private GameObject playerWeapon;

    public event EventHandler<Zombie> OnMeleeZombieAction;

    private Animator animator;

    private float timeLastMelee;

    private void Awake()
    {
        GameInput.Instance.OnMeleeAction += Instance_OnMeleeAction;
        animator = GetComponentInChildren<Animator>();
    }

    private void Instance_OnMeleeAction(object sender, System.EventArgs e)
    {
        Debug.Log("doei");
        animator.SetBool("isMeleeing", true);
        playerWeapon.gameObject.SetActive(false);

        // cant melee yet
        if (Time.time - timeLastMelee < meleeSpeed)
        {
            return;
        }

        timeLastMelee = Time.time;

        RaycastHit2D? hit = GetHit();
        if (hit != null) 
        {
            Damage(hit.Value.collider.gameObject);
        }
    }

    private void Damage(GameObject target)
    {
        Zombie zombie = target.GetComponent<Zombie>();
        if (zombie != null)
        {        
            zombie.healthSystem.Damage(meleeDamage);
            OnMeleeZombieAction?.Invoke(this, zombie);
        }
    }

    private RaycastHit2D? GetHit()
    {
        RaycastHit2D[] hits = Physics2D.CircleCastAll(meleePoint.position - new Vector3(0, 0, 1), meleeRange, new Vector3(0, 0, 1));
        RaycastHit2D? closestHit = FindClosestHit(hits);

        return closestHit;
    }

    private RaycastHit2D? FindClosestHit(RaycastHit2D[] hits)
    {
        if (hits.Length == 0)
        {
            return null;
        }

        RaycastHit2D? closestHit = null;

        float closestDistance = 9999;
        foreach (RaycastHit2D hit in hits)
        {
            if (hit.distance < closestDistance && ShouldProcessHit(hit))
            {
                closestHit = hit;
                closestDistance = hit.distance;
            }
        }

        return closestHit;
    }

    private bool ShouldProcessHit(RaycastHit2D hit)
    {
        return !(hit.collider.CompareTag("Wall") || hit.collider.CompareTag("Player"));
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(meleePoint.position, meleeRange);
    }
}
