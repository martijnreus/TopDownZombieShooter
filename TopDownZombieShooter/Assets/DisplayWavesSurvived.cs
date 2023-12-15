using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DisplayWavesSurvived : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI textMeshProUGUI;

    private WaveManager waveManager;

    private void Start()
    {
        waveManager = FindObjectOfType<WaveManager>();
    }

    private void Update()
    {
        textMeshProUGUI.text = "Survived " + Mathf.Max((waveManager.GetCurrentWave() - 1), 0) + " waves";
    }
}
