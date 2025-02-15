using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicController : MonoBehaviour
{
    public static MusicController instance;

    [SerializeField]
    private AudioClip _menuMusic;
    [SerializeField]
    private AudioClip _battleMusic;
    [SerializeField]
    private AudioClip _winMusic;

    public float musicVolume = 1;

    private bool _playedMenu;
    private bool _playedBattle;

    private AudioSource _music;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        SceneManager.sceneLoaded += SetMusic;

        _music = GetComponent<AudioSource>();
        DontDestroyOnLoad(this.gameObject);

        CheckVolume();
        SetMusic(SceneManager.GetActiveScene(), LoadSceneMode.Single);
    }

    private void SetMusic(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == GameManager.instance.menuScene && !_playedMenu)
        {
            StopMusic();
            MenuMusic();
        }
        else if (scene.name == GameManager.instance.joiningScene)
        {
            StopMusic();
        }
        else if (scene.name == GameManager.instance.gameScene && !_playedBattle)
        {
            StopMusic();
            BattleMusic();
        }
        else if (scene.name == GameManager.instance.endgameScene)
        {
            StopMusic();
            WinMusic();
        }
    }

    public void CheckVolume()
    {
        _music.volume = musicVolume * GameManager.instance.masterVolume;
    }

    void StopMusic()
    {
        _playedMenu = false;
        _music.Stop();
    }

    void MenuMusic()
    {
        _playedMenu = true;
        _music.clip = _menuMusic;
        _music.Play();
        CheckVolume();
    }

    void BattleMusic()
    {
        _playedMenu = false;
        _playedBattle = true;
        _music.clip = _battleMusic;
        _music.Play();
        CheckVolume();
    }

    void WinMusic()
    {
        _playedMenu = false;
        _playedBattle = false;
        _music.clip = _winMusic;
        _music.Play();
    } 
}
