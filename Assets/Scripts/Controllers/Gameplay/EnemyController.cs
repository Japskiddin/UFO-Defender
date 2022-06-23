using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

/// <summary>
/// Controller which spawns enemies on level.
/// </summary>
public class EnemyController : MonoBehaviour
{
    [Header("Prefabs")]
    [SerializeField] private GameObject bossPrefab;
    [SerializeField] private GameObject ufoPrefab;
    [Header("Properties")]
    [SerializeField] private float secondsForSpawn = 3f;
    [SerializeField] private int enemySpawnCount = 10;
    [SerializeField] private int mobTotal = 20;
    private int _mobSpawned = 0;
    private int _bossCount = 0;
    private float _timer;

    private void Awake()
    {
        if (bossPrefab.GetComponent<Ufo>() == null)
        {
            throw new InvalidCastException("Boss prefab must contain Ufo component.");
        }
        if (ufoPrefab.GetComponent<Ufo>() == null)
        {
            throw new InvalidCastException("Mob prefab must contain Ufo component.");
        }
        Messenger<Ufo>.AddListener(GameEvent.ENEMY_MOB_KILLED, OnEnemyMobKilled);
        int level = Managers.Scene.CurrentLevel;
        mobTotal *= level;
        if (level > 1)
        {
            _bossCount = level * 2;
        }
    }

    private void Start()
    {
        Controllers.HUD.UpdateMobTotal(mobTotal);
    }

    private void OnDestroy()
    {
        Messenger<Ufo>.RemoveListener(GameEvent.ENEMY_MOB_KILLED, OnEnemyMobKilled);
    }

    private void Update()
    {
        if (Controllers.Gameplay.GameStatus == GameStatus.Running)
        {
            CheckUfoSpawn();
        }
    }

    private void CheckUfoSpawn()
    {
        bool canSpawn = _mobSpawned < enemySpawnCount && _mobSpawned < mobTotal;
        _timer += Time.deltaTime;
        if (_timer > secondsForSpawn && canSpawn)
        {
            if (_bossCount > 0)
            {
                int type = UnityEngine.Random.Range(1, 10);
                if (type == 2)
                {
                    Instantiate(bossPrefab);
                    _bossCount--;
                }
                else
                {
                    Instantiate(ufoPrefab);
                }
            }
            else
            {
                Instantiate(ufoPrefab);
            }
            _mobSpawned++;
            _timer = 0;
        }
    }

    private void OnEnemyMobKilled(Ufo ufo)
    {
        if (_mobSpawned == 0 || mobTotal == 0) return;
        _mobSpawned--;
        mobTotal--;
        Controllers.HUD.UpdateMobTotal(mobTotal);
        if (mobTotal == 0)
        {
            Controllers.Gameplay.LevelComplete();
        }
    }
}