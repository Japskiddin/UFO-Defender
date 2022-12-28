using UFO_Defense.Scripts.Controllers.Game;
using UFO_Defense.Scripts.Managers;
using UnityEngine;
using UnityEngine.Localization.Components;
using UnityEngine.Localization.SmartFormat.PersistentVariables;
using UnityEngine.UI;

namespace UFO_Defense.Scripts.UI
{
    public class LevelPauseModal : BasePanel
    {
        [Header("Properties")]
        [SerializeField] private AudioClip sound;

        [Header("Panel")]
        [SerializeField] private Image levelPause;

        private void Awake()
        {
            Init(levelPause, sound);
        }

        public void OnExitClick()
        {
            PlaySound();
            Manager.Scene.OpenMainMenu();
        }

        public void OnSettingsClick()
        {
            PlaySound();
            Hide();
            Controller.Settings.Show();
        }
    }
}