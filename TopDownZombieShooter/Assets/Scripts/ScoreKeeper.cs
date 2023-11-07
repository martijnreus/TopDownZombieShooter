using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreKeeper : MonoBehaviour
{
    private int currentWave = 0;
    private int currentKills = 0;

    private bool isUpdated = false;

    private Player player;
    private WaveManager waveManager;

    private void Awake()
    {
        player = FindObjectOfType<Player>();
        waveManager = FindObjectOfType<WaveManager>();
    }

    private void Start()
    {
        HealthSystem healthSystem = player.GetHealthSystem();
        healthSystem.OnDead += Die;

        waveManager.OnNextWaveAction += WaveManager_OnNextWaveAction;

        GameManager.instance.gameData.gamesPlayed++;
    }

    private void WaveManager_OnNextWaveAction(object sender, EventArgs e)
    {
        currentWave++;
    }

    private void Die(object sender, EventArgs e)
    {
        if (!isUpdated) 
        {
            UpdateGameData();
        }
    }

    private void UpdateGameData()
    {
        isUpdated = true;

        GameManager.instance.gameData.totalWaves += currentWave;
        if (currentWave > GameManager.instance.gameData.highestWave)
        {
            GameManager.instance.gameData.highestWave = currentWave;
        }
        Debug.Log(currentWave);

        GameManager.instance.gameData.totalKills += currentKills;
        if (currentKills > GameManager.instance.gameData.highestKillAmount)
        {
            GameManager.instance.gameData.highestKillAmount = currentKills;
        }
        Debug.Log(currentKills);

        GameManager.instance.SaveGame();
    }

    public void AddKill()
    {
        currentKills++;
    }
}
