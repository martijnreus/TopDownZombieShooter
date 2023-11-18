using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Door : MonoBehaviour, IInteractable
{
    [SerializeField] private int doorCost;
    [SerializeField] private AudioClip purchageSound;
    [SerializeField] private GameObject door;
    [SerializeField] private GameObject mist;

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
            SoundManager.PlaySound(purchageSound, 1f);
            OpenDoor();
        }
    }

    public string GetInteractText()
    {
        return "Press  E  to  open  door \n[Cost: " + doorCost + "]";
    }

    private void OpenDoor()
    {
        door.gameObject.SetActive(false);
        mist.gameObject.SetActive(false);
    }
}
