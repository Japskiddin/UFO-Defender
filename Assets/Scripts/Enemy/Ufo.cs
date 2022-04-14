using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ufo : MonoBehaviour
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
    private int _health;
    private int _directionVertical;
    private int _directionHorizontal;

    private void Awake()
    {
        _pause = false;
        Messenger<bool>.AddListener(GameEvent.GAME_PAUSE, OnGamePause);
        transform.position = new Vector3(_startPosX, Random.Range(_minPosY, _maxPosY), transform.position.z);
        _directionHorizontal = _directionLeft;
        _directionVertical = Random.Range(0, 1) == 1 ? _directionTop : _directionBottom;
        _health = defaultHp;
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
            _directionVertical = _directionBottom;
        } else if (transform.position.y <= _minPosY)
        {
            _directionVertical = _directionTop;
        } else
        {
            if (_directionTimer > _secondsForDirection)
            {
                if (Random.value < 0.5f)
                {
                    _directionVertical = _directionBottom;
                } else
                {
                    _directionVertical = _directionTop;
                }
                _directionTimer = 0;
            }
        }

        if (transform.position.x >= _maxPosX)
        {
            _directionHorizontal = _directionRight;
        } else if (transform.position.x <= _minPosX)
        {
            _directionHorizontal = _directionLeft;
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
        float posX = (_directionHorizontal == _directionRight ? -1 : 1) * speed * Time.deltaTime;
        float posY = (_directionVertical == _directionBottom ? -1 : 1) * speed * Time.deltaTime;
        transform.Translate(posX, posY, 0);
        Debug.Log("UFO Move = " + transform.position + " Screen width = " + Screen.width);
    }

    private void OnGamePause(bool value)
    {
        _pause = value;
    }

    public void TakeDamage()
    {
        Messenger<Vector3>.Broadcast(GameEvent.CREATE_EXPLOSION, transform.position);
        _health--;
        Debug.Log("UFO Mob take damage, hp - " + _health);
        if (_health <= 0)
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