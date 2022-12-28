using UFO_Defense.Scripts.Level;
using UFO_Defense.Scripts.Managers;
using UnityEngine;

namespace UFO_Defense.Scripts.Bullet
{
    public class UfoBullet : BaseBullet
    {
        [Header("Properties")]
        [SerializeField] private float minAngle = 130;

        [SerializeField] private float maxAngle = 230;
        [SerializeField] private float speed = 1.0f;

        private void Awake()
        {
            Init(speed);
        }

        private void Start()
        {
            var angle = Random.Range(minAngle, maxAngle);
            transform.Rotate(0, 0, angle);
            if (Debug.isDebugBuild)
            {
                Debug.Log($"UFO Bullet angle - {angle}");
            }
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (!collision.CompareTag("Homes")) return;
            var home = collision.GetComponent<Home>();
            if (home == null || home.IsDestroyed) return;
            home.TakeDamage();
            Destroy();
        }

        public void Destroy()
        {
            Messenger<Vector3>.Broadcast(GameEvent.CreateExplosion, transform.position);
            Destroy(this.gameObject);
        }
    }
}