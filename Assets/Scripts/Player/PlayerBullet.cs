using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
    private bool _pause;

    [Header("Properties")]
    [SerializeField] private float speed = 3.0f;

    private void Awake()
    {
        _pause = false;
    }

    private void OnEnable()
    {
        UIController.OnGamePauseEvent += OnGamePause;
    }

    private void OnDisable()
    {
        UIController.OnGamePauseEvent -= OnGamePause;
    }

    void Update()
    {
        if (!_pause) transform.Translate(0, speed * Time.deltaTime, 0);
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
        } else if (collision.CompareTag("EnemyBullet"))
        {
            UfoBullet bullet = collision.GetComponent<UfoBullet>();
            if (bullet != null)
            {
                bullet.Destroy();
                Destroy(this.gameObject);
            }
        }
    }

    private void OnBecameInvisible()
    {
        Destroy(this.gameObject);
    }

    private void OnGamePause(bool value)
    {
        _pause = value;
    }
}