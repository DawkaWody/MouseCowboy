using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using System.Linq;

public class JoiningController : MonoBehaviour
{
    [SerializeField]
    private string _gameScene;
    [SerializeField]
    private float _timeBeforeStart;
    [SerializeField]
    private int _maxPlayers;

    private List<PlayerConfig> _playersJoined = new List<PlayerConfig>();

    private float _startCountdown;
    private bool _countDown;

    public static JoiningController Instance {  get; private set; }

    private void Awake()
    {
        Instance = this;
        DontDestroyOnLoad(Instance);
    }

    private void Update()
    {
        if (_countDown)
        {
            _startCountdown -= Time.deltaTime;
            MenuUIController.instance.UpdateGameStartingText((int)Mathf.Round(_startCountdown));

            if (_startCountdown <= 0)
            {
                LevelLoader.instance.Load(_gameScene);
                _countDown = false;
            }
        }
    }

    public void HandleJoin(PlayerInput playerInput)
    {
        playerInput.transform.SetParent(transform);

        if (!_playersJoined.Any(p => p.PlayerIndex == playerInput.playerIndex))
        {
            int characterId;
            if (_playersJoined.Count > 0) { characterId = 1; }
            else { characterId = 0; }

            _playersJoined.Add(new PlayerConfig(playerInput, playerInput.playerIndex, characterId));
            HandleJoinUI(characterId);
        }

        if (_playersJoined.Count == _maxPlayers)
        {
            StartGame();
        }
    }

    private void HandleJoinUI(int charId)
    {
        if (charId == 0)
        {
            MenuUIController.instance.ShowCowboy();
            MenuUIController.instance.HideCowboyJoinText();
            MenuUIController.instance.ShowCowboyJoinedText();
        }
        else
        {
            MenuUIController.instance.ShowMouse();
            MenuUIController.instance.HideMouseJoinText();
            MenuUIController.instance.ShowMouseJoinedText();
        }
    }

    public void HandleDisconnect(PlayerInput playerInput)
    {
        PlayerConfig disconnectedPlayer = _playersJoined.FirstOrDefault(p => p.PlayerIndex == playerInput.playerIndex);

        _playersJoined.Remove(disconnectedPlayer);
        HandleDisconnectUI(disconnectedPlayer.Character);
    }

    private void HandleDisconnectUI(int charId)
    {
        if (charId == 0)
        {
            MenuUIController.instance.HideCowboy();
            MenuUIController.instance.ShowCowboyJoinText();
            MenuUIController.instance.HideCowboyJoinedText();
        }
        else
        {
            MenuUIController.instance.HideMouse();
            MenuUIController.instance.ShowMouseJoinText();
            MenuUIController.instance.HideMouseJoinedText();
        }
    }

    private void StartGame()
    {
        _countDown = true;
        _startCountdown = _timeBeforeStart + .4f;
        MenuUIController.instance.ShowGameStartingText();
    }
}

class PlayerConfig
{
    public PlayerInput PlayerInput { get; set; }
    public int PlayerIndex { get; set; }
    public int Character { get; set; }
    public bool ready {  get; set; }

    public PlayerConfig(PlayerInput pi, int playerIndex, int character)
    {
        PlayerInput = pi;
        PlayerIndex = playerIndex;
        Character = character;

        ready = false;
    }
}
