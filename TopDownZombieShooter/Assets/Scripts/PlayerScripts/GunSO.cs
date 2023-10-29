using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GunSO", menuName = "ScriptableObjects/GunSO", order = 1)]
public class GunSO : ScriptableObject
{
    [Header("Gun Info")]
    public string gunName;
    public int baseDamage;
    public int bulletsPerShot;
    public int bulletCapacity;   
    public float timeBetweenShots; 
    public float reloadTime;
    public GunType gunType;
    public float bulletSpreadAngle;

    [Header("Visual Info")]
    public Sprite gunSprite;
    public Sprite gunUISprite;
    public Vector2 shootVisualOffset;

    [Header("Purchase Info")]
    public int gunPrice;
    public int ammoPrice;

    [Header("Audio Info")]
    public AudioClip shootSound;
    public AudioClip reloadSound;
    public AudioClip emptySound;
    public float shootSoundTimeBetweenPlay;

    public enum GunType
    {
        Automatic,
        SemiAutomatic,
        Shotgun,
        Burst,
    }
}
