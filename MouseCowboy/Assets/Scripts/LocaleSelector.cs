using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization.Settings;
using TMPro;

public class LocaleSelector : MonoBehaviour
{
    public static LocaleSelector Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(this.gameObject);
        }

        Instance = this;
    }

    void Start()
    {
        SelectLocale(PlayerPrefs.GetInt(
            GameManager.instance.playerprefsLocaleKey, LocalizationSettings.AvailableLocales.Locales.FindIndex(x => x == LocalizationSettings.SelectedLocale)));
    }

    public void SelectLocale(int index)
    {
        LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[index];
        PlayerPrefs.SetInt(GameManager.instance.playerprefsLocaleKey, index);
    }
}
