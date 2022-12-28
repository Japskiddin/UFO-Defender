using System.Collections;
using UFO_Defense.Scripts.Managers;
using UnityEngine;

namespace UFO_Defense.Scripts.Level
{
    public class Explosion : MonoBehaviour
    {
        [Header("Properties")]
        [SerializeField] private AudioClip explosionSound;

        [SerializeField] private float explodeTime = 0.5f;

        private void Start()
        {
            Manager.Audio.PlaySound(explosionSound);
            StartCoroutine(Explode());
        }

        private IEnumerator Explode()
        {
            yield return new WaitForSeconds(explodeTime);
            Destroy(this.gameObject);
        }
    }
}