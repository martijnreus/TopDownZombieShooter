using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    [SerializeField] private Transform shootTransform;
    [SerializeField] private WallCheck wallCheck;

    public event EventHandler<Zombie> OnHitZombieAction;

    private GunInventory gunInventory;
    private HealthSystem healthSystem;
    private PlayerShootVisual playerShootVisual;
    private PerkInventory perkInventory;

    private float timeLastShot;
    private bool isShooting;
    private bool canShoot = true;
    private bool isHolding;
    private float damageMultiplier = 1;

    private RaycastHit2D? closestHit;

    private List<Zombie> uniqueHits = new List<Zombie>();

    private void Awake()
    {
        gunInventory = GetComponent<GunInventory>();
        playerShootVisual = GetComponent<PlayerShootVisual>();

        perkInventory = GetComponent<PerkInventory>();
        perkInventory.OnPerkAdd += PerkInventory_OnPerkAdd;
    }

    private void Start()
    {
        healthSystem = GetComponent<Player>().GetHealthSystem();
        healthSystem.OnDead += Die;

        GameInput.Instance.OnShootAction += StartShooting;
        GameInput.Instance.OnStopShootAction += StopShooting;
    }

    private void PerkInventory_OnPerkAdd(object sender, PerkSO perk)
    {
        if (perk.type == PerkSO.Type.DoubleTap)
        {
            damageMultiplier = 2;
        }
    }

    private void Die(object sender, EventArgs e)
    {
        canShoot = false;
    }

    private void StartShooting(object sender, EventArgs e)
    {
        if (!canShoot)
        {
            return;
        }

        uniqueHits.Clear();
        Gun currentGun = gunInventory.GetCurrentGun();

        if (currentGun.GetAmmoInWeapon() > 0 && Time.time - timeLastShot > currentGun.GetGunSO().timeBetweenShots)
        {
            if (gunInventory.GetIsReloading())
            {
                gunInventory.CancelReload();
            }

            switch (currentGun.GetGunSO().gunType)
            {
                case GunSO.GunType.Automatic:
                    HandleAutomaticGun();
                    break;
                case GunSO.GunType.SemiAutomatic:
                    HandleSemiAutomaticGun();
                    break;
                case GunSO.GunType.Shotgun:
                    HandleShotgun();
                    break;
                case GunSO.GunType.Burst:
                    HandleBurstGun();
                    break;
            }
        }
        else if (currentGun.GetAmmoInWeapon() == 0 && !isHolding) 
        {
            SoundManager.PlaySound(currentGun.GetGunSO().emptySound, SoundManager.soundEffectVolume);
            isHolding = true;
        }
    }

    private void HandleAutomaticGun()
    {
        gunInventory.GetCurrentGun().UseAmmo();
        isShooting = true;
        ShootBullet();
    }

    private void HandleSemiAutomaticGun()
    {
        if (!isShooting)
        {
            gunInventory.GetCurrentGun().UseAmmo();
            isShooting = true;
            ShootBullet();
        }
    }

    private void HandleShotgun()
    {
        gunInventory.GetCurrentGun().UseAmmo();
        for (int i = 0; i < gunInventory.GetCurrentGun().GetGunSO().bulletsPerShot; i++)
        {
            isShooting = true;
            ShootBullet();
        }

        foreach (Zombie hit in uniqueHits)
        {
            OnHitZombieAction?.Invoke(this, hit);
        }
    }

    private void HandleBurstGun()
    {
        if (!isShooting && Time.time - timeLastShot > gunInventory.GetCurrentGun().GetGunSO().bulletsPerShot * gunInventory.GetCurrentGun().GetGunSO().timeBetweenShots)
        {
            isShooting = true;
            StartCoroutine(DoBurst());
        }
    }

    private IEnumerator DoBurst()
    {
        for (int i = 0; i < gunInventory.GetCurrentGun().GetGunSO().bulletsPerShot; i++)
        {
            gunInventory.GetCurrentGun().UseAmmo();
            ShootBullet();
            yield return new WaitForSeconds(gunInventory.GetCurrentGun().GetGunSO().timeBetweenShots);
        }
    }

    private void StopShooting(object sender, EventArgs e)
    {
        isShooting = false;
        isHolding = false;
    }

    private void ShootBullet()
    {
        timeLastShot = Time.time;

        Vector3 aimDirection = GetShootDirection();

        RaycastHit2D[] raycastHit2DArray = Physics2D.RaycastAll(shootTransform.position, aimDirection);
        closestHit = FindClosestHit(raycastHit2DArray);

        if (closestHit == null)
        {
            return;
        }

        if (raycastHit2DArray.Length > 0)
        {
            Damage(closestHit.Value.collider.gameObject);
        }

        playerShootVisual.CreateShootEffect(closestHit.Value.point);

        SoundManager.PlaySound(gunInventory.GetCurrentGun().GetGunSO().shootSound, SoundManager.soundEffectVolume ,gunInventory.GetCurrentGun().GetGunSO().shootSoundTimeBetweenPlay);
    }

    private RaycastHit2D? FindClosestHit(RaycastHit2D[] hits)
    {
        if (hits.Length == 0)
        {
            return null;
        }

        RaycastHit2D? closestHit = null;

        float closestDistance = 9999;
        foreach (RaycastHit2D hit in hits)
        {
            if (hit.distance < closestDistance && ShouldProcessHit(hit))
            {
                closestHit = hit;
                closestDistance = hit.distance;
            }
        }

        return closestHit;
    }

    private bool ShouldProcessHit(RaycastHit2D hit)
    {
        return !(wallCheck.GetIsInWall() && hit.collider.CompareTag("Wall") && IsAimingDown()) && !hit.collider.CompareTag("Interactable") && !hit.collider.CompareTag("Player");
    }

    private void Damage(GameObject target)
    {
        Zombie zombie = target.GetComponent<Zombie>();
        if (zombie != null)
        {
            if (!uniqueHits.Contains(zombie))
            {
                uniqueHits.Add(zombie);
            }

            zombie.healthSystem.Damage((int)(gunInventory.GetCurrentGun().GetGunSO().baseDamage * damageMultiplier));

            if (gunInventory.GetCurrentGun().GetGunSO().gunType != GunSO.GunType.Shotgun)
            {
                OnHitZombieAction?.Invoke(this, zombie);
            }

            playerShootVisual.CreateBloodEffect(closestHit);
        }
    }

    public Vector3 GetShootDirection()
    {
        Vector3 aimDirection = GetAimDirection();
        float randomSpreadAngle = UnityEngine.Random.Range(-gunInventory.GetCurrentGun().GetGunSO().bulletSpreadAngle / 2, gunInventory.GetCurrentGun().GetGunSO().bulletSpreadAngle / 2);
        Vector3 shootDirection = Quaternion.AngleAxis(randomSpreadAngle, Vector3.forward) * aimDirection;
        return shootDirection;
    }

    public Vector3 GetAimDirection()
    {
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 aimDirection = ((Vector3)mousePosition - shootTransform.position).normalized;
        return aimDirection;
    }

    private bool IsAimingDown()
    {
        return GetShootDirection().y < 0;
    }

    public float GetTimeLastShot()
    {
        return timeLastShot;
    }
}
