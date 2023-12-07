using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundSound : MonoBehaviour
{
    [SerializeField] AudioClip[] audioToPlay;
    [SerializeField] float minTimeNextSound;
    [SerializeField] float maxTimeNextSound;
    [SerializeField] float backGroundSoundVolume = 0.5f;

    private float randomSoundTimer;

    private void Awake()
    {
        ResetRandomTimer();
    }

    private void Start()
    {
        PlayRandomSound();
    }

    private void Update()
    {
        randomSoundTimer -= Time.deltaTime;

        if (randomSoundTimer <= 0) 
        {
            PlayRandomSound();
            ResetRandomTimer();
        }
    }

    private void ResetRandomTimer()
    {
        randomSoundTimer = Random.Range(minTimeNextSound, maxTimeNextSound);
    }

    private void PlayRandomSound()
    {
        SoundManager.PlaySound(audioToPlay[Random.Range(0, audioToPlay.Length - 1)], SoundManager.soundEffectVolume * backGroundSoundVolume);
    }
}
