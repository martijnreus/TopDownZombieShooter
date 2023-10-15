using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PerkMachine : MonoBehaviour, IInteractable
{
    [SerializeField] private PerkSO perk;

    private PerkInventory perkInventory;

    private void Awake()
    {
        perkInventory = FindObjectOfType<PerkInventory>();
    }

    public void Interact()
    {
        if (!perkInventory.HasPerk(perk))
        {
            perkInventory.AddPerk(perk);
        }
    }

    public string GetInteractText()
    {
        if (!perkInventory.HasPerk(perk))
        {
            return "Press E to buy " + perk.ToString() + "\n[Cost: " + perk.cost + "]";
        }
        return "You already have " + perk.ToString();
            

    }
}
