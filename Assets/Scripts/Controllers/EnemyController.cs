using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EnemyController : MonoBehaviour
{
    [Header("Properties")]
    [SerializeField] private Ufo bossPrefab;
    [SerializeField] private Ufo ufoPrefab;
    [SerializeField] private float secondsForSpawn = 3f;
    [SerializeField] private int enemySpawnCount = 10;
    [SerializeField] private int mobTotal = 20;
    private int _mobSpawned = 0;
    private float _timer;
    private bool _isBossSpawned = false;

    private void Awake()
    {
        Messenger<Ufo>.AddListener(GameEvent.ENEMY_MOB_KILLED, OnEnemyMobKilled);
        int level = Managers.Mission.CurrentLevel;
        mobTotal *= level;
    }

    private void OnDestroy()
    {
        Messenger<Ufo>.RemoveListener(GameEvent.ENEMY_MOB_KILLED, OnEnemyMobKilled);
    }

    private void Update()
    {
        if (Controllers.Game.GameStatus == GameStatus.Running)
        {
            CheckUfoSpawn();
        }
    }

    public void Prepare()
    {
        Controllers.UI.UpdateMobTotal(mobTotal);
    }

    private void CheckUfoSpawn()
    {
        bool canSpawn = _mobSpawned < enemySpawnCount && _mobSpawned < mobTotal;
        _timer += Time.deltaTime;
        if (_timer > secondsForSpawn && canSpawn)
        {
            if (Managers.Mission.CurrentLevel > 1 && mobTotal % 3 == 0 && !_isBossSpawned)
            {
                Ufo boss = Instantiate(bossPrefab) as Ufo;
                _isBossSpawned = true;
            }
            else
            {
                Ufo mob = Instantiate(ufoPrefab) as Ufo;
            }
            _mobSpawned++;
            _timer = 0;
        }
    }

    private void OnEnemyMobKilled(Ufo ufo)
    {
        if (_mobSpawned == 0 || mobTotal == 0) return;
        if (ufo.IsBoss())
        {
            _isBossSpawned = false;
        }
        _mobSpawned--;
        mobTotal--;
        Controllers.UI.UpdateMobTotal(mobTotal);
        if (mobTotal == 0)
        {
            Controllers.Game.LevelComplete();
        }
    }
}