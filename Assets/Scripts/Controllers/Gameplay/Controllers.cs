using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

[RequireComponent(typeof(GameplayController))]
[RequireComponent(typeof(EnemyController))]
[RequireComponent(typeof(LevelController))]
[RequireComponent(typeof(HUDController))]
[RequireComponent(typeof(LevelCompleteModal))]
[RequireComponent(typeof(LevelPauseModal))]
[RequireComponent(typeof(GameOverModal))]
[RequireComponent(typeof(SettingsModal))]
public class Controllers : MonoBehaviour
{
    public static HUDController HUD { get; private set; }
    public static LevelCompleteModal LevelComplete { get; private set; }
    public static LevelPauseModal LevelPause { get; private set; }
    public static SettingsModal Settings { get; private set; }
    public static GameOverModal GameOver { get; private set; }
    public static GameplayController Gameplay { get; private set; }
    public static EnemyController Enemy { get; private set; }
    public static LevelController Level { get; private set; }

    private void Awake()
    {
        Gameplay = GetComponent<GameplayController>();
        if (Gameplay == null)
        {
            throw new NullReferenceException("Gameplay controller is null");
        }
        Enemy = GetComponent<EnemyController>();
        if (Enemy == null)
        {
            throw new NullReferenceException("Enemy controller is null");
        }
        Level = GetComponent<LevelController>();
        if (Level == null)
        {
            throw new NullReferenceException("Level controller is null");
        }
        HUD = GetComponent<HUDController>();
        if (HUD == null)
        {
            throw new NullReferenceException("HUD controller is null");
        }
        LevelComplete = GetComponent<LevelCompleteModal>();
        if (LevelComplete == null)
        {
            throw new NullReferenceException("Level Complete controller is null");
        }
        LevelPause = GetComponent<LevelPauseModal>();
        if (LevelPause == null)
        {
            throw new NullReferenceException("Level Pause controller is null");
        }
        Settings = GetComponent<SettingsModal>();
        if (Settings == null)
        {
            throw new NullReferenceException("Settings controller is null");
        }
        GameOver = GetComponent<GameOverModal>();
        if (GameOver == null)
        {
            throw new NullReferenceException("Game Over controller is null");
        }
    }
}