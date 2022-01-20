using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Home : MonoBehaviour
{
    [SerializeField] Sprite[] sprites;
    private const int defaultHp = 4;
    private int _hp;
    public bool isDead {
        get
        {
            return _hp == 0;
        }
    }

    private void Awake()
    {
        _hp = defaultHp;
        UpdateData();
    }

    private void UpdateData()
    {
        SpriteRenderer sprite = GetComponent<SpriteRenderer>();
        if (sprite != null)
        {
            sprite.sprite = sprites[_hp];
        }
    }

    public void TakeDamage()
    {
        if (isDead) return;
        _hp--;
        UpdateData();
    }
}