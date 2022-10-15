using UFO_Defense.Scripts.Controllers.Game;
using UFO_Defense.Scripts.Managers;
using UnityEngine;

namespace UFO_Defense.Scripts.Level
{
    [RequireComponent(typeof(SpriteRenderer))]
    [RequireComponent(typeof(BoxCollider2D))]
    public class Home : MonoBehaviour
    {
        private const int DefaultHp = 4;
        private SpriteRenderer _sprite;
        private BoxCollider2D _collider;
        private int _health;

        public bool IsDestroyed => _health == 0;

        private void Awake()
        {
            _collider = GetComponent<BoxCollider2D>();
            if (_collider == null)
            {
                throw new System.NullReferenceException("Component BoxCollider2D is null");
            }

            _sprite = GetComponent<SpriteRenderer>();
            if (_sprite == null)
            {
                throw new System.NullReferenceException("Component SpriteRenderer is null");
            }

            _health = DefaultHp;
            _collider.enabled = true;
        }

        private void Start()
        {
            UpdateState();
        }

        private void UpdateState()
        {
            if (Debug.isDebugBuild)
            {
                Debug.Log($"Current home health - {_health}");
            }

            _sprite.sprite = Controller.Level.HomeSprites[_health];
            Vector2 size = _sprite.bounds.size / 0.6f;
            _collider.size = size;
            _collider.offset = new Vector2(0, size.y / 2f);
        }

        public void TakeDamage()
        {
            if (IsDestroyed) return;
            _health--;
            if (Debug.isDebugBuild)
            {
                Debug.Log($"Home take damage, health - {_health}");
            }

            UpdateState();
            if (!IsDestroyed) return;
            _collider.enabled = false;
            Messenger.Broadcast(GameEvent.HomeDestroyed);
        }
    }
}