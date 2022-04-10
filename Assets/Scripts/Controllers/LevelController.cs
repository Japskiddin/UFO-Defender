using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(UIController))]
public class LevelController : MonoBehaviour
{
    private const float _offsetX = 2.5f;
    [SerializeField] private Home originalHome;
    [SerializeField] private Explosion originalExplosion;
    public int homeCount = 4;
    private int _homeAlive;
    private UIController _uiController;

    private void Awake()
    {
        _uiController = GetComponent<UIController>();
        _homeAlive = homeCount;
        Messenger<Vector3>.AddListener(GameEvent.CREATE_EXPLOSION, OnCreateExplosion);
        Messenger.AddListener(GameEvent.HOME_DESTROYED, OnHomeDestroyed);
    }

    private void OnDestroy()
    {
        Messenger<Vector3>.RemoveListener(GameEvent.CREATE_EXPLOSION, OnCreateExplosion);
        Messenger.RemoveListener(GameEvent.HOME_DESTROYED, OnHomeDestroyed);
    }

    // Start is called before the first frame update
    void Start()
    {
        CreateHouses();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            _uiController.OnGamePause();
        }
    }

    private void CreateHouses()
    {
        Vector3 startPos = originalHome.transform.position;
        for (int i = 0; i < homeCount; i++)
        {
            Home home;
            if (i == 0)
            {
                home = originalHome;
            } else
            {
                home = Instantiate(originalHome) as Home;
            }

            float posX = (_offsetX * i) + startPos.x;
            float posY = startPos.y;
            home.transform.position = new Vector3(posX, posY, startPos.z);
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