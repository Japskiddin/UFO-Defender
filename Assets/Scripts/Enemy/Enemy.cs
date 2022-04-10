using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private const int _directionLeft = 0;
    private const int _directionRight = 1;
    private const int _directionTop = 2;
    private const int _directionBottom = 3;

    private const float _maxPosX = 8f;
    private const float _minPosX = -8f;
    private const float _maxPosY = 4.33f;
    private const float _minPosY = 0.78f;
    private const float _startPosX = 10.5f;
    private const float _secondsForDirection = 1f;

    [SerializeField] private UfoBullet ufoBullet;
    private bool _pause;
    private float _timer;
    private float _directionTimer;
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
        transform.position = new Vector3(_startPosX, Random.Range(_minPosY, _maxPosY), transform.position.z);
        DirectionHorizontal = _directionLeft;
        DirectionVertical = Random.Range(0, 1) == 1 ? _directionTop : _directionBottom;
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
        if (transform.position.y >= _maxPosY)
        {
            DirectionVertical = _directionBottom;
        } else if (transform.position.y <= _minPosY)
        {
            DirectionVertical = _directionTop;
        } else
        {
            if (_directionTimer > _secondsForDirection)
            {
                if (Random.value < 0.5f)
                {
                    DirectionVertical = _directionBottom;
                } else
                {
                    DirectionVertical = _directionTop;
                }
                _directionTimer = 0;
            }
        }

        if (transform.position.x >= _maxPosX)
        {
            DirectionHorizontal = _directionRight;
        } else if (transform.position.x <= _minPosX)
        {
            DirectionHorizontal = _directionLeft;
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
        float posX = (DirectionHorizontal == _directionRight ? -1 : 1) * speed * Time.deltaTime;
        float posY = (DirectionVertical == _directionBottom ? -1 : 1) * speed * Time.deltaTime;
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