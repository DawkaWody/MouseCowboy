using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [Header("Localization")]
    public string playerprefsLocaleKey;

    [Header("Scene Loading")]
    public string menuScene;
    public string joiningScene;
    public string creditsScene;
    public string settingsScene;
    public string gameScene;
    public string scoreboardScene;
    public string endgameScene;

    [Header("SettingsStorage")]
    public float masterVolume = 1;
    public float sfxVolume = 1;
    public float brightness = 1;

    [Header("DuelStorage")]
    public int currentRound = 1;
    public string duelResult;

    private void Awake()
    {
        if (instance != null) 
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        SceneManager.sceneLoaded += OnNewSceneLoaded;
    }

    void OnNewSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (gameScene == scene.name)
        {
            foreach (PlayerInputHandler playerInput in FindObjectsOfType<PlayerInputHandler>())
            {
                if (!playerInput.inGame)
                {
                    playerInput.inGame = true;
                }
                else
                {
                    playerInput.Reconfigure();
                }
            }
        }

        else if (scoreboardScene == scene.name)
        {
            GameObject.Find("Canvas").GetComponent<ScoreboardUpdater>().Setup();
            for (int i = 0; i < currentRound; i++)
            {
                if (PlayerPrefs.GetInt("CowboyTick_" + i, 0) == 1)
                {
                    ScoreboardUIController.instance.CheckCowboyTick(i);
                }
            }

            for (int i = 0; i < currentRound; i++)
            {
                if (PlayerPrefs.GetInt("MouseTick_" + i, 0) == 1)
                {
                    ScoreboardUIController.instance.CheckMouseTick(i);
                }
            }
        }

        else if (endgameScene ==  scene.name)
        {
            SceneManager.MoveGameObjectToScene(GameObject.Find("JoiningController"), SceneManager.GetActiveScene());
            Destroy(GameObject.Find("JoiningController"));
        }

        else if (menuScene == scene.name)
        {
            duelResult = "";
            currentRound = 1;
        }

        else if (settingsScene == scene.name)
        {
            GameObject.Find("Canvas").GetComponent<SettingsUIController>().AdjustSliders(masterVolume, brightness);
        }
    }

    private void OnApplicationQuit()
    {
        for (int i = 0; i < 5; i++)
        {
            PlayerPrefs.DeleteKey("CowboyTick_" + i);
            PlayerPrefs.DeleteKey("MouseTick_" + i);
        }
    }
}
