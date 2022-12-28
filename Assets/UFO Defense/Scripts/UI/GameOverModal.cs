using UFO_Defense.Scripts.Managers;
using UnityEngine;
using UnityEngine.UI;

namespace UFO_Defense.Scripts.UI
{
    public class GameOverModal : BasePanel
    {
        [Header("Properties")]
        [SerializeField] private AudioClip sound;

        [Header("Panel")]
        [SerializeField] private Image gameOver;

        private void Awake()
        {
            Init(gameOver, sound);
        }

        public void OnExitClick()
        {
            PlaySound();
            Manager.Scene.OpenMainMenu();
        }
    }
}