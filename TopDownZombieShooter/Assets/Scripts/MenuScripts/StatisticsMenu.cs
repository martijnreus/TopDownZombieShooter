using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StatisticsMenu : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI totalKillsText;
    [SerializeField] TextMeshProUGUI mostKillsText;
    [SerializeField] TextMeshProUGUI totalWavesText;
    [SerializeField] TextMeshProUGUI highestWavesText;
    [SerializeField] TextMeshProUGUI totalGamesText;

    private void Update()
    {
        totalKillsText.text = "Total kills: " + GameManager.instance.gameData.totalKills;
        mostKillsText.text = "Most kills: " + GameManager.instance.gameData.highestKillAmount;
        totalWavesText.text = "Total waves: " + GameManager.instance.gameData.totalWaves;
        highestWavesText.text = "Highest wave: " + GameManager.instance.gameData.highestWave;
        totalGamesText.text = "Total games: " + GameManager.instance.gameData.gamesPlayed;
    }
}
