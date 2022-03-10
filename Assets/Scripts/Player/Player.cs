using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] PlayerBullet bulletPrefab;
    private float _timer;
    public float secondsForShoot = 0.5f;

    // Update is called once per frame
    void Update()
    {
        _timer += Time.deltaTime;

        Vector3 mousePos = Input.mousePosition;
        float angle = Mathf.Clamp(Mathf.Atan2(mousePos.y, mousePos.x) * Mathf.Rad2Deg, 0, 90);

        Quaternion rotation = Quaternion.Euler(new Vector3(0, 0, angle));
        transform.rotation = rotation;

        if (Input.GetMouseButtonDown(0) && _timer > secondsForShoot)
        {
            Quaternion bulletRotation = Quaternion.Euler(new Vector3(0, 0, angle - 90));
            PlayerBullet bullet = Instantiate(bulletPrefab) as PlayerBullet;
            bullet.transform.position = transform.position;
            bullet.transform.rotation = bulletRotation;

            _timer = 0;
        }
    }
}