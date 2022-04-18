using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(UIController))]
public class LevelController : MonoBehaviour
{
    private const float _offsetX = 2.5f;
    private const float _homeStartX = -4f;
    private const float _homeStartY = -3.7f;
    private int _homeAlive;
    private UIController _uiController;
    [Header("Prefabs")]
    [SerializeField] private Explosion originalExplosion;
    [Header("Properties")]
    [SerializeField] private int homeCount = 4;
    [Header("Background")]
    [SerializeField] private SpriteRenderer levelBackground;

    private void Awake()
    {
        _uiController = GetComponent<UIController>();
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

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            _uiController.OnGamePause();
        }
    }

    private void PrepareLevel()
    {
        int level = Managers.Mission.CurrentLevel;
        Debug.Log("Current level = " + level);
        CreateHouses(level);
        CreateBackground(level);
    }

    private void CreateBackground(int level)
    {
        Sprite background = Resources.Load("Level backgrounds/level" + level, typeof(Sprite)) as Sprite;
        levelBackground.sprite = background;
    }

    private void CreateHouses(int level)
    {
        Home homePrefab = Resources.Load("Prefabs/home" + level, typeof(Home)) as Home;
        for (int i = 0; i < homeCount; i++)
        {
            Home home = Instantiate(homePrefab) as Home;
            float posX = (_offsetX * i) + _homeStartX;
            float posY = _homeStartY;
            home.transform.position = new Vector3(posX, posY, 0);
        }
    }

    private void OnCreateExplosion(Vector3 position)
    {
        Explosion explosion = Instantiate(originalExplosion) as Explosion;
        explosion.transform.position = new Vector3(position.x, position.y, position.z - 1);
        Debug.Log("EXPLOSION!!! at " + explosion.transform.position);
    }

    private void OnHomeDestroyed()
    {
        _homeAlive--;
        if (_homeAlive <= 0)
        {
            _uiController.OnGameOver();
        }
    }
}