using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private AudioClip walkingSound;

    private float soundEffectLength;

    private Rigidbody2D playerBody;
    private Vector2 moveDirection;
    private PerkInventory perkInventory;

    private float speedMultiplier = 1f;

    private void Awake()
    {
        playerBody = GetComponent<Rigidbody2D>();
        perkInventory = GetComponent<PerkInventory>();

        perkInventory.OnPerkAdd += PerkInventory_OnPerkAdd;
    }

    private void PerkInventory_OnPerkAdd(object sender, PerkSO perk)
    {
        if (perk.type == PerkSO.Type.StaminUp) 
        { 
            speedMultiplier = 1.2f;
        }
    }

    private void Update()
    {
        moveDirection = GameInput.Instance.GetMovementInput().normalized;

        PlayWalkingSound();
    }

    private void FixedUpdate()
    {
        playerBody.velocity = moveDirection * moveSpeed * speedMultiplier; 
    }

    public Vector2 GetMoveDirection()
    {
        return moveDirection;
    }

    private void PlayWalkingSound() 
    {
        if (moveDirection != new Vector2(0, 0))
        {
            // Play walking sound
            SoundManager.PlaySound(walkingSound, soundEffectLength);
        }
    }
}
