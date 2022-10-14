using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Localization.SmartFormat.PersistentVariables;
using UnityEngine.Localization.Components;

public class LevelPauseModal : BasePanel
{
    [Header("Properties")]
    [SerializeField] private AudioClip sound;
    [Header("Panel")]
    [SerializeField] private Image levelPause;
    [Header("Strings")]
    [SerializeField] private LocalizeStringEvent levelString;

    private void Awake()
    {
        Init(levelPause, sound);
    }

    private void OnEnable()
    {
        Messenger<int>.AddListener(GameEvent.LEVEL_UPDATED, OnRefreshLevel);
    }

    private void OnDisable()
    {
        Messenger<int>.RemoveListener(GameEvent.LEVEL_UPDATED, OnRefreshLevel);
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

    private void OnRefreshLevel(int level)
    {
        Debug.Log("OnRefreshLevel - level = " + level);
        (levelString.StringReference["level_num"] as IntVariable).Value = level;
        levelString.RefreshString();
    }
}