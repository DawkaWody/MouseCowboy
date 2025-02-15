using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Linq;
using System.Text;

public class ScoreboardUIController : MonoBehaviour
{
    public static ScoreboardUIController instance;

    [SerializeField]
    private GameObject[] _cowboyTicks;
    [SerializeField]
    private GameObject[] _mouseTicks;
    [SerializeField]
    private Animator _cowboyTextAnimator;
    [SerializeField]
    private Animator _mouseTextAnimator;
    [SerializeField]
    private Slider _cowboySlider;
    [SerializeField]
    private Slider _mouseSlider;
    [SerializeField]
    private TMP_Text _gameContinuingText;

    private void Awake()
    {
        instance = this;
    }


    public void CheckCowboyTick(int tickIndex)
    {
        PlayerPrefs.SetInt("CowboyTick_" + tickIndex, 1);
        _cowboyTicks[tickIndex].SetActive(true);
    }

    public void CheckMouseTick(int tickIndex)
    {
        PlayerPrefs.SetInt("MouseTick_" + tickIndex, 1);
        _mouseTicks[tickIndex].SetActive(true);
    }

    public void GlowCowboyText()
    {
        _cowboyTextAnimator.SetBool("TextGlowing", true);
    }

    public void GlowMouseText() 
    {
        _mouseTextAnimator.SetBool("TextGlowing", true);
    }

    public void SetCowboyAccuracy(float accuracy)
    {
        _cowboySlider.value = accuracy;
    }

    public void SetMouseAccuracy(float accuracy)
    {
        _mouseSlider.value = accuracy;
    }

    public int GetCowboyTicks()
    {
        int result = 0;

        for (int i = 0;  i < _cowboyTicks.Length;  i++)
        {
            if (_cowboyTicks[i].activeInHierarchy)
            {
                result++;
            }
        }

        return result;
    }

    public int GetMouseTicks()
    {
        int result = 0;

        for (int i = 0; i < _mouseTicks.Length; i++)
        {
            if (_mouseTicks[i].activeInHierarchy)
            {
                result++;
            }
        }

        return result;
    }

    public void UpdateGameContinuingText(int value)
    {
        StringBuilder _descText = new StringBuilder();
        for (int i = 0; i < _gameContinuingText.text.Length; i++)
        {
            Debug.Log(i);
            if (_gameContinuingText.text[i] != ':')
            {
                _descText.Append(_gameContinuingText.text[i]);
            }
            else
            {
                _descText.Append(_gameContinuingText.text[i]);
                _descText.Append(' ');
                break;
            }
        }

        _gameContinuingText.text = _descText.ToString() + value;
    }
}
