using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LevelPauseModal : BasePanel
{
    [Header("Properties")]
    [SerializeField] private AudioClip sound;
    [Header("Panel")]
    [SerializeField] private Image levelPause;

    private void Awake()
    {
        Init(levelPause, sound);
    }

    public void OnExitClick()
    {
        PlaySound();
        Managers.Scene.OpenMainMenu();
    }

    public void OnSettingsClick()
    {
        PlaySound();
        Hide();
        Controllers.Settings.Show();
    }
}