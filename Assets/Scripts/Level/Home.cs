using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class Home : MonoBehaviour
{
    [SerializeField] private Sprite[] sprites;
    private const int _defaultHp = 4;
    private SpriteRenderer sprite;
    public bool IsDead
    {
        get
        {
            return Health == 0;
        }
    }
    public int Health { get; set; }

    private void Awake()
    {
        sprite = GetComponent<SpriteRenderer>();
        Health = _defaultHp;
        UpdateData();
    }

    private void UpdateData()
    {
        sprite.sprite = sprites[Health];
    }

    public void TakeDamage()
    {
        if (IsDead) return;
        Messenger<Vector3>.Broadcast(GameEvent.CREATE_EXPLOSION, transform.position);
        Health--;
        UpdateData();
        if (IsDead) Messenger.Broadcast(GameEvent.HOME_DESTROYED);
    }
}