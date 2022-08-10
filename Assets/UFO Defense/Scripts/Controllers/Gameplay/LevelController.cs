using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class LevelController : MonoBehaviour
{
    private const float _offsetX = 2.5f;
    private const float _homeStartX = -4f;
    private const float _homeStartY = -4f;
    private int _homeAlive;
    [Header("Prefabs")]
    [SerializeField] private GameObject explosionPrefab;
    [SerializeField] private GameObject homePrefab;
    [Header("Properties")]
    [SerializeField] private int homeCount = 4;
    [Header("Background")]
    [SerializeField] private SpriteRenderer levelBackground;
    public Sprite[] HomeSprites { get; private set; }

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

    private void OnDestroy()
    {
        Messenger<Vector3>.RemoveListener(GameEvent.CREATE_EXPLOSION, OnCreateExplosion);
        Messenger.RemoveListener(GameEvent.HOME_DESTROYED, OnHomeDestroyed);
    }

    private void PrepareLevel()
    {
        int level = Managers.Scene.CurrentLevel;
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
        HomeSprites = new Sprite[5];
        HomeSprites[0] = Resources.Load("Homes/home_" + level + "_0", typeof(Sprite)) as Sprite;
        HomeSprites[1] = Resources.Load("Homes/home_" + level + "_33", typeof(Sprite)) as Sprite;
        HomeSprites[2] = Resources.Load("Homes/home_" + level + "_66", typeof(Sprite)) as Sprite;
        HomeSprites[3] = Resources.Load("Homes/home_" + level + "_99", typeof(Sprite)) as Sprite;
        HomeSprites[4] = Resources.Load("Homes/home_" + level + "_100", typeof(Sprite)) as Sprite;
    }

    private void LoadBackground(int level)
    {
        Sprite background = Resources.Load("Level backgrounds/level" + level, typeof(Sprite)) as Sprite;
        levelBackground.sprite = background;
    }

    private void CreateHouses(int level)
    {
        for (int i = 0; i < homeCount; i++)
        {
            float posX = (_offsetX * i) + _homeStartX;
            float posY = _homeStartY;
            Instantiate(homePrefab, new Vector3(posX, posY, 0), transform.rotation);
        }
    }

    private void OnCreateExplosion(Vector3 position)
    {
        GameObject explosion = Instantiate(explosionPrefab, new Vector3(position.x, position.y, position.z - 1), transform.rotation);
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