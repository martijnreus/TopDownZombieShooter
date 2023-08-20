using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private float moveToMouseIntensity;

    private void LateUpdate()
    {
        UpdateCameraFollowPosition();
    }

    private void UpdateCameraFollowPosition()
    {
        Vector3 newCameraPosition = GetPointToFollow();
        newCameraPosition.z = -10;

        transform.position = newCameraPosition;
    }

    private Vector3 GetPointToFollow()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition = new Vector3(mousePosition.x, mousePosition.y, 0); 
        Vector3 pointToFollow = player.transform.position + (mousePosition - player.transform.position) * moveToMouseIntensity;

        return pointToFollow;
    }
}
