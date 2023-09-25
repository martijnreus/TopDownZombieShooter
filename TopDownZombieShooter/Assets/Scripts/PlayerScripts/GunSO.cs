using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GunSO", menuName = "ScriptableObjects/GunSO", order = 1)]
public class GunSO : ScriptableObject
{
    public string gunName;
    public int baseDamage;
    public int bulletsPerShot;
    public int bulletCapacity;   
    public float timeBetweenShots; 
    public float reloadTime;
    public GunType gunType;
    public float bulletSpreadAngle;
    public Sprite gunSprite;
    public Sprite gunUISprite;
    public int gunPrice;
    public int ammoPrice;

    public enum GunType
    {
        Automatic,
        SemiAutomatic,
        Shotgun,
        Burst,
    }
}
