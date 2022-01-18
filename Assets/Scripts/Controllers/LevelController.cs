using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour
{
    public const float offsetX = 2.5f;
    private float _timer;
    public float secondsForSpawn = 3f;
    public int enemyCount = 10;
    [SerializeField] private Home originalHome;
    [SerializeField] private Explosion originalExplosion;
    [SerializeField] private UfoMob originalUfoMob;

    private void Awake()
    {
        Messenger<Vector3>.AddListener(GameEvent.CREATE_EXPLOSION, OnCreateExplosion);
    }

    private void OnDestroy()
    {
        Messenger<Vector3>.RemoveListener(GameEvent.CREATE_EXPLOSION, OnCreateExplosion);
    }

    // Start is called before the first frame update
    void Start()
    {
        CreateHouses();
    }

    private void Update()
    {
        CheckSpawn();
    }

    private void CreateHouses()
    {
        Vector3 startPos = originalHome.transform.position;
        for (int i = 0; i < 4; i++)
        {
            Home home;
            if (i == 0)
            {
                home = originalHome;
            } else
            {
                home = Instantiate(originalHome) as Home;
            }

            float posX = (offsetX * i) + startPos.x;
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

    private void CheckSpawn()
    {
        _timer += Time.deltaTime;
        if (_timer > secondsForSpawn)
        {
            UfoMob mob = Instantiate(originalUfoMob) as UfoMob;
            _timer = 0;
        }
    }
}
