using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private float _timer;

    [Header("Properties")]
    [SerializeField] private AudioClip shootSound;
    [SerializeField] private PlayerBullet bulletPrefab;
    [SerializeField] private float secondsForShoot = 0.5f;

    void Update()
    {
        if (Controllers.Game.GameStatus == GameStatus.Running)
        {
            CheckPlayerShoot();
        }
    }

    private void CheckPlayerShoot() {
        _timer += Time.deltaTime;

        Vector3 mousePos = Input.mousePosition;
        float angle = Mathf.Clamp(Mathf.Atan2(mousePos.y, mousePos.x) * Mathf.Rad2Deg, 0, 90);

        Quaternion rotation = Quaternion.Euler(new Vector3(0, 0, angle));
        transform.rotation = rotation;

        if (Input.GetMouseButtonDown(0) && _timer > secondsForShoot) {
            Quaternion bulletRotation = Quaternion.Euler(new Vector3(0, 0, angle - 90));
            PlayerBullet bullet = Instantiate(bulletPrefab) as PlayerBullet;
            bullet.transform.position = transform.position;
            bullet.transform.rotation = bulletRotation;
            Managers.Audio.PlaySound(shootSound);
            _timer = 0;
        }
    }
}