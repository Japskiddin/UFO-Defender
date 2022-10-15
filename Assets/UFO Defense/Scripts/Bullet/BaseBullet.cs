using UFO_Defense.Scripts.Controllers.Game;
using UnityEngine;

namespace UFO_Defense.Scripts.Bullet
{
    public class BaseBullet : MonoBehaviour
    {
        private float _speed;

        protected void Init(float speed)
        {
            _speed = speed;
        }

        private void Update()
        {
            if (Controller.Gameplay.Status == GameStatus.Running)
            {
                transform.Translate(0, _speed * Time.deltaTime, 0);
            }
        }

        private void OnBecameInvisible()
        {
            Destroy(this.gameObject);
        }
    }
}