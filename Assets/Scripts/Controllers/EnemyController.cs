using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(UIController))]
public class EnemyController : MonoBehaviour
{
    [SerializeField] private Enemy originalMob;
    private float _timer;
    public float secondsForSpawn = 3f;
    public int enemySpawnCount = 10;
    private int _mobSpawned = 0;
    public int mobTotal = 20;
    private bool _pause;
    private UIController uiController;

    private void Awake()
    {
        uiController = GetComponent<UIController>();
        _pause = false;
        Messenger<bool>.AddListener(GameEvent.GAME_PAUSE, OnGamePause);
        Messenger.AddListener(GameEvent.ENEMY_MOB_KILLED, OnEnemyMobKilled);
        uiController.UpdateMobTotal(mobTotal);
    }

    private void OnDestroy()
    {
        Messenger<bool>.RemoveListener(GameEvent.GAME_PAUSE, OnGamePause);
        Messenger.RemoveListener(GameEvent.ENEMY_MOB_KILLED, OnEnemyMobKilled);
    }

    private void Update()
    {
        if (!_pause) CheckSpawn();
    }

    private void CheckSpawn()
    {
        bool canSpawn = _mobSpawned < enemySpawnCount && _mobSpawned < mobTotal;
        _timer += Time.deltaTime;
        if (_timer > secondsForSpawn && canSpawn)
        {
            Enemy mob = Instantiate(originalMob) as Enemy;
            _mobSpawned++;
            _timer = 0;
        }
    }

    private void OnEnemyMobKilled()
    {
        if (_mobSpawned == 0 || mobTotal == 0) return;
        _mobSpawned--;
        mobTotal--;
        uiController.UpdateMobTotal(mobTotal);
        if (mobTotal == 0) uiController.OnLevelComplete();
    }

    private void OnGamePause(bool value)
    {
        _pause = value;
    }
}