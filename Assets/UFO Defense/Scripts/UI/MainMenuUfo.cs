using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuUfo : MonoBehaviour
{
    private const int _directionLeft = 0;
    private const int _directionRight = 1;
    private const int _directionTop = 2;
    private const int _directionBottom = 3;

    private int _directionHorizontal;
    private int _directionVertical;
    private float _minPosY;
    private float _maxPosY;
    private float _minPosX;
    private float _maxPosX;

    [Header("Properties")]
    [SerializeField] private float speed = 1.5f;

    private void Awake()
    {
        _directionHorizontal = Random.Range(0, 1) == 1 ? _directionLeft : _directionRight;
        _directionVertical = Random.Range(0, 1) == 1 ? _directionTop : _directionBottom;
    }

    private void Start()
    {
        CheckScreenEdges();
        transform.position = new Vector3(Random.Range(_minPosX, _maxPosX), Random.Range(_minPosY, _maxPosY), transform.position.z);
    }

    void Update()
    {
        CheckScreenEdges();
        CheckEdgesCollision();
        Move();
    }

    private void CheckScreenEdges()
    {
        var cameraPos = new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z);
        var screenBounds = Camera.main.ScreenToWorldPoint(cameraPos);
        var spriteRenderer = transform.GetComponent<SpriteRenderer>();
        var spriteWidth = spriteRenderer.bounds.size.x;
        var spriteHeight = spriteRenderer.bounds.size.y;
        _minPosY = -screenBounds.y + spriteHeight / 2f;
        _maxPosY = screenBounds.y - spriteHeight / 2f;
        _minPosX = -screenBounds.x + spriteWidth / 2f;
        _maxPosX = screenBounds.x - spriteWidth / 2f;
    }

    private void CheckEdgesCollision()
    {
        if (transform.position.y >= _maxPosY)
        {
            _directionVertical = _directionBottom;
        }
        else if (transform.position.y <= _minPosY)
        {
            _directionVertical = _directionTop;
        }

        if (transform.position.x >= _maxPosX)
        {
            _directionHorizontal = _directionRight;
        }
        else if (transform.position.x <= _minPosX)
        {
            _directionHorizontal = _directionLeft;
        }
    }

    public void Move()
    {
        float posX = (_directionHorizontal == _directionRight ? -1 : 1) * speed * Time.deltaTime;
        float posY = (_directionVertical == _directionBottom ? -1 : 1) * speed * Time.deltaTime;
        transform.Translate(posX, posY, 0);
    }
}