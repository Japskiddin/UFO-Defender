using UFO_Defense.Scripts.Bullet;
using UFO_Defense.Scripts.Controllers.Game;
using UFO_Defense.Scripts.Managers;
using UnityEngine;

namespace UFO_Defense.Scripts.Enemy
{
    public class Ufo : MonoBehaviour
    {
        private const int DirectionLeft = 0;
        private const int DirectionRight = 1;
        private const int DirectionTop = 2;
        private const int DirectionBottom = 3;
        private const float SecondsForDirection = 1f;

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

            _directionHorizontal = DirectionLeft;
            _directionVertical = Random.Range(0, 1) == 1 ? DirectionTop : DirectionBottom;
            _health = defaultHealth;
            _secondsForShoot = Random.Range(2f, 3f);
            _speed = defaultSpeed + Random.Range(0f, 0.5f);
        }

        private void Start()
        {
            CalculateScreenEdges();
            var spriteRenderer = transform.GetComponent<SpriteRenderer>();
            transform.position = new Vector3(_maxPosX + spriteRenderer.bounds.size.x, Random.Range(_minPosY, _maxPosY),
                transform.position.z);
        }

        private void Update()
        {
            if (Controller.Gameplay.Status != GameStatus.Running) return;
            CheckEdgesCollision();
            Move();
            CheckShoot();
        }

        private void CalculateScreenEdges()
        {
            var mainCamera = Camera.main;
            if (mainCamera == null)
            {
                throw new System.NullReferenceException("Camera is null!");
            }

            var cameraPos = new Vector3(Screen.width, Screen.height, mainCamera.transform.position.z);
            var screenBounds = mainCamera.ScreenToWorldPoint(cameraPos);
            var spriteRenderer = transform.GetComponent<SpriteRenderer>();
            var bounds = spriteRenderer.bounds;
            var spriteWidth = bounds.size.x;
            var spriteHeight = bounds.size.y;
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
                _directionVertical = DirectionBottom;
            }
            else if (transform.position.y <= _minPosY)
            {
                _directionVertical = DirectionTop;
            }
            else
            {
                if (_directionTimer > SecondsForDirection)
                {
                    if (Random.value < 0.5f)
                    {
                        _directionVertical = DirectionBottom;
                    }
                    else
                    {
                        _directionVertical = DirectionTop;
                    }

                    _directionTimer = 0;
                }
            }

            if (transform.position.x >= _maxPosX)
            {
                _directionHorizontal = DirectionRight;
            }
            else if (transform.position.x <= _minPosX)
            {
                _directionHorizontal = DirectionLeft;
            }
        }

        private void CheckShoot()
        {
            _shootTimer += Time.deltaTime;
            if (!(_shootTimer > _secondsForShoot)) return;
            Shoot();
            _shootTimer = 0;
        }

        private void Move()
        {
            var posX = (_directionHorizontal == DirectionRight ? -1 : 1) * _speed * Time.deltaTime;
            var posY = (_directionVertical == DirectionBottom ? -1 : 1) * _speed * Time.deltaTime;
            transform.Translate(posX, posY, 0);
        }

        public void TakeDamage()
        {
            Messenger<Vector3>.Broadcast(GameEvent.CreateExplosion, transform.position);
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
            Messenger<Ufo>.Broadcast(GameEvent.EnemyMobKilled, this);
            Destroy(this.gameObject);
        }

        private void Shoot()
        {
            Instantiate(ufoBullet, transform.position, transform.rotation);
            Manager.Audio.PlaySound(shootSound);
        }
    }
}