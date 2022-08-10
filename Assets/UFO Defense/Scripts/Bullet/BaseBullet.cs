using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseBullet : MonoBehaviour
{
    private float _speed;

    public virtual void Init(float speed)
    {
        _speed = speed;
    }

    void Update()
    {
        if (Controllers.Gameplay.GameStatus == GameStatus.Running)
        {
            transform.Translate(0, _speed * Time.deltaTime, 0);
        }
    }

    private void OnBecameInvisible()
    {
        Destroy(this.gameObject);
    }
}