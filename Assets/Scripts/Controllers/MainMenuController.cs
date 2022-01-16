using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour
{
    public void OnExitClick()
    {
        Application.Quit();
    }

    public void OnStartClick()
    {
        Managers.Mission.GoToNext();
    }
}
