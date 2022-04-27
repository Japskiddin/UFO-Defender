using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[RequireComponent(typeof(SpriteRenderer))]
public class Home : MonoBehaviour
{
    [Header("Properties")]
    [SerializeField] private Sprite[] sprites;
    private const int _defaultHp = 4;
    private SpriteRenderer _sprite;
    private int _health;
    public bool IsDead
    {
        get
        {
            return _health == 0;
        }
    }

    private void Awake()
    {
        _sprite = GetComponent<SpriteRenderer>();
        if (_sprite == null)
        {
            throw new NullReferenceException("Component SpriteRenderer is null");
        }
        _health = _defaultHp;
        UpdateData();
    }

    private void UpdateData()
    {
        _sprite.sprite = sprites[_health];
    }

    public void TakeDamage()
    {
        if (IsDead) return;
        Messenger<Vector3>.Broadcast(GameEvent.CREATE_EXPLOSION, transform.position);
        _health--;
        UpdateData();
        if (IsDead) Messenger.Broadcast(GameEvent.HOME_DESTROYED);
    }
}