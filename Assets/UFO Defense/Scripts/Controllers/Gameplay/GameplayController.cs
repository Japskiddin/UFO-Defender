using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class GameplayController : MonoBehaviour
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
                if (!Controllers.Settings.Visible()
                && !Controllers.LevelComplete.Visible()
                && !Controllers.GameOver.Visible())
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
            if (Debug.isDebugBuild)
            {
                if (Input.GetKeyDown(KeyCode.N))
                {
                    LevelComplete();
                }
                if (Input.GetKeyDown(KeyCode.G))
                {
                    GameOver();
                }
            }
        }
    }

    public void OnGameResume()
    {
        GameStatus = GameStatus.Running;
        Time.timeScale = 1;
        Controllers.LevelPause.PlaySound();
        Controllers.LevelPause.Hide();
    }

    public void OnGamePause()
    {
        GameStatus = GameStatus.Paused;
        Time.timeScale = 0;
        Controllers.LevelPause.PlaySound();
        Controllers.LevelPause.Show();
    }

    public void GameOver()
    {
        GameStatus = GameStatus.GameOver;
        Time.timeScale = 0;
        Controllers.GameOver.Show();
    }

    public void LevelComplete()
    {
        GameStatus = GameStatus.Completed;
        Time.timeScale = 0;
        Controllers.LevelComplete.Show();
    }
}