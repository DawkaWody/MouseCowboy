using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuUIController : MonoBehaviour
{
    public void StartGame()
    {
        PlayerPrefs.DeleteAll();
        LevelLoader.instance.Load(GameManager.instance.joiningScene);
    }

    public void ShowSettings()
    {
        LevelLoader.instance.Load(GameManager.instance.settingsScene);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
