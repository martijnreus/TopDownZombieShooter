using UnityEngine;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{
    [SerializeField] Slider masterSlider;
    [SerializeField] Slider soundEffectSlider;
    [SerializeField] Slider musicSlider;

    private void Start()
    {
        masterSlider.value = SoundManager.masterVolumeMultiplier;
        soundEffectSlider.value = SoundManager.soundEffectVolume;
        musicSlider.value = SoundManager.musicVolume;
    }

    public void UpdateMasterVolume()
    {
        SoundManager.masterVolumeMultiplier = masterSlider.value;
    }

    public void UpdateSoundEffectVolume()
    {
        SoundManager.soundEffectVolume = soundEffectSlider.value;
    }

    public void UpdateMusicVolume()
    {
        SoundManager.musicVolume = musicSlider.value;
    }
}
