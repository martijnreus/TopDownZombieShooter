using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShootVisual : MonoBehaviour
{
    [SerializeField] private Transform shootTransform;
    [SerializeField] private Material weaponTracerMaterial;
    [SerializeField] private GameObject bloodParticlePrefab;
    [SerializeField] private GameObject shootVisual;
    [SerializeField] private float cameraShakeAmount;

    private PlayerShoot playerShoot;
    private GunInventory gunInventory;

    private bool muzzleFlashIsActive;

    private void Awake()
    {
        playerShoot = GetComponent<PlayerShoot>();
        gunInventory = GetComponent<GunInventory>();
    }

    public void CreateShootEffect(Vector3 hitPosition)
    {
        Vector3 shootDirection = playerShoot.GetShootDirection();
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

    public void CreateBloodEffect(RaycastHit2D? closestHit)
    {
        GameObject bloodParticle = Instantiate(bloodParticlePrefab, closestHit.Value.point,
                    Quaternion.Euler(0, 0, Mathf.Atan2(playerShoot.GetShootDirection().y, playerShoot.GetShootDirection().x) * Mathf.Rad2Deg));
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
        shootVisual.transform.localPosition = (Vector3)gunInventory.GetCurrentGun().GetGunSO().shootVisualOffset;
        shootVisual.SetActive(true);
        muzzleFlashIsActive = true;
        yield return new WaitForSeconds(0.1f);
        shootVisual.SetActive(false);
        muzzleFlashIsActive = false;
    }
}
