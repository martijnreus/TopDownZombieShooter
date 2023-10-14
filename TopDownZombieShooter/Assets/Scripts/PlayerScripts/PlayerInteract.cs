using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    [SerializeField] private float interactRange;
    [SerializeField] private Transform interactTransform;

    private void Start()
    {
        GameInput.Instance.OnInteractAction += OnInteractAction;
    }

    private void OnInteractAction(object sender, EventArgs e)
    {
        TryInteract();
    }

    private void TryInteract()
    {
        Collider2D[] colliderArray = Physics2D.OverlapCircleAll(interactTransform.position, interactRange);
        foreach (Collider2D collider in colliderArray)
        {
            if (collider.TryGetComponent(out IInteractable interactable))
            {
                interactable.Interact();
            }
        }
    }

    public IInteractable GetInteractableObject()
    {
        Collider2D[] colliderArray = Physics2D.OverlapCircleAll(interactTransform.position, interactRange);
        foreach (Collider2D collider in colliderArray)
        {
            if (collider.TryGetComponent(out IInteractable interactable))
            {
                return interactable;
            }
        }
        return null;
    }
}
