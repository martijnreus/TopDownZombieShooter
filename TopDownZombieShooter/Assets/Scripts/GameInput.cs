using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Unity.VisualScripting;

public class GameInput: MonoBehaviour
{
    public event EventHandler OnShootAction;
    public event EventHandler OnStopShootAction;
    public event EventHandler OnReloadAction;
    public event EventHandler OnSwitchAction;
    public event EventHandler OnInteractAction;

    public static GameInput Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    private void Update()
    {
        if (Input.GetMouseButton(0))
        {
            OnShootAction?.Invoke(this, EventArgs.Empty);
        }

        if (Input.GetMouseButtonUp(0))
        {
            OnStopShootAction?.Invoke(this, EventArgs.Empty);
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            OnReloadAction?.Invoke(this, EventArgs.Empty);
        }

        if (Input.GetKeyDown(KeyCode.T))
        {
            OnSwitchAction?.Invoke(this, EventArgs.Empty);
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            OnInteractAction?.Invoke(this, EventArgs.Empty);
        }
    }

    public Vector2 GetMovementInput()
    {
        Vector2 inputVector = Vector2.zero;

        if (Input.GetKey(KeyCode.W))
        {
            inputVector.y = 1;
        }
        if (Input.GetKey(KeyCode.S))
        {
            inputVector.y = -1;
        }
        if (Input.GetKey(KeyCode.A))
        {
            inputVector.x = -1; 
        }
        if (Input.GetKey(KeyCode.D))
        {
            inputVector.x = 1;
        }

        return inputVector;
    }
}
