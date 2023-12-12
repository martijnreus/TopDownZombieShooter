using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using DG.Tweening;
using UnityEngine.Tilemaps;
using System.Numerics;

public class Door : MonoBehaviour, IInteractable
{
    [SerializeField] private int doorCost;
    [SerializeField] private AudioClip purchageSound;
    [SerializeField] private GameObject door;
    [SerializeField] private GameObject[] mistArray;
    [SerializeField] private GameObject[] spawners;

    private PointManager pointManager;
    private WaveManager waveManager;

    private void Awake()
    {
        pointManager = FindObjectOfType<PointManager>();
        waveManager = FindObjectOfType<WaveManager>();
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
        foreach (GameObject mist in mistArray)
        {
            mist.GetComponent<TilemapRenderer>().material.DOFade(0f, 0.5f);
        }

        foreach (GameObject spawner in spawners)
        {
            spawner.SetActive(true);
        }

        waveManager.UpdateSpawners();
        door.gameObject.SetActive(false);
    }
}
