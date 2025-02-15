using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Text;

public class MenuUIController : MonoBehaviour
{
    public static MenuUIController instance;

    [SerializeField]
    private GameObject _cowboyJoinText;
    [SerializeField]
    private GameObject _mouseJoinText;
    [SerializeField]
    private GameObject _cowboyJoinedText;
    [SerializeField]
    private GameObject _mouseJoinedText;
    [SerializeField]
    private TMP_Text _gameStartingText;
    [SerializeField]
    private Image _cowboyImage, _mouseImage;

    private void Awake()
    {
        instance = this;
    }

    public void ShowCowboyJoinText()
    {
        _cowboyJoinText.SetActive(true);
    }

    public void ShowMouseJoinText()
    {
        _mouseJoinText.SetActive(true);
    }

    public void HideCowboyJoinText()
    {
        _cowboyJoinText.SetActive(false);
    }

    public void HideMouseJoinText()
    {
        _mouseJoinText.SetActive(false);
    }

    public void ShowCowboyJoinedText()
    {
        _cowboyJoinedText.SetActive(true);
    }

    public void ShowMouseJoinedText()
    {
        _mouseJoinedText.SetActive(true);
    }

    public void HideCowboyJoinedText()
    {
        _cowboyJoinedText.SetActive(false);
    }

    public void HideMouseJoinedText()
    {
        _mouseJoinedText.SetActive(false);
    }

    public void ShowCowboy()
    {
        _cowboyImage.color = new Color(1f, 1f, 1f, 1f);
    }

    public void HideCowboy()
    {
        _cowboyImage.color = new Color(1f, 1f, 1f, .8f);
    }

    public void ShowMouse()
    {
        _mouseImage.color = new Color(1f, 1f, 1f, 1f);
    }

    public void HideMouse()
    {
        _mouseImage.color = new Color(1f, 1f, 1f, .8f);
    }

    public void ShowGameStartingText()
    {
        _gameStartingText.gameObject.SetActive(true);
    }

    public void HideGameStartingText()
    {
        _gameStartingText.gameObject.SetActive(false);
    }

    public void UpdateGameStartingText(int value)
    {
        StringBuilder descText = new StringBuilder();
        for (int i = 0; i < _gameStartingText.text.Length; i++)
        {
            if (_gameStartingText.text[i] != ':')
            {
                descText.Append(_gameStartingText.text[i]);
            }   
            else
            {   
                descText.Append(_gameStartingText.text[i]);
                descText.Append(' ');
                break;
            }
        }

        _gameStartingText.text = descText.ToString() + value;
    }
}
