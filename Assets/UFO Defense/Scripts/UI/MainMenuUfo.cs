using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuUfo : MonoBehaviour
{
    private const int _directionLeft = 0;
    private const int _directionRight = 1;
    private const int _directionTop = 2;
    private const int _directionBottom = 3;

    private const float _maxPosX = 7f;
    private const float _minPosX = -7f;
    private const float _maxPosY = 4f;
    private const float _minPosY = -4f;

    private int _directionHorizontal;
    private int _directionVertical;

    [Header("Properties")]
    [SerializeField] private float speed = 1.5f;

    private void Awake()
    {
        _directionHorizontal = Random.Range(0, 1) == 1 ? _directionLeft : _directionRight;
        _directionVertical = Random.Range(0, 1) == 1 ? _directionTop : _directionBottom;
    }

    void Update()
    {
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