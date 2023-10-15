using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PerkInventory : MonoBehaviour
{
    private List<PerkSO> perks = new List<PerkSO>();

    public event EventHandler OnPerkChange;

    public void AddPerk(PerkSO perk)
    {
        perks.Add(perk);
        OnPerkChange?.Invoke(this, EventArgs.Empty);
    }

    public void RemovePerk(PerkSO perk)
    {
        perks.Remove(perk);
        OnPerkChange?.Invoke(this, EventArgs.Empty);
    }

    public bool HasPerk(PerkSO perk)
    {
        return perks.Contains(perk);
    }
}
