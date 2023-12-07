using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuSound : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;

    private void Update()
    {
        audioSource.volume = SoundManager.musicVolume * SoundManager.masterVolumeMultiplier;
    }
}
