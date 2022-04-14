using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour
{
    [SerializeField] private AudioClip sound;
    [SerializeField] private Image mainMenu;
    [SerializeField] private Image settingsMenu;
    [SerializeField] private Image about;

    private void Awake()
    {
        mainMenu.gameObject.SetActive(true);
        settingsMenu.gameObject.SetActive(false);
        about.gameObject.SetActive(false);
    }

    public void OnExitClick()
    {
        Managers.Audio.PlaySound(sound);
        Application.Quit();
    }

    public void OnStartClick()
    {
        Managers.Audio.PlaySound(sound);
        Managers.Mission.GoToNext();
    }

    public void OnSettingsClick()
    {
        Managers.Audio.PlaySound(sound);
        mainMenu.gameObject.SetActive(false);
        settingsMenu.gameObject.SetActive(true);
    }

    public void OnSettingsClose()
    {
        mainMenu.gameObject.SetActive(true);
        settingsMenu.gameObject.SetActive(false);
        Managers.Audio.PlaySound(sound);
    }
}