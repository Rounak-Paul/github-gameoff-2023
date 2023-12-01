using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;

public class PostProcessingSettingsManager : MonoBehaviour
{
    [Header("Graphics")]
    [SerializeField] private Toggle bloomToggle;
    [SerializeField] private float bloomIntensity;
    [SerializeField] private Toggle vignetteToggle;
    [SerializeField] private float vignetteIntensity;
    [SerializeField] private VolumeProfile globalVolumeProfile;

    private Bloom bloom;
    private Vignette vignette;

    void Start()
    {
        InitPostProcessing();
    }
    private void InitPostProcessing()
    {

        try { PlayerPrefs.GetInt("bloom"); }
        catch { PlayerPrefs.SetInt("bloom", 1); }

        _ = globalVolumeProfile.TryGet(out bloom);
        bloomToggle.isOn = (PlayerPrefs.GetInt("bloom") == 1);
        bloomToggle.onValueChanged.AddListener(delegate { BloomToggled(); });
        BloomUpdate();

        try { PlayerPrefs.GetInt("vignette"); }
        catch { PlayerPrefs.SetInt("vignette", 1); }

        _ = globalVolumeProfile.TryGet(out vignette);
        vignetteToggle.isOn = (PlayerPrefs.GetInt("vignette") == 1);
        vignetteToggle.onValueChanged.AddListener(delegate { VignetteToggled(); });
        VignetteUpdate();
    }
    private void BloomToggled()
    {
        PlayerPrefs.SetInt("bloom", bloomToggle.isOn == true ? 1 : 0);
        BloomUpdate();
    }
    private void BloomUpdate()
    {
        bloom.intensity.value = (PlayerPrefs.GetInt("bloom") == 1) ? bloomIntensity : 0f;
    }

    private void VignetteToggled()
    {
        PlayerPrefs.SetInt("vignette", vignetteToggle.isOn == true ? 1 : 0);
        VignetteUpdate();
    }
    private void VignetteUpdate()
    {
        vignette.intensity.value = (PlayerPrefs.GetInt("vignette") == 1) ? vignetteIntensity : 0f;
    }
}
