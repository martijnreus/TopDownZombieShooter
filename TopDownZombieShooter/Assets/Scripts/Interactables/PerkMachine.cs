using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PerkMachine : MonoBehaviour, IInteractable
{
    [SerializeField] private PerkSO perk;
    [SerializeField] private AudioClip purchageSound;

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
            SoundManager.PlaySound(purchageSound, 1f);
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
