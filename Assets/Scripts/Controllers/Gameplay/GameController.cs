using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public GameStatus GameStatus { get; private set; }

    private void Awake()
    {
        GameStatus = GameStatus.Running;
        Time.timeScale = 1;
    }

    private void Update()
    {
        if (GameStatus != GameStatus.Completed && GameStatus != GameStatus.GameOver)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (GameStatus == GameStatus.Paused)
                {
                    OnGameResume();
                }
                else
                {
                    OnGamePause();
                }
            }
        }
    }

    public void OnGameResume()
    {
        GameStatus = GameStatus.Running;
        Time.timeScale = 1;
        Controllers.UI.HideGamePauseWindow();
    }

    public void OnGamePause()
    {
        GameStatus = GameStatus.Paused;
        Time.timeScale = 0;
        Controllers.UI.ShowGamePauseWindow();
    }

    public void GameOver()
    {
        GameStatus = GameStatus.GameOver;
        Time.timeScale = 0;
        Controllers.UI.ShowGameOverWindow();
    }

    public void LevelComplete()
    {
        GameStatus = GameStatus.Completed;
        Time.timeScale = 0;
        Controllers.UI.ShowLevelCompleteWindow();
    }
}