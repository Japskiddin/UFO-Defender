using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController
{
    private float _secondsForShoot;
    private float _timer;
    private PlayerView _view;

    public PlayerController(PlayerView view, float secondsForShoot)
    {
        _view = view;
        _secondsForShoot = secondsForShoot;
    }

    public void CheckPlayerShoot()
    {
        if (Controllers.Game.GameStatus == GameStatus.Running)
        {
            _timer += Time.deltaTime;
            Vector3 mousePos = Input.mousePosition;
            float angle = Mathf.Clamp(Mathf.Atan2(mousePos.y, mousePos.x) * Mathf.Rad2Deg, 0, 90);
            Quaternion rotation = Quaternion.Euler(new Vector3(0, 0, angle));
            _view.ChangeRotation(rotation);

            if (Input.GetMouseButtonDown(0) && _timer > _secondsForShoot)
            {
                Shoot(angle);
            }
        }
    }

    private void Shoot(float angle)
    {
        Quaternion bulletRotation = Quaternion.Euler(new Vector3(0, 0, angle - 90));
        _view.CreateBullet(bulletRotation);
        _view.PlayShootSound();
        _timer = 0;
    }
}