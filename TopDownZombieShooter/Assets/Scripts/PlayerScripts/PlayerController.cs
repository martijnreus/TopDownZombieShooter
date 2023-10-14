using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed;

    private Rigidbody2D playerBody;
    private Vector2 moveDirection;

    private void Awake()
    {
        playerBody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        moveDirection = GameInput.Instance.GetMovementInput().normalized;
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
