using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

[RequireComponent(typeof(UIController))]
[RequireComponent(typeof(GameController))]
[RequireComponent(typeof(EnemyController))]
[RequireComponent(typeof(LevelController))]
public class Controllers : MonoBehaviour
{
    public static UIController UI { get; private set; }
    public static GameController Game { get; private set; }
    public static EnemyController Enemy { get; private set; }
    public static LevelController Level { get; private set; }

    private void Awake()
    {
        Game = GetComponent<GameController>();
        if (Game == null)
        {
            throw new NullReferenceException("Game controller is null");
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
        UI = GetComponent<UIController>();
        if (UI == null)
        {
            throw new NullReferenceException("UI controller is null");
        }
    }
}