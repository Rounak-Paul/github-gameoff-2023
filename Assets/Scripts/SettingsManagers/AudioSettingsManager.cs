using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioSettingsManager : MonoBehaviour
{
    [Header("Audio")]
    [SerializeField] Slider musicSlider;
    [SerializeField] Slider sfxSlider;
    [SerializeField] AudioMixer audioMixer;

    private void Start()
    {
        InitMusic();
    }

    private void InitMusic()
    {
        try { PlayerPrefs.GetFloat("musicVolume"); }
        catch { PlayerPrefs.SetFloat("musicVolume", 0f); }

        musicSlider.value = PlayerPrefs.GetFloat("musicVolume");
        musicSlider.onValueChanged.AddListener(delegate { MusicValueChange(); });
        MusicVolumeUpdate();

        try { PlayerPrefs.GetFloat("sfxVolume"); }
        catch { PlayerPrefs.SetFloat("sfxVolume", 0f); }

        sfxSlider.value = PlayerPrefs.GetFloat("sfxVolume");
        sfxSlider.onValueChanged.AddListener(delegate { SfxValueChange(); });
        SfxVolumeUpdate();
    }
    private void SfxVolumeUpdate()
    {
        audioMixer.SetFloat("sfxVolume", PlayerPrefs.GetFloat("sfxVolume"));
    }

    private void MusicVolumeUpdate()
    {
        audioMixer.SetFloat("musicVolume", PlayerPrefs.GetFloat("musicVolume"));
    }

    private void SfxValueChange()
    {
        PlayerPrefs.SetFloat("sfxVolume", sfxSlider.value);
        SfxVolumeUpdate();
    }

    private void MusicValueChange()
    {
        PlayerPrefs.SetFloat("musicVolume", musicSlider.value);
        MusicVolumeUpdate();
    }


}
