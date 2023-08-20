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

    private GameInput gameInput;

    private void Awake()
    {
        gameInput = FindObjectOfType<GameInput>();
    }

    private void Start()
    {
        gameInput.OnShootAction += Shoot;
    }

    private void Shoot(object sender, System.EventArgs e)
    {
        Vector3 aimDirection = GetAimDirection();

        RaycastHit2D raycastHit2D = Physics2D.Raycast(shootTransform.position, aimDirection);

        if (raycastHit2D != false)
        {
            Damage(raycastHit2D.collider.gameObject);
        }
        
        CreateShootEffect(raycastHit2D.point);
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
        StartCoroutine(DoFlashEffect());
    }

    private Vector3 GetAimDirection()
    {
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 aimDirection = ((Vector3)mousePosition - transform.position).normalized;

        return aimDirection;
    }

    private void CreatBulletTracer(Vector3 fromPosition, Vector3 targetPosition)
    {
        Vector3 shootDirection = (targetPosition - fromPosition).normalized;
        float eulerZ = Mathf.Atan2(shootDirection.y, shootDirection.x) * Mathf.Rad2Deg - 90;
        float distance = Vector2.Distance(fromPosition, targetPosition);
        Vector3 tracerSpawnPosisition = fromPosition + shootDirection * distance * .5f;
        Material tmpWeaponTracerMaterial = new Material(weaponTracerMaterial);
        tmpWeaponTracerMaterial.SetTextureScale("_MainTex", new Vector2(1f, distance / 32f));
        World_Mesh worldMesh = World_Mesh.Create(tracerSpawnPosisition, eulerZ, 1f, distance, tmpWeaponTracerMaterial, null, 10000);

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
        yield return new WaitForSeconds(0.1f);
        muzzleFlash.SetActive(false);
        shootFlash.SetActive(false);
    }
}
