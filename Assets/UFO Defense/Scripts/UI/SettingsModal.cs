using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsModal : BasePanel
{
    [Header("Properties")]
    [SerializeField] private AudioClip sound;
    [Header("Panel")]
    [SerializeField] private Image settingsMenu;

    private void Awake()
    {
        Init(settingsMenu, sound);
    }

    public void OnSettingsCloseClick()
    {
        PlaySound();
        Controllers.LevelPause.Show();
        Hide();
    }
}