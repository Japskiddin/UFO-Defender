using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] PlayerBullet bulletPrefab;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePos = Input.mousePosition;
            Vector3 objPos = Camera.main.WorldToScreenPoint(transform.position);

            if (mousePos.y >= objPos.y && mousePos.x >= objPos.x)
            {
                mousePos.x -= objPos.x;
                mousePos.y -= objPos.y;

                float angle = Mathf.Atan2(mousePos.y, mousePos.x) * Mathf.Rad2Deg;

                Quaternion bulletRotation = Quaternion.Euler(new Vector3(0, 0, angle - 90));
                Quaternion rotation = Quaternion.Euler(new Vector3(0, 0, angle));
                transform.rotation = rotation;

                PlayerBullet bullet = Instantiate(bulletPrefab) as PlayerBullet;
                bullet.transform.rotation = bulletRotation;
            }
        }
    }
}