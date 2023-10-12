using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloodParticle : MonoBehaviour
{
    private float spawnTime;
    private float destroyTime = 0.5f;

    private void Awake()
    {
        spawnTime = Time.time; 
    }

    private void Update()
    {
        if (Time.time > spawnTime + destroyTime) 
        {
            Destroy(gameObject);
        }
    }
}
