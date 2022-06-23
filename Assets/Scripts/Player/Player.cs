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
        GameObject bullet = Instantiate(bulletPrefab);
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
            Vector3 mousePos = Input.mousePosition;
            float angle = Mathf.Clamp(Mathf.Atan2(mousePos.y, mousePos.x) * Mathf.Rad2Deg, 0, 90);
            Quaternion rotation = Quaternion.Euler(new Vector3(0, 0, angle));
            ChangeRotation(rotation);

            if (Input.GetMouseButtonDown(0) && _timer > secondsForShoot)
            {
                Shoot(angle);
            }
        }
    }

    private void Shoot(float angle)
    {
        Quaternion bulletRotation = Quaternion.Euler(new Vector3(0, 0, angle - 90));
        CreateBullet(bulletRotation);
        PlayShootSound();
        _timer = 0;
    }
}