using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Door : MonoBehaviour, IInteractable
{
    [SerializeField] int doorCost;

    private PointManager pointManager;

    private void Awake()
    {
        pointManager = FindObjectOfType<PointManager>();
    }

    public void Interact()
    {
        if (pointManager.GetCurrentPointAmount() >= doorCost) 
        {
            pointManager.RemovePoints(doorCost);
            OpenDoor();
        }
    }

    public string GetInteractText()
    {
        return "Press  E  to  open  door \n[Cost: " + doorCost + "]";
    }

    private void OpenDoor()
    {
        // placeholder replace with actual implementation of opening a door
        gameObject.SetActive(false);
    }
}
