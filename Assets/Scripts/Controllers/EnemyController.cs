using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(UIController))]
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
    private bool _pause = false;
    private bool _isBossSpawned = false;
    private UIController _uiController;

    private void Awake()
    {
        _uiController = GetComponent<UIController>();
        _pause = false;
        Messenger<bool>.AddListener(GameEvent.GAME_PAUSE, OnGamePause);
        Messenger<Ufo>.AddListener(GameEvent.ENEMY_MOB_KILLED, OnEnemyMobKilled);
        int level = Managers.Mission.CurrentLevel;
        mobTotal *= level;
        _uiController.UpdateMobTotal(mobTotal);
    }

    private void OnDestroy()
    {
        Messenger<bool>.RemoveListener(GameEvent.GAME_PAUSE, OnGamePause);
        Messenger<Ufo>.RemoveListener(GameEvent.ENEMY_MOB_KILLED, OnEnemyMobKilled);
    }

    private void Update()
    {
        if (!_pause)
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
        if (ufo.IsBoss()) _isBossSpawned = false;
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