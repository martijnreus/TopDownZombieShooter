using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerInteractUI : MonoBehaviour
{
    [SerializeField] private GameObject containerObject;
    [SerializeField] private TextMeshProUGUI interactTextMeshProUGUI;

    private PlayerInteract playerInteract;

    private void Start()
    {
        playerInteract = FindObjectOfType<PlayerInteract>();
    }

    private void Update()
    {
        if (playerInteract.GetInteractableObject() != null)
        {
            Show(playerInteract.GetInteractableObject());
        }
        else
        {
            Hide();
        }
    }

    private void Show(IInteractable interactableObject)
    {
        containerObject.SetActive(true);
        interactTextMeshProUGUI.text = interactableObject.GetInteractText();
    }

    private void Hide()
    {
        containerObject.SetActive(false);
    }
}
