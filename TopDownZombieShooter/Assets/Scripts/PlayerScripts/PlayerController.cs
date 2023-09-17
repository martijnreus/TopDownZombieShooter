using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed;

    private Rigidbody2D playerBody;
    private Vector2 moveDirection;

    private GameInput gameInput;

    private void Awake()
    {
        playerBody = GetComponent<Rigidbody2D>();
        gameInput = FindObjectOfType<GameInput>();
    }

    private void Update()
    {
        moveDirection = gameInput.GetMovementInput().normalized;
    }

    private void FixedUpdate()
    {
        playerBody.velocity = moveDirection * moveSpeed;
    }

    public Vector2 GetMoveDirection()
    {
        return moveDirection;
    }
}
