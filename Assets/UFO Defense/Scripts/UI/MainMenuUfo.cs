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
    private float _spriteWidth;
    private float _spriteHeight;
    private float _minPosY;
    private float _maxPosY;
    private Vector3 _screenBounds;

    [Header("Properties")]
    [SerializeField] private float speed = 1.5f;

    private void Awake()
    {
        _directionHorizontal = Random.Range(0, 1) == 1 ? _directionLeft : _directionRight;
        _directionVertical = Random.Range(0, 1) == 1 ? _directionTop : _directionBottom;
    }

    private void Start()
    {
        _screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
        _spriteWidth = transform.GetComponent<SpriteRenderer>().bounds.size.x / 2;
        _spriteHeight = transform.GetComponent<SpriteRenderer>().bounds.size.y / 2;
        _minPosY = _screenBounds.y * 0.1f + _spriteHeight;
        _maxPosY = _screenBounds.y * 0.9f - _spriteHeight;
        var minPosX = _screenBounds.x * 0.1f + _spriteWidth;
        var maxPosX = _screenBounds.x * 0.9f - _spriteWidth;
        transform.position = new Vector3(Random.Range(minPosX, maxPosX), Random.Range(_minPosY, _maxPosY), transform.position.z);
    }

    void Update()
    {
        Debug.Log(_screenBounds);
        CheckScreenEdges();
        Move();
    }

    private void CheckScreenEdges()
    {
        if (transform.position.y >= _maxPosY)
        {
            _directionVertical = _directionBottom;
        }
        else if (transform.position.y <= _minPosY)
        {
            _directionVertical = _directionTop;
        }

        if (transform.position.x >= _screenBounds.x - _spriteWidth)
        {
            _directionHorizontal = _directionRight;
        }
        else if (transform.position.x <= _spriteWidth)
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