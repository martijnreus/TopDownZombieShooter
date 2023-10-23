using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PerkMachine : MonoBehaviour, IInteractable
{
    [SerializeField] private PerkSO perk;

    private PerkInventory perkInventory;
    private PointManager pointManager;

    private void Awake()
    {
        perkInventory = FindObjectOfType<PerkInventory>();
        pointManager = FindObjectOfType<PointManager>();
    }

    public void Interact()
    {
        if (!perkInventory.HasPerk(perk) && pointManager.GetCurrentPointAmount() >= perk.cost)
        {
            perkInventory.AddPerk(perk);
            pointManager.RemovePoints(perk.cost);
        }
    }

    public string GetInteractText()
    {
        if (!perkInventory.HasPerk(perk))
        {
            return "Press  E  to  buy  " + perk.name + "\n[Cost: " + perk.cost + "]";
        }        

        return "";
    }
}
