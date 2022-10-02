using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class LevelController : MonoBehaviour
{
    [Header("Prefabs")]
    [SerializeField] private GameObject explosionPrefab;
    [SerializeField] private Home homePrefab;
    [Header("Properties")]
    [SerializeField] private int homeCount = 4;
    [Header("Background")]
    [SerializeField] private SpriteRenderer levelBackground;
    public Sprite[] HomeSprites { get; private set; }
    private int _homeAlive;
    private Home[] _homes = new Home[4];

    private void Awake()
    {
        if (explosionPrefab.GetComponent<Explosion>() == null)
        {
            throw new InvalidCastException("Explosion prefab must contain Explosion component.");
        }
        if (homePrefab.GetComponent<Home>() == null)
        {
            throw new InvalidCastException("Home prefab must contain Home component.");
        }
        _homeAlive = homeCount;
        PrepareLevel();
        Messenger<Vector3>.AddListener(GameEvent.CREATE_EXPLOSION, OnCreateExplosion);
        Messenger.AddListener(GameEvent.HOME_DESTROYED, OnHomeDestroyed);
    }

    private void Update()
    {
        CalculatePosition();
    }

    private void OnDestroy()
    {
        Messenger<Vector3>.RemoveListener(GameEvent.CREATE_EXPLOSION, OnCreateExplosion);
        Messenger.RemoveListener(GameEvent.HOME_DESTROYED, OnHomeDestroyed);
    }

    private void PrepareLevel()
    {
        var level = Managers.Scene.CurrentLevel;
        if (Debug.isDebugBuild)
        {
            Debug.Log($"Current level = {level}");
        }
        LoadBackground(level);
        LoadHomeSprites(level);
        CreateHouses(level);
    }

    private void LoadHomeSprites(int level)
    {
        HomeSprites = Resources.LoadAll<Sprite>("Homes/home_" + level);
    }

    private void LoadBackground(int level)
    {
        var background = Resources.Load("Level backgrounds/level" + level, typeof(Sprite)) as Sprite;
        levelBackground.sprite = background;
    }

    private void CreateHouses(int level)
    {
        for (int i = 0; i < homeCount; i++)
        {
            Home home = Instantiate(homePrefab) as Home;
            home.transform.rotation = transform.rotation;
            _homes[i] = home;
        }
    }

    private void CalculatePosition()
    {
        var cameraPos = new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z);
        var screenBounds = Camera.main.ScreenToWorldPoint(cameraPos);
        var spriteRenderer = homePrefab.transform.GetComponent<SpriteRenderer>();
        var spriteWidth = spriteRenderer.bounds.size.x;
        var spriteHeight = spriteRenderer.bounds.size.y;
        var offset = screenBounds.x * 0.15f + spriteWidth;
        var homePosX = -screenBounds.x * 0.5f;
        for (int i = 0; i < _homes.Length; i++)
        {
            Home home = _homes[i];
            home.transform.position = new Vector3(homePosX + (offset * i), -screenBounds.y + (screenBounds.y * 0.2f), 1);
        }
    }

    private void OnCreateExplosion(Vector3 position)
    {
        var explosion = Instantiate(explosionPrefab, new Vector3(position.x, position.y, position.z - 1), transform.rotation);
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
            Controllers.Gameplay.GameOver();
        }
    }
}