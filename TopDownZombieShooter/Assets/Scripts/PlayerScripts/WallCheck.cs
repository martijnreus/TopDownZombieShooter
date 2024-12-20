using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallCheck : MonoBehaviour
{
    private bool isInWall;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Wall")
        {
            isInWall = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Wall")
        {
            isInWall = false;
        }  
    }

    public bool GetIsInWall()
    {
        return isInWall;
    }
}
