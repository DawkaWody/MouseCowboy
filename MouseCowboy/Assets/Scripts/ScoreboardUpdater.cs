using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class ScoreboardUpdater : MonoBehaviour
{
    [SerializeField]
    private float _timeBeforeContinuing;

    private float _continueCountdown;
    private bool _countDown = true;

    private GameObject _cowboy, _mouse;

    // Start is called before the first frame update
    void Start()
    {
        _continueCountdown = _timeBeforeContinuing + .5f;
        SceneManager.sceneLoaded += OnNewSceneLoaded;
        _cowboy = GameObject.Find("Cowboy");
        _mouse = GameObject.Find("Mouse");
        OnNewSceneLoaded(SceneManager.GetActiveScene(), new LoadSceneMode());
    }

    // Update is called once per frame
    void Update()
    {
        if (_countDown)
        {
            _continueCountdown -= Time.deltaTime;
            ScoreboardUIController.instance.UpdateGameContinuingText((int)Mathf.Round(_continueCountdown));

            if (_continueCountdown <= 0)
            {
                _countDown = false;
                Destroy(_cowboy);
                Destroy(_mouse);
                GameManager.instance.currentRound++;
                LevelLoader.instance.Load(GameManager.instance.gameScene);
                
            }
        }
    }

    public void Setup()
    {
        _cowboy = GameObject.Find("Cowboy");
        _mouse = GameObject.Find("Mouse");
        _continueCountdown = _timeBeforeContinuing + .5f;
    }

    void OnNewSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name != GameManager.instance.scoreboardScene)
        {
            return;
        }

        PlayerHealthController cowboy = _cowboy.GetComponent<PlayerHealthController>();
        PlayerHealthController mouse = _mouse.GetComponent<PlayerHealthController>();

        bool cowboyWinning = cowboy.Health > 0;
        if (cowboyWinning)
        {
            ScoreboardUIController.instance.CheckCowboyTick(GameManager.instance.currentRound - 1);
        }

        else
        {
            ScoreboardUIController.instance.CheckMouseTick(GameManager.instance.currentRound - 1);
        }

        if (GameManager.instance.currentRound == 5)
        {
            if (CalculateCowboyAccuracy() > CalculateMouseAccuracy())
            {
                GameManager.instance.duelResult = "cowboy";
            }
            else
            {
                GameManager.instance.duelResult = "mouse";
            }

            SceneManager.MoveGameObjectToScene(_cowboy, SceneManager.GetActiveScene());
            SceneManager.MoveGameObjectToScene(_mouse, SceneManager.GetActiveScene());
            Destroy(_cowboy);
            Destroy(_mouse);
            SceneManager.LoadScene(GameManager.instance.endgameScene);
        }

        if (cowboy != null && mouse != null)
        {

            float cowboyAccuracy = CalculateCowboyAccuracy();
            float mouseAccuracy =  CalculateMouseAccuracy();

            ScoreboardUIController.instance.SetCowboyAccuracy(cowboyAccuracy);
            ScoreboardUIController.instance.SetMouseAccuracy(mouseAccuracy);

            bool accuracyEqual = cowboyAccuracy == mouseAccuracy;
            bool cowboyAccuracyWinning = cowboyAccuracy > mouseAccuracy;

            if (accuracyEqual)
            {
                ScoreboardUIController.instance.GlowCowboyText();
                ScoreboardUIController.instance.GlowMouseText();
            }

            else if (cowboyAccuracyWinning)
            {
                ScoreboardUIController.instance.GlowCowboyText();
            }

            else
            {
                ScoreboardUIController.instance.GlowMouseText();
            }
        }
    }

    float CalculateCowboyAccuracy()
    {
        float acc = (float)ScoreboardUIController.instance.GetCowboyTicks() / (float)GameManager.instance.currentRound;
        return acc;
    }

    float CalculateMouseAccuracy()
    {
        float acc = (float)ScoreboardUIController.instance.GetMouseTicks() / (float)GameManager.instance.currentRound;
        return acc;
    }
}
