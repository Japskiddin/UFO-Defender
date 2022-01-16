using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UfoBullet : MonoBehaviour
{
    public float minAngle = 130;
    public float maxAngle = 230;
    public float speed = 1.0f;

    private void Start()
    {
        float angle = Random.Range(minAngle, maxAngle);
        transform.Rotate(0, 0, angle);
        Debug.Log("UFO Bullet angle = " + angle);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(0, speed * Time.deltaTime, 0);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Home home = collision.GetComponent<Home>();
        if (home != null)
        {
            home.TakeDamage();
            Messenger<Vector3>.Broadcast(GameEvent.CREATE_EXPLOSION, collision.transform.position);
        }
        Destroy(this.gameObject);
    }

    private void OnBecameInvisible()
    {
        Destroy(this.gameObject);
    }
}