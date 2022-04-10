using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour, IGameManager
{
    [SerializeField] private AudioSource music1Source;
    [SerializeField] private AudioSource music2Source;
    // Ячейка переменной на панели для ссылки на новый источник звука.
    [SerializeField] private AudioSource soundSource;
    // Строки указывают имена музыкальных клипов.
    [SerializeField] private string mainMenuBGMusic;
    [SerializeField] private string levelBGMusic;
    [SerializeField] private string levelMenuBGMusic;
    private AudioSource _activeMusic;
    // Следим за тем, какой из источников активен, а какой нет.
    private AudioSource _inactiveMusic;

    public float crossFadeRate = 1.5f;
    // Переключатель, позволяющий избежать ошибок в процессе перехода.
    private bool _crossFading;

    public ManagerStatus Status { get; private set; }
    // Непосредственный доступ к закрытой переменной невозможен, только через функцию задания свойства.
    private float _musicVolume;
    public float MusicVolume
    {
        get
        {
            return _musicVolume;
        }
        set
        {
            _musicVolume = value;

            if (music1Source != null && !_crossFading)
            {
                // Напрямую регулируем громкость источника звука.
                music1Source.volume = _musicVolume;
                // Регулировка громкости обоих источников звука.
                music2Source.volume = _musicVolume;
            }
        }
    }

    public bool MusicMute
    {
        get
        {
            if (music1Source != null)
            {
                return music1Source.mute;
            }
            // Значение по умолчанию если AudioSource отсутствует.
            return false;
        }
        set
        {
            if (music1Source != null)
            {
                music1Source.mute = value;
                music2Source.mute = value;
            }
        }
    }

    public float SoundVolume
    {
        // Свойство для громкости с функцией чтения и функцией доступа.
        // Реализуем функции чтения/доступа с помощью AudioListener.
        get { return AudioListener.volume; }
        set { AudioListener.volume = value; }
    }

    public bool SoundMute
    { 
        // Добавляем аналогичное свойство для выключения звука.
        get { return AudioListener.pause; }
        set { AudioListener.pause = value; }
    }

    public void Startup()
    {
        Debug.Log("Audio manager starting...");

        // Свойства заставляют AudioSource игнорировать громкость компонента AudioListener.
        music1Source.ignoreListenerVolume = true;
        music1Source.ignoreListenerPause = true;
        music2Source.ignoreListenerVolume = true;
        music2Source.ignoreListenerPause = true;

        // 1 - полная громкость.
        MusicVolume = 1f;
        SoundVolume = 1f;

        // Инициализируем один из источников как активный.
        _activeMusic = music1Source;
        _inactiveMusic = music2Source;

        Status = ManagerStatus.Started;
    }

    public void PlaySound(AudioClip clip)
    { 
        // Воспроизводим звуки, не имеющие другого источника.
        soundSource.PlayOneShot(clip);
    }

    public void PlayMainMenuMusic()
    { 
        // Загрузка музыки intro из папки Resources.
        PlayMusic(Resources.Load("Music/" + mainMenuBGMusic) as AudioClip);
    }

    public void PlayLevelMenuMusic()
    {
        PlayMusic(Resources.Load("Music/" + levelMenuBGMusic) as AudioClip);
    }

    public void PlayLevelMusic()
    { 
        // Загрузка основной музыки из папки Resources.
        PlayMusic(Resources.Load("Music/" + levelBGMusic) as AudioClip);
    }

    private void PlayMusic(AudioClip clip)
    {
        if (_crossFading) return;
        // При изменении музыкальной композиции вызываем сопрограмму.
        StartCoroutine(CrossFadeMusic(clip));
    }

    private IEnumerator CrossFadeMusic(AudioClip clip)
    {
        _crossFading = true;

        _inactiveMusic.clip = clip;
        _inactiveMusic.volume = 0;
        _inactiveMusic.Play();

        float scaledRate = crossFadeRate * _musicVolume;
        while (_activeMusic.volume > 0)
        {
            _activeMusic.volume -= scaledRate * Time.deltaTime;
            _inactiveMusic.volume += scaledRate * Time.deltaTime;

            // Оператор yield останавливает операции на один кадр.
            yield return null;
        }

        // Временная переменная, используемая когда мы меняем местами _active и _inactive.
        AudioSource temp = _activeMusic;

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