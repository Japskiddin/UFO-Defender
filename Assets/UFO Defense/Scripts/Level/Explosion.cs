using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    [Header("Properties")]
    [SerializeField] private AudioClip explosionSound;
    [SerializeField] private float explodeTime = 0.5f;

    void Start()
    {
        Managers.Audio.PlaySound(explosionSound);
        StartCoroutine(Explode());
    }

    private IEnumerator Explode()
    {
        yield return new WaitForSeconds(explodeTime);
        Destroy(this.gameObject);
    }
}