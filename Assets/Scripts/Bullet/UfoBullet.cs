using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UfoBullet : BaseBullet
{
    [Header("Properties")]
    [SerializeField] private float minAngle = 130;
    [SerializeField] private float maxAngle = 230;
    [SerializeField] private float speed = 1.0f;

    private void Awake()
    {
        Init(speed);
    }

    private void Start()
    {
        float angle = Random.Range(minAngle, maxAngle);
        transform.Rotate(0, 0, angle);
        if (Debug.isDebugBuild)
        {
            Debug.Log($"UFO Bullet angle - {angle}");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Homes"))
        {
            Home home = collision.GetComponent<Home>();
            if (home != null && !home.IsDestroyed)
            {
                home.TakeDamage();
                Destroy(this.gameObject);
            }
        }
    }

    public void Destroy()
    {
        Messenger<Vector3>.Broadcast(GameEvent.CREATE_EXPLOSION, transform.position);
        Destroy(this.gameObject);
    }
}