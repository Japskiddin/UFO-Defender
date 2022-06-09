using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(BoxCollider2D))]
public class Home : MonoBehaviour
{
    private const int _defaultHp = 4;
    private SpriteRenderer _sprite;
    private BoxCollider2D _collider;
    private int _health;
    public bool IsDestroyed
    {
        get
        {
            return _health == 0;
        }
    }

    private void Awake()
    {
        _collider = GetComponent<BoxCollider2D>();
        if (_collider == null)
        {
            throw new NullReferenceException("Component BoxCollider2D is null");
        }
        _sprite = GetComponent<SpriteRenderer>();
        if (_sprite == null)
        {
            throw new NullReferenceException("Component SpriteRenderer is null");
        }
        _health = _defaultHp;
    }

    private void Start()
    {
        UpdateData();
    }

    private void UpdateData()
    {
        Debug.Log($"Current home health - {_health}");
        _sprite.sprite = Controllers.Level.HomeSprites[_health];
        Vector2 size = _sprite.bounds.size / 0.6f;
        _collider.size = size;
        _collider.offset = new Vector2(0, size.y / 2f);
    }

    public void TakeDamage()
    {
        if (IsDestroyed) return;
        Messenger<Vector3>.Broadcast(GameEvent.CREATE_EXPLOSION, transform.position);
        _health--;
        UpdateData();
        if (IsDestroyed)
        {
            Messenger.Broadcast(GameEvent.HOME_DESTROYED);
        }
    }
}