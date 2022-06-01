using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    [Header("Properties")]
    [SerializeField] private AudioClip sound;
    [SerializeField] private Text mobTotal;
    [Header("Panels")]
    [SerializeField] private Image gameOver;
    [SerializeField] private Image levelComplete;
    [SerializeField] private Image levelPause;
    [SerializeField] private Image settingsMenu;

    private void Awake()
    {
        gameOver.gameObject.SetActive(false);
        levelComplete.gameObject.SetActive(false);
        levelPause.gameObject.SetActive(false);
        settingsMenu.gameObject.SetActive(false);
    }

    public void UpdateMobTotal(int value)
    {
        mobTotal.text = value.ToString();
    }

    public void ShowGameOverWindow()
    {
        gameOver.gameObject.SetActive(true);
    }

    public void ShowLevelCompleteWindow()
    {
        levelComplete.gameObject.SetActive(true);
    }

    public void ShowGamePauseWindow()
    {
        if (settingsMenu.gameObject.activeSelf
            || levelComplete.gameObject.activeSelf
            || gameOver.gameObject.activeSelf)
        {
            return;
        }

        Managers.Audio.PlaySound(sound);
        levelPause.gameObject.SetActive(true);
    }

    public void HideGamePauseWindow()
    {
        Managers.Audio.PlaySound(sound);
        levelPause.gameObject.SetActive(false);
    }

    public void OnSettingsClick()
    {
        Managers.Audio.PlaySound(sound);
        levelPause.gameObject.SetActive(false);
        settingsMenu.gameObject.SetActive(true);
    }

    public void OnSettingsCloseClick()
    {
        Managers.Audio.PlaySound(sound);
        levelPause.gameObject.SetActive(true);
        settingsMenu.gameObject.SetActive(false);
    }

    public void OnExitClick()
    {
        Managers.Audio.PlaySound(sound);
        Managers.Mission.OpenMainMenu();
    }

    public void OnNextLevelClick()
    {
        Managers.Mission.GoToNext();
    }
}