using UFO_Defense.Scripts.Controllers.Game;
using UnityEngine;
using UnityEngine.UI;

namespace UFO_Defense.Scripts.UI
{
    public class SettingsModal : BasePanel
    {
        [Header("Properties")] [SerializeField]
        private AudioClip sound;

        [Header("Panel")] [SerializeField] private Image settingsMenu;

        private void Awake()
        {
            Init(settingsMenu, sound);
        }

        public void OnSettingsCloseClick()
        {
            PlaySound();
            Controller.LevelPause.Show();
            Hide();
        }
    }
}