using System;
using TMPro;
using UFO_Defense.Scripts.Managers;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.Components;
using UnityEngine.Localization.SmartFormat.PersistentVariables;

namespace UFO_Defense.Scripts.Controllers.Game
{
    public class HUDController : MonoBehaviour
    {
        [Header("Properties")] [SerializeField]
        private TextMeshProUGUI mobTotal;

        [Header("Strings")] [SerializeField] private LocalizeStringEvent pauseLevelString;
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
            mobTotal.text = value.ToString();
        }
    }
}