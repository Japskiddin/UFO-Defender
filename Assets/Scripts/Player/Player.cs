using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private PlayerBullet bulletPrefab;
    public float secondsForShoot = 0.5f;
    private float _timer;
    private bool _pause;

    private void Awake()
    {
        _pause = false;
        Messenger<bool>.AddListener(GameEvent.GAME_PAUSE, OnGamePause);
    }

    private void OnDestroy()
    {
        Messenger<bool>.RemoveListener(GameEvent.GAME_PAUSE, OnGamePause);
    }

    // Update is called once per frame
    void Update()
    {
        if (!_pause)
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

            _timer = 0;
        }
    }

    private void OnGamePause(bool value)
    {
        _pause = value;
    }
}