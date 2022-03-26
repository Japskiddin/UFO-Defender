using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class Home : MonoBehaviour
{
    [SerializeField] Sprite[] sprites;
    private const int defaultHp = 4;
    private SpriteRenderer sprite;
    public bool isDead
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
        Health = defaultHp;
        UpdateData();
    }

    private void UpdateData()
    {
        sprite.sprite = sprites[Health];
    }

    public void TakeDamage()
    {
        if (isDead) return;
        Messenger<Vector3>.Broadcast(GameEvent.CREATE_EXPLOSION, transform.position);
        Health--;
        UpdateData();
        if (isDead) Messenger.Broadcast(GameEvent.HOME_DESTROYED);
    }
}