using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(UIController))]
public class EnemyController : MonoBehaviour
{
    [SerializeField] private Ufo ufoPrefab;
    private float _timer;
    public float secondsForSpawn = 3f;
    public int enemySpawnCount = 10;
    private int _mobSpawned = 0;
    public int mobTotal = 20;
    private bool _pause;
    private UIController _uiController;

    private void Awake()
    {
        _uiController = GetComponent<UIController>();
        _pause = false;
        Messenger<bool>.AddListener(GameEvent.GAME_PAUSE, OnGamePause);
        Messenger.AddListener(GameEvent.ENEMY_MOB_KILLED, OnEnemyMobKilled);
        _uiController.UpdateMobTotal(mobTotal);
    }

    private void OnDestroy()
    {
        Messenger<bool>.RemoveListener(GameEvent.GAME_PAUSE, OnGamePause);
        Messenger.RemoveListener(GameEvent.ENEMY_MOB_KILLED, OnEnemyMobKilled);
    }

    private void Update()
    {
        if (!_pause)
        {
            CheckSpawn();
        }
    }

    private void CheckSpawn()
    {
        bool canSpawn = _mobSpawned < enemySpawnCount && _mobSpawned < mobTotal;
        _timer += Time.deltaTime;
        if (_timer > secondsForSpawn && canSpawn)
        {
            Ufo mob = Instantiate(ufoPrefab) as Ufo;
            _mobSpawned++;
            _timer = 0;
        }
    }

    private void OnEnemyMobKilled()
    {
        if (_mobSpawned == 0 || mobTotal == 0) return;
        _mobSpawned--;
        mobTotal--;
        _uiController.UpdateMobTotal(mobTotal);
        if (mobTotal == 0) _uiController.OnLevelComplete();
    }

    private void OnGamePause(bool value)
    {
        _pause = value;
    }
}