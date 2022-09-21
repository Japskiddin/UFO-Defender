using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    private void Awake()
    {
        if (bulletPrefab.GetComponent<PlayerBullet>() == null)
        {
            throw new System.NullReferenceException("Bullet prefab must contain PlayerBullet component!");
        }
    }

    private void Start()
    {
        var screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
        var spriteWidth = transform.GetComponent<SpriteRenderer>().bounds.size.x / 2;
        var spriteHeight = transform.GetComponent<SpriteRenderer>().bounds.size.y / 2;
        transform.position = new Vector3(screenBounds.x * 0.05f + spriteWidth, screenBounds.y * 0.05f + spriteHeight, transform.position.z);
    }

    private void Update()
    {
        CheckPlayerShoot();
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
        Managers.Audio.PlaySound(shootSound);
    }

    private void CheckPlayerShoot()
    {
        if (Controllers.Gameplay.GameStatus == GameStatus.Running)
        {
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
    }

    private void Shoot(float angle)
    {
        var bulletRotation = Quaternion.Euler(new Vector3(0, 0, angle - 90));
        CreateBullet(bulletRotation);
        PlayShootSound();
        _timer = 0;
    }
}