using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
    public float speed = 3.0f;
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
        if (!_pause) transform.Translate(0, speed * Time.deltaTime, 0);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
        {
            UfoMob mob = collision.GetComponent<UfoMob>();
            if (mob != null)
            {
                mob.TakeDamage();
                Destroy(this.gameObject);
            }
        } else if (collision.tag == "EnemyBullet")
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