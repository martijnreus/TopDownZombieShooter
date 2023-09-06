using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallCheck : MonoBehaviour
{
    private bool isInWall;

    private void Update()
    {
        Debug.Log(isInWall);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        isInWall = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        isInWall = false;
    }

    public bool GetIsInWall()
    {
        return isInWall;
    }
}
