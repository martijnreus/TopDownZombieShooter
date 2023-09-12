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
    [SerializeField] private int damageAmount;

    [SerializeField] private WallCheck wallCheck;

    private GameInput gameInput;
    private GunInventory gunInventory;

    private float timeLastShot;

    private bool isShooting;
    private bool muzzleFlashIsActive;

    RaycastHit2D closestHit;

    private void Awake()
    {
        gameInput = FindObjectOfType<GameInput>();
        gunInventory = FindObjectOfType<GunInventory>();
    }

    private void Start()
    {
        gameInput.OnShootAction += StartShooting;
        gameInput.OnStopShootAction += StopShooting;
    }

    private void StartShooting(object sender, System.EventArgs e)
    {
        if (gunInventory.GetCurrentGun().GetAmmoInWeapon() > 0 && Time.time - timeLastShot > gunInventory.GetCurrentGun().GetGunSO().timeBetweenShots)
        {
            //TODO make this a enum and switch case
            // In case of a automatic weapon
            if (gunInventory.GetCurrentGun().GetGunSO().isAutomatic)
            {
                ShootBullet();
            }
            else if (!gunInventory.GetCurrentGun().GetGunSO().isAutomatic && isShooting == false)
            {
                ShootBullet();
            }
        }
    }

    private void StopShooting(object sender, System.EventArgs e)
    {
        isShooting = false;
    }

    private void ShootBullet()
    {
        isShooting = true;
        timeLastShot = Time.time;
        gunInventory.GetCurrentGun().UseAmmo();

        Vector3 aimDirection = GetAimDirection();

        RaycastHit2D[] raycastHit2DArray = Physics2D.RaycastAll(shootTransform.position, aimDirection);
        RaycastHit2D? raycastHit2D = FindClosestHit(raycastHit2DArray);

        // If there is no hit dont continue
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
            if (hit.distance < closestDistance)
            {
                //TODO switch the tag with a certain wall collider so the player can only shoot trough the closest wall
                if (!(wallCheck.GetIsInWall() && hit.collider.gameObject.tag == "Wall" && IsAimingDown()) && hit.collider.tag != "Player")
                {
                    closestHit = hit;
                    closestDistance = hit.distance;                 
                }
            }
        }

        return closestHit;
    }

    private void Damage(GameObject target)
    {
        Zombie zombie = target.GetComponent<Zombie>();
        if (zombie != null)
        {
            zombie.healthSystem.Damage(damageAmount);
        }
    }

    private void CreateShootEffect(Vector3 hitPosition)
    {
        Vector3 aimDirection = GetAimDirection();
        if (hitPosition == Vector3.zero)
        {
            hitPosition = shootTransform.position + aimDirection * 20f;
        }

        CreatBulletTracer(shootTransform.position, hitPosition);
        if (muzzleFlashIsActive == false)
        {
            StartCoroutine(DoFlashEffect());
        }
        
    }

    private Vector3 GetAimDirection()
    {
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 aimDirection = ((Vector3)mousePosition - transform.position).normalized;

        // Give a random spread angle to the shooting direction
        float randomSpreadAngle = UnityEngine.Random.Range(-gunInventory.GetCurrentGun().GetGunSO().bulletSpreadAngle / 2,
                                                            gunInventory.GetCurrentGun().GetGunSO().bulletSpreadAngle / 2);
        Vector3 shootDirection = Quaternion.AngleAxis(randomSpreadAngle, Vector3.forward) * aimDirection;

        return shootDirection;
    }

    private bool IsAimingDown()
    {
        if (GetAimDirection().y < 0)
        {
            return true;
        }
        return false;
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
}
