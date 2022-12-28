using UFO_Defense.Scripts.Bullet;
using UFO_Defense.Scripts.Controllers.Game;
using UFO_Defense.Scripts.Managers;
using UnityEngine;

namespace UFO_Defense.Scripts.Player
{
    public class Player : MonoBehaviour
    {
        [Header("Prefabs")]
        [SerializeField] private GameObject gun;
        [SerializeField] private GameObject bulletPrefab;
        [Header("Sounds")]
        [SerializeField] private AudioClip shootSound;

        [Header("Properties")]
        [SerializeField] private float secondsForShoot = 0.5f;

        private float _timer;
        private SpriteRenderer _spriteRenderer;

        private void Awake()
        {
            if (bulletPrefab.GetComponent<PlayerBullet>() == null)
            {
                throw new System.NullReferenceException("Bullet prefab must contain PlayerBullet component!");
            }

            _spriteRenderer = transform.GetComponent<SpriteRenderer>();
        }

        private void Update()
        {
            CalculatePosition();
            CheckPlayerShoot();
        }

        private void CalculatePosition()
        {
            var mainCamera = Camera.main;
            if (mainCamera == null)
            {
                throw new System.NullReferenceException("Camera is null!");
            }

            var cameraPos = new Vector3(Screen.width, Screen.height, mainCamera.transform.position.z);
            var screenBounds = mainCamera.ScreenToWorldPoint(cameraPos);
            var bounds = _spriteRenderer.bounds;
            var spriteWidth = bounds.size.x;
            var spriteHeight = bounds.size.y;
            transform.position = new Vector3(-screenBounds.x * 0.98f + spriteWidth / 2f,
                -screenBounds.y * 0.9f + spriteHeight / 2f, transform.position.z);
        }

        private void ChangeRotation(Quaternion rotation)
        {
            gun.transform.rotation = rotation;
        }

        private void CreateBullet(Quaternion bulletRotation)
        {
            var bullet = Instantiate(bulletPrefab);
            bullet.transform.position = gun.transform.position;
            bullet.transform.rotation = bulletRotation;
        }

        private void PlayShootSound()
        {
            Manager.Audio.PlaySound(shootSound);
        }

        private void CheckPlayerShoot()
        {
            if (Controller.Gameplay.Status != GameStatus.Running) return;
            _timer += Time.deltaTime;
            var mousePos = Input.mousePosition;
            var angle = Mathf.Clamp(Mathf.Atan2(mousePos.y, mousePos.x) * Mathf.Rad2Deg, 0, 90);
            var rotation = Quaternion.Euler(new Vector3(0, 0, angle));
            ChangeRotation(rotation);

            if (Input.GetMouseButtonDown(0) && _timer > secondsForShoot)
            {
                Shoot(angle);
            }
        }

        private void Shoot(float angle)
        {
            var bulletRotation = Quaternion.Euler(new Vector3(0, 0, angle - 90));
            CreateBullet(bulletRotation);
            PlayShootSound();
            _timer = 0;
        }
    }
}