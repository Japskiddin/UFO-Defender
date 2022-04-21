using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class Home : MonoBehaviour
{
    [Header("Properties")]
    [SerializeField] private Sprite[] sprites;
    private const int _defaultHp = 4;
    private SpriteRenderer sprite;
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
        sprite = GetComponent<SpriteRenderer>();
        _health = _defaultHp;
        UpdateData();
    }

    private void UpdateData()
    {
        sprite.sprite = sprites[_health];
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