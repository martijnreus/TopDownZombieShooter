using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public static class SoundManager
{
    private static Dictionary<AudioClip, float> soundTimerDictionary = new Dictionary<AudioClip, float>();
    private static Dictionary<AudioClip, AudioSource> soundSourceDictionary = new Dictionary<AudioClip, AudioSource>();

    public static float masterVolumeMultiplier = 1f;
    public static float soundEffectVolume = 1f;
    public static float musicvolume = 1f;

    public static void PlaySound(AudioClip sound, float volume)
    {
        GameObject soundGameObject = new GameObject("Sound");
        AudioSource audioSource = soundGameObject.AddComponent<AudioSource>();
        soundSourceDictionary[sound] = audioSource;
        audioSource.PlayOneShot(sound, volume * masterVolumeMultiplier);
        GameObject.Destroy(soundGameObject, 20f);
    }

    public static void PlaySound(AudioClip sound, float volume, float timeBetweenPlay)
    {
        if (ShouldPlaySound(sound, timeBetweenPlay))
        {
            PlaySound(sound, volume);
        }      
    }

    private static bool ShouldPlaySound(AudioClip sound, float timeBetweenPlay)
    {
        if (!soundTimerDictionary.ContainsKey(sound) || Time.time - soundTimerDictionary[sound] > timeBetweenPlay)
        {
            soundTimerDictionary[sound] = Time.time;
            return true;
        }

        return false; 
    }

    public static void StopSound(AudioClip sound)   
    {
        if (!soundSourceDictionary.ContainsKey(sound) || soundSourceDictionary[sound] == null) 
        {
            return;
        }

        AudioSource audioSource = soundSourceDictionary[sound];
        audioSource.Stop();
    }
}
