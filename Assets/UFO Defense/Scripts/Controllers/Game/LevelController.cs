using UFO_Defense.Scripts.Level;
using UFO_Defense.Scripts.Managers;
using UnityEngine;

namespace UFO_Defense.Scripts.Controllers.Game
{
    public class LevelController : MonoBehaviour
    {
        [Header("Prefabs")] [SerializeField] private GameObject explosionPrefab;
        [SerializeField] private Home homePrefab;

        [Header("Properties")] [SerializeField]
        private int homeCount = 4;

        [Header("Background")] [SerializeField]
        private SpriteRenderer levelBackground;

        public Sprite[] HomeSprites { get; private set; }
        private int _homeAlive;
        private readonly Home[] _homes = new Home[4];
        private SpriteRenderer _homePrefabSpriteRenderer;

        private void Awake()
        {
            if (explosionPrefab.GetComponent<Explosion>() == null)
            {
                throw new System.InvalidCastException("Explosion prefab must contain Explosion component.");
            }

            if (homePrefab.GetComponent<Home>() == null)
            {
                throw new System.InvalidCastException("Home prefab must contain Home component.");
            }

            _homePrefabSpriteRenderer = homePrefab.transform.GetComponent<SpriteRenderer>();
            _homeAlive = homeCount;
            PrepareLevel();
            Messenger<Vector3>.AddListener(GameEvent.CreateExplosion, OnCreateExplosion);
            Messenger.AddListener(GameEvent.HomeDestroyed, OnHomeDestroyed);
        }

        private void Update()
        {
            CalculatePosition();
        }

        private void OnDestroy()
        {
            Messenger<Vector3>.RemoveListener(GameEvent.CreateExplosion, OnCreateExplosion);
            Messenger.RemoveListener(GameEvent.HomeDestroyed, OnHomeDestroyed);
        }

        private void PrepareLevel()
        {
            var level = Manager.Scene.CurrentLevel;
            if (Debug.isDebugBuild)
            {
                Debug.Log($"Current level = {level}");
            }

            LoadBackground(level);
            LoadHomeSprites(level);
            CreateHouses();
        }

        private void LoadHomeSprites(int level)
        {
            var path = string.Concat("Homes/home_", level.ToString());
            HomeSprites = Resources.LoadAll<Sprite>(path);
        }

        private void LoadBackground(int level)
        {
            var path = string.Concat("Level backgrounds/level", level.ToString());
            var background = Resources.Load(path, typeof(Sprite)) as Sprite;
            levelBackground.sprite = background;
        }

        private void CreateHouses()
        {
            for (var i = 0; i < homeCount; i++)
            {
                var home = Instantiate(homePrefab);
                home.transform.rotation = transform.rotation;
                _homes[i] = home;
            }
        }

        private void CalculatePosition()
        {
            var mainCamera = Camera.main;
            if (mainCamera == null)
            {
                throw new System.NullReferenceException("Camera is null!");
            }

            var cameraPos = new Vector3(Screen.width, Screen.height, mainCamera.transform.position.z);
            var screenBounds = mainCamera.ScreenToWorldPoint(cameraPos);
            var spriteWidth = _homePrefabSpriteRenderer.bounds.size.x;
            var offset = screenBounds.x * 0.15f + spriteWidth;
            var homePosX = -screenBounds.x * 0.5f;
            for (var i = 0; i < _homes.Length; i++)
            {
                var home = _homes[i];
                home.transform.position =
                    new Vector3(homePosX + (offset * i), -screenBounds.y + (screenBounds.y * 0.2f), 1);
            }
        }

        private void OnCreateExplosion(Vector3 position)
        {
            var explosion = Instantiate(explosionPrefab, new Vector3(position.x, position.y, position.z - 1),
                transform.rotation);
            if (Debug.isDebugBuild)
            {
                Debug.Log($"EXPLOSION!!! at {explosion.transform.position}");
            }
        }

        private void OnHomeDestroyed()
        {
            _homeAlive--;
            if (_homeAlive <= 0)
            {
                Controller.Gameplay.GameOver();
            }
        }
    }
}