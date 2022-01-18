using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEnemy
{
    public Direction EnemyDirection { get; }
    public int Health { get; }

    void TakeDamage();

    void Die();

    void Shoot();

    void Move();
}