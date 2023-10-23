using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PerkInventory : MonoBehaviour
{
    private List<PerkSO> perks = new List<PerkSO>();

    public event EventHandler OnPerkChange;
    public event EventHandler<PerkSO> OnPerkAdd;
    public event EventHandler<PerkSO> OnPerkRemove;

    public void AddPerk(PerkSO perk)
    {
        perks.Add(perk);
        OnPerkChange?.Invoke(this, EventArgs.Empty);
        OnPerkAdd?.Invoke(this, perk);
    }

    public void RemovePerk(PerkSO perk)
    {
        perks.Remove(perk);
        OnPerkChange?.Invoke(this, EventArgs.Empty);
        OnPerkRemove?.Invoke(this, perk);
    }

    public bool HasPerk(PerkSO perk)
    {
        return perks.Contains(perk);
    }
}
