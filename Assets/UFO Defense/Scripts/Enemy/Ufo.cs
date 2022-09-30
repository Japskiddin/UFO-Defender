using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ufo : MonoBehaviour
{
    private const int _directionLeft = 0;
    private const int _directionRight = 1;
    private const int _directionTop = 2;
    private const int _directionBottom = 3;

    private const float _secondsForDirection = 1f;

    [Header("Sound FX")]
    [SerializeField] private AudioClip shootSound;
    [Header("Prefab")]
    [SerializeField] private GameObject ufoBullet;
    [Header("Properties")]
    [SerializeField] private float defaultSpeed = 1.5f;
    [SerializeField] private int defaultHealth = 1;

    private float _shootTimer;
    private float _directionTimer;
    private float _secondsForShoot;
    private float _speed;
    private float _minPosY;
    private float _maxPosY;
    private float _minPosX;
    private float _maxPosX;
    private int _health;
    private int _directionVertical;
    private int _directionHorizontal;

    private void Awake()
    {
        if (ufoBullet.GetComponent<UfoBullet>() == null)
        {
            throw new System.NullReferenceException("Ufo bullet must contain UfoBullet component.");
        }
        _directionHorizontal = _directionLeft;
        _directionVertical = Random.Range(0, 1) == 1 ? _directionTop : _directionBottom;
        _health = defaultHealth;
        _secondsForShoot = Random.Range(2f, 3f);
        _speed = defaultSpeed + Random.Range(0f, 0.5f);
    }

    private void Start()
    {
        CalculateScreenEdges();
        transform.position = new Vector3(_maxPosX + transform.GetComponent<SpriteRenderer>().bounds.size.x, Random.Range(_minPosY, _maxPosY), transform.position.z);
    }

    void Update()
    {
        if (Controllers.Gameplay.GameStatus == GameStatus.Running)
        {
            CheckEdgesCollision();
            Move();
            CheckShoot();
        }
    }

    private void CalculateScreenEdges()
    {
        var cameraPos = new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z);
        var screenBounds = Camera.main.ScreenToWorldPoint(cameraPos);
        var spriteRenderer = transform.GetComponent<SpriteRenderer>();
        var spriteWidth = spriteRenderer.bounds.size.x;
        var spriteHeight = spriteRenderer.bounds.size.y;
        _minPosY = -screenBounds.y * 0.2f + spriteHeight;
        _maxPosY = screenBounds.y - spriteHeight;
        _minPosX = -screenBounds.x + spriteWidth / 2f;
        _maxPosX = screenBounds.x - spriteWidth / 2f;
    }

    private void CheckEdgesCollision()
    {
        _directionTimer += Time.deltaTime;
        if (transform.position.y >= _maxPosY)
        {
            _directionVertical = _directionBottom;
        }
        else if (transform.position.y <= _minPosY)
        {
            _directionVertical = _directionTop;
        }
        else
        {
            if (_directionTimer > _secondsForDirection)
            {
                if (Random.value < 0.5f)
                {
                    _directionVertical = _directionBottom;
                }
                else
                {
                    _directionVertical = _directionTop;
                }
                _directionTimer = 0;
            }
        }

        if (transform.position.x >= _maxPosX)
        {
            _directionHorizontal = _directionRight;
        }
        else if (transform.position.x <= _minPosX)
        {
            _directionHorizontal = _directionLeft;
        }
    }

    private void CheckShoot()
    {
        _shootTimer += Time.deltaTime;
        if (_shootTimer > _secondsForShoot)
        {
            Shoot();
            _shootTimer = 0;
        }
    }

    private void Move()
    {
        var posX = (_directionHorizontal == _directionRight ? -1 : 1) * _speed * Time.deltaTime;
        var posY = (_directionVertical == _directionBottom ? -1 : 1) * _speed * Time.deltaTime;
        transform.Translate(posX, posY, 0);
    }

    public void TakeDamage()
    {
        Messenger<Vector3>.Broadcast(GameEvent.CREATE_EXPLOSION, transform.position);
        _health--;
        if (Debug.isDebugBuild)
        {
            Debug.Log($"UFO Mob take damage, hp - {_health}");
        }
        if (_health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Messenger<Ufo>.Broadcast(GameEvent.ENEMY_MOB_KILLED, this);
        Destroy(this.gameObject);
    }

    private void Shoot()
    {
        Instantiate(ufoBullet, transform.position, transform.rotation);
        Managers.Audio.PlaySound(shootSound);
    }
}