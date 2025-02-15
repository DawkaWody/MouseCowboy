using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinChecker : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if (GameManager.instance.duelResult == "cowboy")
        {
            DuelResultUIController.instance.SetWinnerForText("Cowboy");
            DuelResultUIController.instance.SetWinnerForImage("cowboy");
        }

        else
        {
            DuelResultUIController.instance.SetWinnerForText("Mouse");
            DuelResultUIController.instance.SetWinnerForImage("mouse");
        }
    }
}
