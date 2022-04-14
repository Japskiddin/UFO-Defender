using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    [SerializeField] private AudioClip explosionSound;
    public float explodeTime = 0.5f;

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