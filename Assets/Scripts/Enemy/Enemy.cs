using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public const int DIRECTION_LEFT = 0;
    public const int DIRECTION_RIGHT = 1;
    public const int DIRECTION_TOP = 2;
    public const int DIRECTION_BOTTOM = 3;

    private const float maxPosX = 8f;
    private const float minPosX = -8f;
    private const float maxPosY = 4.33f;
    private const float minPosY = 0.78f;
    private const float startPosX = 10.5f;
    private const float secondsForDirection = 1f;

    [SerializeField] UfoBullet ufoBullet;
    private bool _pause;
    private float _timer, _directionTimer;
    public float speed = 1.5f;
    public float secondsForShoot = 3;
    public int defaultHp = 1;
    public int Health { get; set; }
    public int DirectionVertical { get; set; }
    public int DirectionHorizontal { get; set; }

    private void Awake()
    {
        _pause = false;
        Messenger<bool>.AddListener(GameEvent.GAME_PAUSE, OnGamePause);
        transform.position = new Vector3(startPosX, Random.Range(minPosY, maxPosY), transform.position.z);
        DirectionHorizontal = DIRECTION_LEFT;
        DirectionVertical = Random.Range(0, 1) == 1 ? DIRECTION_TOP : DIRECTION_BOTTOM;
        Health = defaultHp;
    }

    private void OnDestroy()
    {
        Messenger<bool>.RemoveListener(GameEvent.GAME_PAUSE, OnGamePause);
    }

    void Update()
    {
        if (!_pause)
        {
            CheckScreenEdges();
            Move();
            CheckShoot();
        }
    }

    private void CheckScreenEdges()
    {
        _directionTimer += Time.deltaTime;
        if (transform.position.y >= maxPosY)
        {
            DirectionVertical = DIRECTION_BOTTOM;
        } else if (transform.position.y <= minPosY)
        {
            DirectionVertical = DIRECTION_TOP;
        } else
        {
            if (_directionTimer > secondsForDirection)
            {
                if (Random.value < 0.5f)
                {
                    DirectionVertical = DIRECTION_BOTTOM;
                } else
                {
                    DirectionVertical = DIRECTION_TOP;
                }
                _directionTimer = 0;
            }
        }

        if (transform.position.x >= maxPosX)
        {
            DirectionHorizontal = DIRECTION_RIGHT;
        } else if (transform.position.x <= minPosX)
        {
            DirectionHorizontal = DIRECTION_LEFT;
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
        float posX = (DirectionHorizontal == DIRECTION_RIGHT ? -1 : 1) * speed * Time.deltaTime;
        float posY = (DirectionVertical == DIRECTION_BOTTOM ? -1 : 1) * speed * Time.deltaTime;
        transform.Translate(posX, posY, transform.position.z);
        Debug.Log("UFO Move = " + transform.position + " Screen width = " + Screen.width);
    }

    private void OnGamePause(bool value)
    {
        _pause = value;
    }

    public void TakeDamage()
    {
        Messenger<Vector3>.Broadcast(GameEvent.CREATE_EXPLOSION, transform.position);
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
}