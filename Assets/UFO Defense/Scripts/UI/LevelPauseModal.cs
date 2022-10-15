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
        [Header("Properties")] [SerializeField]
        private AudioClip sound;

        [Header("Panel")] [SerializeField] private Image levelPause;
        [Header("Strings")] [SerializeField] private LocalizeStringEvent levelString;

        private void Awake()
        {
            Init(levelPause, sound);
        }

        private void OnEnable()
        {
            Messenger<int>.AddListener(GameEvent.LevelUpdated, OnRefreshLevel);
        }

        private void OnDisable()
        {
            Messenger<int>.RemoveListener(GameEvent.LevelUpdated, OnRefreshLevel);
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

        private void OnRefreshLevel(int level)
        {
            Debug.Log("OnRefreshLevel - level = " + level);
            var levelNumString = levelString.StringReference["level_num"] as IntVariable;
            if (levelNumString == null) return;
            levelNumString.Value = level;
            levelString.RefreshString();
        }
    }
}