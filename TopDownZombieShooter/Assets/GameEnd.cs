using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEnd : MonoBehaviour, IInteractable
{
    [SerializeField] private int doorCost;
    [SerializeField] private AudioClip purchageSound;

    private PointManager pointManager;
    private Player player;

    private void Start()
    {
        pointManager = FindObjectOfType<PointManager>();
        player = FindObjectOfType<Player>();
    }

    public void Interact()
    {
        if (pointManager.GetCurrentPointAmount() >= doorCost)
        {
            pointManager.RemovePoints(doorCost);
            SoundManager.PlaySound(purchageSound, 1f);
            player.GetHealthSystem().Die();
        }
    }

    public string GetInteractText()
    {
        return "Press  E  to  escape \n[Cost: " + doorCost + "]";
    }
}

