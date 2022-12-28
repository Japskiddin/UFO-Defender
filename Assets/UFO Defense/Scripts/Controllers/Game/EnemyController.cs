using System;
using UFO_Defense.Scripts.Enemy;
using UFO_Defense.Scripts.Managers;
using UnityEngine;

namespace UFO_Defense.Scripts.Controllers.Game
{
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
        private int _mobSpawned;
        private int _bossCount;
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

            Messenger<Ufo>.AddListener(GameEvent.EnemyMobKilled, OnEnemyMobKilled);
            var level = Manager.Scene.CurrentLevel;
            mobTotal *= level;
            if (level > 1)
            {
                _bossCount = level * 2;
            }
        }

        private void Start()
        {
            Controller.HUD.UpdateMobTotal(mobTotal);
        }

        private void OnDestroy()
        {
            Messenger<Ufo>.RemoveListener(GameEvent.EnemyMobKilled, OnEnemyMobKilled);
        }

        private void Update()
        {
            if (Controller.Gameplay.Status == GameStatus.Running)
            {
                CheckUfoSpawn();
            }
        }

        private void CheckUfoSpawn()
        {
            var canSpawn = _mobSpawned < enemySpawnCount && _mobSpawned < mobTotal;
            _timer += Time.deltaTime;
            if (!(_timer > secondsForSpawn) || !canSpawn) return;
            if (_bossCount > 0)
            {
                var type = UnityEngine.Random.Range(1, 10);
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

        private void OnEnemyMobKilled(Ufo ufo)
        {
            if (_mobSpawned == 0 || mobTotal == 0) return;
            _mobSpawned--;
            mobTotal--;
            Controller.HUD.UpdateMobTotal(mobTotal);
            if (mobTotal == 0)
            {
                Controller.Gameplay.LevelComplete();
            }
        }
    }
}