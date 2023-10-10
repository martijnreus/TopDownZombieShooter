using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    [SerializeField] private Transform shootTransform;
    [SerializeField] private Material weaponTracerMaterial;
    [SerializeField] private GameObject muzzleFlash;
    [SerializeField] private GameObject shootFlash;
    [SerializeField] private WallCheck wallCheck;
    [SerializeField] private float cameraShakeAmount;

    public event EventHandler<Zombie> OnHitZombieAction;

    private GameInput gameInput;
    private GunInventory gunInventory;
    private HealthSystem healthSystem;

    private float timeLastShot;
    private bool isShooting;
    private bool muzzleFlashIsActive;
    private bool canShoot = true;

    private RaycastHit2D closestHit;

    private List<Zombie> uniqueHits = new List<Zombie>();

    private void Awake()
    {
        gameInput = FindObjectOfType<GameInput>();
        gunInventory = FindObjectOfType<GunInventory>();
    }

    private void Start()
    {
        healthSystem = GetComponent<Player>().GetHealthSystem();
        healthSystem.OnDead += Die;

        gameInput.OnShootAction += StartShooting;
        gameInput.OnStopShootAction += StopShooting;
    }

    private void Die(object sender, EventArgs e)
    {
        canShoot = false;
    }

    private void StartShooting(object sender, System.EventArgs e)
    {
        if (!canShoot)
        {
            return;
        }

        uniqueHits.Clear();
        GunSO currentGun = gunInventory.GetCurrentGun().GetGunSO();

        if (gunInventory.GetCurrentGun().GetAmmoInWeapon() > 0 && Time.time - timeLastShot > currentGun.timeBetweenShots)
        {
            if (gunInventory.GetIsReloading())
            {
                gunInventory.CancelReload();
            }

            switch (currentGun.gunType)
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
    }

    private void ShootBullet()
    {
        timeLastShot = Time.time;

        Vector3 aimDirection = GetShootDirection();

        RaycastHit2D[] raycastHit2DArray = Physics2D.RaycastAll(shootTransform.position, aimDirection);
        RaycastHit2D? raycastHit2D = FindClosestHit(raycastHit2DArray);

        if (raycastHit2D == null)
        {
            return;
        }

        if (raycastHit2DArray.Length > 0)
        {
            Damage(raycastHit2D.Value.collider.gameObject);
        }

        CreateShootEffect(raycastHit2D.Value.point);
    }

    private RaycastHit2D? FindClosestHit(RaycastHit2D[] hits)
    {
        if (hits.Length == 0)
        {
            return null;
        }

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
        return !(wallCheck.GetIsInWall() && hit.collider.CompareTag("Wall") && IsAimingDown()) && !hit.collider.CompareTag("Player");
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

            zombie.healthSystem.Damage(gunInventory.GetCurrentGun().GetGunSO().baseDamage);

            if (gunInventory.GetCurrentGun().GetGunSO().gunType != GunSO.GunType.Shotgun)
            {
                OnHitZombieAction?.Invoke(this, zombie);
            }
        }
    }

    private void CreateShootEffect(Vector3 hitPosition)
    {
        Vector3 shootDirection = GetShootDirection();
        if (hitPosition == Vector3.zero)
        {
            hitPosition = shootTransform.position + shootDirection * 20f;
        }

        CreatBulletTracer(shootTransform.position, hitPosition);
        if (muzzleFlashIsActive == false)
        {
            StartCoroutine(DoFlashEffect());
        }

        CinemachineShake.Instance.ShakeCamera(cameraShakeAmount, 0.1f);
    }

    private Vector3 GetShootDirection()
    {
        Vector3 aimDirection = GetAimDirection();
        float randomSpreadAngle = UnityEngine.Random.Range(-gunInventory.GetCurrentGun().GetGunSO().bulletSpreadAngle / 2, gunInventory.GetCurrentGun().GetGunSO().bulletSpreadAngle / 2);
        Vector3 shootDirection = Quaternion.AngleAxis(randomSpreadAngle, Vector3.forward) * aimDirection;
        return shootDirection;
    }

    public Vector3 GetAimDirection()
    {
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 aimDirection = ((Vector3)mousePosition - transform.position).normalized;
        return aimDirection;
    }

    private bool IsAimingDown()
    {
        return GetShootDirection().y < 0;
    }

    private void CreatBulletTracer(Vector3 fromPosition, Vector3 targetPosition)
    {
        Vector3 shootDirection = (targetPosition - fromPosition).normalized;
        float eulerZ = Mathf.Atan2(shootDirection.y, shootDirection.x) * Mathf.Rad2Deg - 90;
        float distance = Vector2.Distance(fromPosition, targetPosition);
        Vector3 tracerSpawnPosisition = fromPosition + shootDirection * distance * .5f;
        Material tmpWeaponTracerMaterial = new Material(weaponTracerMaterial);
        tmpWeaponTracerMaterial.SetTextureScale("_MainTex", new Vector2(1f, distance / 32f));
        World_Mesh worldMesh = World_Mesh.Create(tracerSpawnPosisition, eulerZ, 0.5f, distance, tmpWeaponTracerMaterial, null, 10000);
        worldMesh.gameObject.GetComponent<Renderer>().sortingLayerID = SortingLayer.NameToID("BulletTracer");
        StartCoroutine(DoTracerCycle(worldMesh));
    }

    private IEnumerator DoTracerCycle(World_Mesh worldMesh)
    {
        int frame = 0;
        float frameRate = 0.016f;
        worldMesh.SetUVCoords(new World_Mesh.UVCoords(0, 0, 16, 256));
        while (frame < 4)
        {
            yield return new WaitForSeconds(frameRate);
            frame++;
            worldMesh.SetUVCoords(new World_Mesh.UVCoords(16 * frame, 0, 16, 256));
        }

        Destroy(worldMesh.gameObject);
    }

    private IEnumerator DoFlashEffect()
    {
        muzzleFlash.SetActive(true);
        shootFlash.SetActive(true);
        muzzleFlashIsActive = true;
        yield return new WaitForSeconds(0.1f);
        muzzleFlash.SetActive(false);
        shootFlash.SetActive(false);
        muzzleFlashIsActive = false;
    }

    public float GetTimeLastShot()
    {
        return timeLastShot;
    }
}
