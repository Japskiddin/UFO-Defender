using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : BaseBullet
{
    [Header("Properties")]
    [SerializeField] private float speed = 3.0f;

    private void Awake()
    {
        Init(speed);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            Ufo mob = collision.GetComponent<Ufo>();
            if (mob != null)
            {
                mob.TakeDamage();
                Destroy(this.gameObject);
            }
        }
        else if (collision.CompareTag("EnemyBullet"))
        {
            UfoBullet bullet = collision.GetComponent<UfoBullet>();
            if (bullet != null)
            {
                bullet.Destroy();
                Destroy(this.gameObject);
            }
        }
    }
}