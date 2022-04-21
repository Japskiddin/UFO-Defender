using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UfoBullet : MonoBehaviour
{
    [Header("Properties")]
    [SerializeField] private float minAngle = 130;
    [SerializeField] private float maxAngle = 230;
    [SerializeField] private float speed = 1.0f;
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

    void Update()
    {
        if (!_pause) transform.Translate(0, speed * Time.deltaTime, 0);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Homes"))
        {
            Home home = collision.GetComponent<Home>();
            if (home != null && !home.IsDead)
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