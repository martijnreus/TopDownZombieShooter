using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    [SerializeField] private GameObject zombiePrefab;
    [SerializeField] private int maxZombiesAmount = 12;

    //TODO the spawn rate should be depended on the wave

    public List<GameObject> activeZombies;

    private Spawner[] spawners;

    private int zombiesToSpawn;
    private float spawnTimer;
    private int currentWave = 0;
    private float spawnSpeed = 2f;

    private void Start()
    {
        spawners = FindObjectsOfType<Spawner>();
    }

    private void Update()
    {
        if (zombiesToSpawn > 0 && activeZombies.Count < maxZombiesAmount)
        {
            spawnTimer += Time.deltaTime;
            if (spawnTimer > spawnSpeed)
            {
                SpawnZombie();
                spawnTimer = 0f;
            }
        }
        else if (activeZombies.Count == 0 && zombiesToSpawn == 0)
        {
            StartNextWave();
        }
    }

    private void SpawnZombie()
    {
        Spawner spawner = spawners[Random.Range(0, spawners.Length)];
        GameObject newZombie = Instantiate(zombiePrefab, spawner.transform.position, Quaternion.identity);
        activeZombies.Add(newZombie);
        zombiesToSpawn--;
    }

    private void StartNextWave()
    {
        //TODO at a pauze before starting next wave
        currentWave++;
        zombiesToSpawn = (int)(0.000058f * Mathf.Pow(currentWave, 3) + 0.074032f * Mathf.Pow(currentWave, 2) + 0.718119f * Mathf.Pow(currentWave, 1) + 6);

        //Add the calculation
        spawnSpeed = Mathf.Max(2 * Mathf.Pow(0.95f, currentWave - 1), 0.1f);
    }

    public int GetCurrentWave()
    {
        return currentWave;
    }
}
