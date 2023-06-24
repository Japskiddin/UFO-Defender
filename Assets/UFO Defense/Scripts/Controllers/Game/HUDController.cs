using System;
using TMPro;
using UFO_Defense.Scripts.Managers;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.Components;
using UnityEngine.Localization.SmartFormat.PersistentVariables;
using UFO_Defense.Scripts.UI;

namespace UFO_Defense.Scripts.Controllers.Game
{
    public class HUDController : MonoBehaviour
    {
        [Header("Properties")]
        [SerializeField] private ProgressBar homeProgress;
        [SerializeField] private ProgressBar ufoProgress;

        [Header("Strings")]
        [SerializeField] private LocalizeStringEvent pauseLevelString;
        private LocalizedString _localizedPauseLevelString;
        private IntVariable _pauseLevel;

        private void Start()
        {
            _localizedPauseLevelString = pauseLevelString.StringReference;

            if (!_localizedPauseLevelString.TryGetValue("level_num", out var variable))
            {
                _pauseLevel = new IntVariable();
                _localizedPauseLevelString.Add("level_num", _pauseLevel);
            }
            else
            {
                _pauseLevel = variable as IntVariable;
            }

            _pauseLevel.Value = Manager.Scene.CurrentLevel;
        }

        public void UpdateMobTotal(int value)
        {
            ufoProgress.SetMaxValue(value);
        }

        public void UpdateMobCount(int value)
        {
            ufoProgress.SetValue(value);
        }

        public void UpdateHomeTotal(int value)
        {
            homeProgress.SetMaxValue(value);
        }

        public void UpdateHomeCount(int value)
        {
            homeProgress.SetValue(value);
        }
    }
}