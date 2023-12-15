using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEnd : MonoBehaviour, IInteractable
{
    [SerializeField] private int doorCost;
    [SerializeField] private AudioClip purchageSound;

    private PointManager pointManager;
    private DieCameraAnimation dieCameraAnimation;
    private ScoreKeeper scoreKeeper;

    private void Start()
    {
        pointManager = FindObjectOfType<PointManager>();
        dieCameraAnimation = FindObjectOfType<DieCameraAnimation>();
        scoreKeeper = FindObjectOfType<ScoreKeeper>();
    }

    public void Interact()
    {
        if (pointManager.GetCurrentPointAmount() >= doorCost)
        {
            pointManager.RemovePoints(doorCost);
            SoundManager.PlaySound(purchageSound, 1f);
            dieCameraAnimation.ZoomOut();
            dieCameraAnimation.FadeToBlack();
            dieCameraAnimation.ShowWinScreen();
            scoreKeeper.hasWon = true;
            scoreKeeper.UpdateGameData();
        }
    }

    public string GetInteractText()
    {
        return "Press  E  to  escape \n[Cost: " + doorCost + "]";
    }
}

