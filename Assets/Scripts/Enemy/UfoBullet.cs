using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UfoBullet : MonoBehaviour
{
    public float minAngle = 130;
    public float maxAngle = 230;
    public float speed = 1.0f;
    private bool _pause = false;

    private void Awake()
    {
        _pause = false;
        Messenger<bool>.AddListener(GameEvent.GAME_PAUSE, OnGamePause);
    }

    private void OnDestroy()
    {
        Messenger<bool>.RemoveListener(GameEvent.GAME_PAUSE, OnGamePause);
    }

    private void Start()
    {
        float angle = Random.Range(minAngle, maxAngle);
        transform.Rotate(0, 0, angle);
        Debug.Log("UFO Bullet angle = " + angle);
    }

    // Update is called once per frame
    void Update()
    {
        if (!_pause) transform.Translate(0, speed * Time.deltaTime, 0);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Homes")
        {
            Home home = collision.GetComponent<Home>();
            if (home != null && !home.isDead)
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

    private void OnGamePause(bool value)
    {
        _pause = value;
    }

    public void Destroy()
    {
        Messenger<Vector3>.Broadcast(GameEvent.CREATE_EXPLOSION, transform.position);
        Destroy(this.gameObject);
    }
}