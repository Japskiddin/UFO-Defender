using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;

namespace UFO_Defense.Scripts.Managers
{
    public class AudioManager : MonoBehaviour, IGameManager
    {
        [Header("Properties")]
        [SerializeField] private AudioSource music1Source;

        [SerializeField] private AudioSource music2Source;
        [SerializeField] private AudioSource soundSource;

        [SerializeField] private string mainMenuBgMusic;
        [SerializeField] private string levelBgMusic;
        [SerializeField] private string levelMenuBgMusic;

        [SerializeField] private float crossFadeRate = 1.5f;

        private AudioSource _activeMusic;
        private AudioSource _inactiveMusic;
        private bool _crossFading;

        public ManagerStatus Status { get; private set; }
        private float _musicVolume;

        public float MusicVolume
        {
            get => _musicVolume;
            set
            {
                _musicVolume = value;
                if (music1Source == null || _crossFading) return;
                music1Source.volume = _musicVolume;
                music2Source.volume = _musicVolume;
            }
        }

        public bool MusicMute
        {
            get => music1Source != null && music1Source.mute;
            set
            {
                if (music1Source == null) return;
                music1Source.mute = value;
                music2Source.mute = value;
            }
        }

        public static float SoundVolume
        {
            get => AudioListener.volume;
            set => AudioListener.volume = value;
        }

        public static bool SoundMute
        {
            get => AudioListener.pause;
            set => AudioListener.pause = value;
        }

        public void Startup()
        {
            if (Debug.isDebugBuild)
            {
                Debug.Log("Audio manager starting...");
            }

            music1Source.ignoreListenerVolume = true;
            music1Source.ignoreListenerPause = true;
            music2Source.ignoreListenerVolume = true;
            music2Source.ignoreListenerPause = true;

            MusicVolume = 1f;
            SoundVolume = 1f;

            _activeMusic = music1Source;
            _inactiveMusic = music2Source;

            Status = ManagerStatus.Started;
        }

        public void PlaySound(AudioClip clip)
        {
            soundSource.PlayOneShot(clip);
        }

        public void PlayMainMenuMusic()
        {
            var path = string.Concat("Music/", mainMenuBgMusic);
            PlayMusic(Resources.Load(path) as AudioClip);
        }

        public void PlayLevelMenuMusic()
        {
            var path = string.Concat("Music/", levelMenuBgMusic);
            PlayMusic(Resources.Load(path) as AudioClip);
        }

        public void PlayLevelMusic()
        {
            var path = string.Concat("Music/", levelBgMusic);
            PlayMusic(Resources.Load(path) as AudioClip);
        }

        private void PlayMusic(AudioClip clip)
        {
            if (_crossFading) return;
            StartCoroutine(CrossFadeMusic(clip));
        }

        private IEnumerator CrossFadeMusic(AudioClip clip)
        {
            _crossFading = true;

            _inactiveMusic.clip = clip;
            _inactiveMusic.volume = 0;
            _inactiveMusic.Play();

            var scaledRate = crossFadeRate * _musicVolume;
            while (_activeMusic.volume > 0)
            {
                _activeMusic.volume -= scaledRate * Time.deltaTime;
                _inactiveMusic.volume += scaledRate * Time.deltaTime;
                yield return null;
            }

            var temp = _activeMusic;

            _activeMusic = _inactiveMusic;
            _activeMusic.volume = _musicVolume;

            _inactiveMusic = temp;
            _inactiveMusic.Stop();

            _crossFading = false;
        }

        public void StopMusic()
        {
            _activeMusic.Stop();
            _inactiveMusic.Stop();
        }
    }
}