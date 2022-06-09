using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UfoBullet : MonoBehaviour
{
    [Header("Properties")]
    [SerializeField] private float minAngle = 130;
    [SerializeField] private float maxAngle = 230;
    [SerializeField] private float speed = 1.0f;

    private void Start()
    {
        float angle = Random.Range(minAngle, maxAngle);
        transform.Rotate(0, 0, angle);
        Debug.Log($"UFO Bullet angle - {angle}");
    }

    void Update()
    {
        if (Controllers.Game.GameStatus == GameStatus.Running)
        {
            transform.Translate(0, speed * Time.deltaTime, 0);
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

    private void OnBecameInvisible()
    {
        Destroy(this.gameObject);
    }

    public void Destroy()
    {
        Messenger<Vector3>.Broadcast(GameEvent.CREATE_EXPLOSION, transform.position);
        Destroy(this.gameObject);
    }
}