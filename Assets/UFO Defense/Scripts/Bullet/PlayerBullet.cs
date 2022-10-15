using UFO_Defense.Scripts.Enemy;
using UnityEngine;

namespace UFO_Defense.Scripts.Bullet
{
    public class PlayerBullet : BaseBullet
    {
        [Header("Properties")] [SerializeField]
        private float speed = 3.0f;

        private void Awake()
        {
            Init(speed);
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Enemy"))
            {
                var mob = collision.GetComponent<Ufo>();
                if (mob == null) return;
                mob.TakeDamage();
                Destroy(this.gameObject);
            }
            else if (collision.CompareTag("EnemyBullet"))
            {
                var bullet = collision.GetComponent<UfoBullet>();
                if (bullet == null) return;
                bullet.Destroy();
                Destroy(this.gameObject);
            }
        }
    }
}