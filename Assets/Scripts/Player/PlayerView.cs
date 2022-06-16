using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class PlayerView : MonoBehaviour
{
    [Header("Prefabs")]
    [SerializeField] private GameObject gun;
    [SerializeField] private GameObject bulletPrefab;
    [Header("Sounds")]
    [SerializeField] private AudioClip shootSound;
    [Header("Properties")]
    [SerializeField] private float secondsForShoot = 0.5f;

    private PlayerController _controller;

    private void Awake()
    {
        if (bulletPrefab.GetComponent<PlayerBullet>() == null)
        {
            throw new InvalidCastException("Bullet prefab must contain PlayerBullet component!");
        }
        _controller = new PlayerController(this, secondsForShoot);
    }

    private void Update()
    {
        _controller.CheckPlayerShoot();
    }

    public void ChangeRotation(Quaternion rotation)
    {
        gun.transform.rotation = rotation;
    }

    public void CreateBullet(Quaternion bulletRotation)
    {
        GameObject bullet = Instantiate(bulletPrefab);
        bullet.transform.position = gun.transform.position;
        bullet.transform.rotation = bulletRotation;
    }

    public void PlayShootSound()
    {
        Managers.Audio.PlaySound(shootSound);
    }
}