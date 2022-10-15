using UFO_Defense.Scripts.Managers;
using UFO_Defense.Scripts.UI;
using UnityEngine;
using UnityEngine.UI;

namespace UFO_Defense.Scripts.Controllers
{
    public class SettingsController : MonoBehaviour
    {
        [Header("Properties")] [SerializeField]
        private AudioClip sound;

        [SerializeField] private Slider soundsSlider;
        [SerializeField] private Slider musicSlider;
        [SerializeField] private MenuCheckbox musicCheckbox;
        [SerializeField] private MenuCheckbox soundCheckbox;

        private void Awake()
        {
            Refresh();
        }

        private void Start()
        {
            UpdateCheckbox(musicCheckbox, Manager.Audio.MusicMute);
            UpdateCheckbox(soundCheckbox, AudioManager.SoundMute);
        }

        private void Refresh()
        {
            soundsSlider.value = AudioManager.SoundVolume;
            musicSlider.value = Manager.Audio.MusicVolume;
        }

        private static void UpdateCheckbox(MenuCheckbox checkbox, bool check)
        {
            checkbox.SetChecked(check);
        }

        public void OnSoundToggle()
        {
            AudioManager.SoundMute = !AudioManager.SoundMute;
            Manager.Audio.PlaySound(sound);
            UpdateCheckbox(soundCheckbox, AudioManager.SoundMute);
        }

        public void OnSoundValue(float volume)
        {
            AudioManager.SoundVolume = volume;
        }

        public void OnMusicToggle()
        {
            Manager.Audio.MusicMute = !Manager.Audio.MusicMute;
            Manager.Audio.PlaySound(sound);
            UpdateCheckbox(musicCheckbox, Manager.Audio.MusicMute);
        }

        public void OnMusicValue(float volume)
        {
            Manager.Audio.MusicVolume = volume;
        }
    }
}