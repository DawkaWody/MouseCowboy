using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Localization.Settings;
using UnityEngine.UI;

public class SettingsUIController : MonoBehaviour
{
    [SerializeField]
    private Slider _masterVolumeSlider;
    [SerializeField]
    private Slider _brightnessSlider;
    [SerializeField]
    private GameObject _updateInfoPanel;
    [SerializeField]
    private TMP_Dropdown dropdown;

    // Start is called before the first frame update
    void Start()
    {
        AdjustSliders(GameManager.instance.masterVolume, GameManager.instance.brightness);

        dropdown.value = PlayerPrefs.GetInt(GameManager.instance.playerprefsLocaleKey, 0);
    }

    public void AdjustSliders(float masterVolume, float brightness)
    {
        _masterVolumeSlider.value = masterVolume;
        _brightnessSlider.value = brightness;
    }

    public void BackToMenu()
    {
        LevelLoader.instance.Load(GameManager.instance.menuScene);
    }

    public void ShowCredits()
    {
        LevelLoader.instance.Load(GameManager.instance.creditsScene);
    }

    public void ResetSettings()
    {
        _masterVolumeSlider.value = _masterVolumeSlider.maxValue;
        _brightnessSlider.value = 1;
    }

    public void ChangeMasterVolume()
    {
        GameManager.instance.masterVolume = _masterVolumeSlider.normalizedValue;
        MusicController.instance.CheckVolume();
    }

    public void ShowUpdateInfo()
    {
        _updateInfoPanel.SetActive(true);
    }

    public void HideUpdateInfo()
    {
        _updateInfoPanel.SetActive(false);
    }
}
