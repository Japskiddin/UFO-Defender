using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour, IGameManager
{
    [Header("Properties")]
    [SerializeField] private AudioSource music1Source;
    [SerializeField] private AudioSource music2Source;
    // ������ ���������� �� ������ ��� ������ �� ����� �������� �����.
    [SerializeField] private AudioSource soundSource;
    // ������ ��������� ����� ����������� ������.
    [SerializeField] private string mainMenuBGMusic;
    [SerializeField] private string levelBGMusic;
    [SerializeField] private string levelMenuBGMusic;
    [SerializeField] private float crossFadeRate = 1.5f;

    private AudioSource _activeMusic;
    // ������ �� ���, ����� �� ���������� �������, � ����� ���.
    private AudioSource _inactiveMusic;
    // �������������, ����������� �������� ������ � �������� ��������.
    private bool _crossFading;

    public ManagerStatus Status { get; private set; }
    // ���������������� ������ � �������� ���������� ����������, ������ ����� ������� ������� ��������.
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
                // �������� ���������� ��������� ��������� �����.
                music1Source.volume = _musicVolume;
                // ����������� ��������� ����� ���������� �����.
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
            // �������� �� ��������� ���� AudioSource �����������.
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
        // �������� ��� ��������� � �������� ������ � �������� �������.
        // ��������� ������� ������/������� � ������� AudioListener.
        get { return AudioListener.volume; }
        set { AudioListener.volume = value; }
    }

    public bool SoundMute
    {
        // ��������� ����������� �������� ��� ���������� �����.
        get { return AudioListener.pause; }
        set { AudioListener.pause = value; }
    }

    public void Startup()
    {
        if (Debug.isDebugBuild)
        {
            Debug.Log("Audio manager starting...");
        }

        // �������� ���������� AudioSource ������������ ��������� ���������� AudioListener.
        music1Source.ignoreListenerVolume = true;
        music1Source.ignoreListenerPause = true;
        music2Source.ignoreListenerVolume = true;
        music2Source.ignoreListenerPause = true;

        // 1 - ������ ���������.
        MusicVolume = 1f;
        SoundVolume = 1f;

        // �������������� ���� �� ���������� ��� ��������.
        _activeMusic = music1Source;
        _inactiveMusic = music2Source;

        Status = ManagerStatus.Started;
    }

    public void PlaySound(AudioClip clip)
    {
        // ������������� �����, �� ������� ������� ���������.
        soundSource.PlayOneShot(clip);
    }

    public void PlayMainMenuMusic()
    {
        // �������� ������ intro �� ����� Resources.
        PlayMusic(Resources.Load("Music/" + mainMenuBGMusic) as AudioClip);
    }

    public void PlayLevelMenuMusic()
    {
        PlayMusic(Resources.Load("Music/" + levelMenuBGMusic) as AudioClip);
    }

    public void PlayLevelMusic()
    {
        // �������� �������� ������ �� ����� Resources.
        PlayMusic(Resources.Load("Music/" + levelBGMusic) as AudioClip);
    }

    private void PlayMusic(AudioClip clip)
    {
        if (_crossFading) return;
        // ��� ��������� ����������� ���������� �������� �����������.
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

            // �������� yield ������������� �������� �� ���� ����.
            yield return null;
        }

        // ��������� ����������, ������������ ����� �� ������ ������� _active � _inactive.
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