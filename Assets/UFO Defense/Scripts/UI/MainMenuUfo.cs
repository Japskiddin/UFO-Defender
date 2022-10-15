using UnityEngine;

namespace UFO_Defense.Scripts.UI
{
    public class MainMenuUfo : MonoBehaviour
    {
        private const int DirectionLeft = 0;
        private const int DirectionRight = 1;
        private const int DirectionTop = 2;
        private const int DirectionBottom = 3;

        private int _directionHorizontal;
        private int _directionVertical;
        private float _minPosY;
        private float _maxPosY;
        private float _minPosX;
        private float _maxPosX;
        private SpriteRenderer _spriteRenderer;

        [Header("Properties")] [SerializeField]
        private float speed = 1.5f;

        private void Awake()
        {
            _spriteRenderer = transform.GetComponent<SpriteRenderer>();
            _directionHorizontal = Random.Range(0, 1) == 1 ? DirectionLeft : DirectionRight;
            _directionVertical = Random.Range(0, 1) == 1 ? DirectionTop : DirectionBottom;
        }

        private void Start()
        {
            CheckScreenEdges();
            transform.position = new Vector3(Random.Range(_minPosX, _maxPosX), Random.Range(_minPosY, _maxPosY),
                transform.position.z);
        }

        private void Update()
        {
            CheckScreenEdges();
            CheckEdgesCollision();
            Move();
        }

        private void CheckScreenEdges()
        {
            var mainCamera = Camera.main;
            if (mainCamera == null)
            {
                throw new System.NullReferenceException("Camera is null!");
            }

            var cameraPos = new Vector3(Screen.width, Screen.height, mainCamera.transform.position.z);
            var screenBounds = mainCamera.ScreenToWorldPoint(cameraPos);
            var bounds = _spriteRenderer.bounds;
            var spriteWidth = bounds.size.x;
            var spriteHeight = bounds.size.y;
            _minPosY = -screenBounds.y + spriteHeight / 2f;
            _maxPosY = screenBounds.y - spriteHeight / 2f;
            _minPosX = -screenBounds.x + spriteWidth / 2f;
            _maxPosX = screenBounds.x - spriteWidth / 2f;
        }

        private void CheckEdgesCollision()
        {
            if (transform.position.y >= _maxPosY)
            {
                _directionVertical = DirectionBottom;
            }
            else if (transform.position.y <= _minPosY)
            {
                _directionVertical = DirectionTop;
            }

            if (transform.position.x >= _maxPosX)
            {
                _directionHorizontal = DirectionRight;
            }
            else if (transform.position.x <= _minPosX)
            {
                _directionHorizontal = DirectionLeft;
            }
        }

        private void Move()
        {
            var posX = (_directionHorizontal == DirectionRight ? -1 : 1) * speed * Time.deltaTime;
            var posY = (_directionVertical == DirectionBottom ? -1 : 1) * speed * Time.deltaTime;
            transform.Translate(posX, posY, 0);
        }
    }
}