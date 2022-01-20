using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private UfoMob originalUfoMob;
    private float _timer;
    public float secondsForSpawn = 3f;
    public int enemyCount = 10;
    private int _mobSpawned = 0;

    private void Awake()
    {
        Messenger.AddListener(GameEvent.ENEMY_MOB_KILLED, OnEnemyMobKilled);
    }

    private void OnDestroy()
    {
        Messenger.RemoveListener(GameEvent.ENEMY_MOB_KILLED, OnEnemyMobKilled);
    }

    private void Update()
    {
        CheckSpawn();
    }

    private void CheckSpawn()
    {
        _timer += Time.deltaTime;
        if (_timer > secondsForSpawn && _mobSpawned < enemyCount)
        {
            UfoMob mob = Instantiate(originalUfoMob) as UfoMob;
            _mobSpawned++;
            _timer = 0;
        }
    }

    private void OnEnemyMobKilled()
    {
        if (_mobSpawned == 0) return;
        _mobSpawned--;
    }
}
