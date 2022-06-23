using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverModal : BasePanel
{
    [Header("Properties")]
    [SerializeField] private AudioClip sound;
    [Header("Panel")]
    [SerializeField] private Image gameOver;

    private void Awake()
    {
        Init(gameOver, sound);
    }

    public void OnExitClick()
    {
        PlaySound();
        Managers.Scene.OpenMainMenu();
    }
}