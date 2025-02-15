using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DuelResultUIController : MonoBehaviour
{
    public static DuelResultUIController instance;

    [SerializeField]
    private TMP_Text _winnerText;
    [SerializeField]
    private Image _winnerImage;
    [SerializeField]
    private Sprite _cowboySprite;
    [SerializeField]
    private Sprite _mouseSprite;

    private void Awake()
    {
        instance = this;
    }

    public void SetWinnerForText(string winner)
    {
        _winnerText.text = winner + " wins! Congratulations!";
    }

    public void SetWinnerForImage(string winner)
    {
        if (winner == "cowboy")
        {
            _winnerImage.sprite = _cowboySprite;
        }
        else if (winner == "mouse")
        {
            _winnerImage.sprite = _mouseSprite;
            _winnerImage.rectTransform.localPosition = new Vector3(-35f, 69f);
        }
    }

    public void ReturnToMenu()
    {
        LevelLoader.instance.Load(GameManager.instance.menuScene);
    }
}
