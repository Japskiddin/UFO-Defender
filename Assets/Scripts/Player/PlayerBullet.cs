using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
    public float speed = 2.0f;

    // Update is called once per frame
    void Update()
    {
        transform.Translate(0, speed * Time.deltaTime, 0);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
        {
            UfoMob mob = collision.GetComponent<UfoMob>();
            if (mob != null)
            {
                mob.TakeDamage();
                Messenger<Vector3>.Broadcast(GameEvent.CREATE_EXPLOSION, collision.transform.position);
                Destroy(this.gameObject);
            }
        }
    }

    private void OnBecameInvisible()
    {
        Destroy(this.gameObject);
    }
}