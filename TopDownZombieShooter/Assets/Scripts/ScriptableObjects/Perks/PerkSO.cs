using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Perk", menuName = "Perk System/Perk")]
public class PerkSO : ScriptableObject
{
    public Type type;
    public int cost;

    public enum Type
    {
        Juggernog,
        SpeedCola,
        DoubleTap,
        QuickRevive,
        MuleKick,
        StaminUp,
    }
}
