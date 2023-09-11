using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public void Spawn(GameObject monster)
    {
        Instantiate(monster, transform.position, Quaternion.identity);
    }
}
