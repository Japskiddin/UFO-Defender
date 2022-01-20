using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UfoMob : MonoBehaviour, IEnemy
{
    [SerializeField] UfoBullet ufoBullet;
    private const int defaultHp = 1;
    public float speed = 1f;
    public Direction EnemyDirection { get; private set; }
    public int Health { get; private set; }
    private float _timer;
    public float secondsForShoot = 3f;
    private const float maxPosX = 8f;
    private const float minPosX = -8f;
    private const float maxPosY = 4.33f;
    private const float minPosY = 0.78f;
    private const float startPosX = 10.5f;

    public void TakeDamage()
    {
        Health--;
        Debug.Log("UFO Mob take damage, hp - " + Health);
        if (Health <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        Messenger.Broadcast(GameEvent.ENEMY_MOB_KILLED);
        Destroy(this.gameObject);
    }

    public void Shoot()
    {
        UfoBullet bullet = Instantiate(ufoBullet) as UfoBullet;
        bullet.transform.position = transform.position;
    }

    private void Awake()
    {
        transform.position = new Vector3(startPosX, Random.Range(minPosY, maxPosY), transform.position.z);
        EnemyDirection = new Direction
        {
            X = Direction.DIRECTION_RIGHT,
            Y = Random.Range(0, 1) == 1 ? Direction.DIRECTION_TOP : Direction.DIRECTION_BOTTOM
        };
        Health = defaultHp;
    }

    // Update is called once per frame
    void Update()
    {
        CheckScreenEdges();
        Move();
        CheckShoot();
    }

    private void CheckScreenEdges()
    {
        if (transform.position.y >= maxPosY)
        {
            EnemyDirection.Y = Direction.DIRECTION_BOTTOM;
        } else if (transform.position.y <= minPosY)
        {
            EnemyDirection.Y = Direction.DIRECTION_TOP;
        }

        if (transform.position.x >= maxPosX)
        {
            EnemyDirection.X = Direction.DIRECTION_RIGHT;
        } else if (transform.position.x <= minPosX)
        {
            EnemyDirection.X = Direction.DIRECTION_LEFT;
        }
    }

    private void CheckShoot()
    {
        _timer += Time.deltaTime;
        if (_timer > secondsForShoot)
        {
            Shoot();
            _timer = 0;
        }
    }

    public void Move()
    {
        float posX = (EnemyDirection.X == Direction.DIRECTION_RIGHT ? -1 : 1) * speed * Time.deltaTime;
        float posY = (EnemyDirection.Y == Direction.DIRECTION_BOTTOM ? -1 : 1) * speed * Time.deltaTime;
        transform.Translate(posX, posY, transform.position.z);
        Debug.Log("UFO Move = " + transform.position + " Screen width = " + Screen.width);
    }
}