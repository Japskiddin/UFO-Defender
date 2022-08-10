using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelCompleteModal : BasePanel
{
    [Header("Properties")]
    [SerializeField] private AudioClip sound;
    [Header("Panel")]
    [SerializeField] private Image levelComplete;

    private void Awake()
    {
        Init(levelComplete, sound);
    }

    public void OnExitClick()
    {
        PlaySound();
        Managers.Scene.OpenMainMenu();
    }

    public void OnNextLevelClick()
    {
        Managers.Scene.GoToNext();
    }
}