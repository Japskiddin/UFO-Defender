using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private UfoMob originalUfoMob;
    private float _timer;
    public float secondsForSpawn = 3f;
    public int enemySpawnCount = 10;
    private int _mobSpawned = 0;
    public int mobTotal = 20;

    private void Awake()
    {
        Messenger.AddListener(GameEvent.ENEMY_MOB_KILLED, OnEnemyMobKilled);
        UIController uiController = GetComponent<UIController>();
        if (uiController != null)
        {
            uiController.UpdateMobTotal(mobTotal);
        }
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
        bool canSpawn = _mobSpawned < enemySpawnCount && _mobSpawned < mobTotal;
        _timer += Time.deltaTime;
        if (_timer > secondsForSpawn && canSpawn)
        {
            UfoMob mob = Instantiate(originalUfoMob) as UfoMob;
            _mobSpawned++;
            _timer = 0;
        }
    }

    private void OnEnemyMobKilled()
    {
        if (_mobSpawned == 0 || mobTotal == 0) return;
        _mobSpawned--;
        mobTotal--;
        UIController uiController = GetComponent<UIController>();
        if (uiController != null)
        {
            uiController.UpdateMobTotal(mobTotal);
        }
    }
}