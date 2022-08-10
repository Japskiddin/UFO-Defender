using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;

public class SettingsController : MonoBehaviour
{
    [Header("Properties")]
    [SerializeField] private AudioClip sound;
    [SerializeField] private Slider soundsSlider;
    [SerializeField] private Slider musicSlider;
    [SerializeField] private TextMeshProUGUI textMusic;
    [SerializeField] private TextMeshProUGUI textSounds;

    private void Awake()
    {
        Refresh();
    }

    public void Refresh()
    {
        soundsSlider.value = Managers.Audio.SoundVolume;
        musicSlider.value = Managers.Audio.MusicVolume;
        UpdateSoundButtonTitle();
        UpdateMusicButtonTitle();
    }

    private void UpdateSoundButtonTitle()
    {
        string settingsSoundRes = Managers.Audio.SoundMute ? "Settings Sound On" : "Settings Sound Off";
        UpdateString(settingsSoundRes, textSounds);
    }

    private void UpdateMusicButtonTitle()
    {
        string settingsMusicRes = Managers.Audio.MusicMute ? "Settings Music On" : "Settings Music Off";
        UpdateString(settingsMusicRes, textMusic);
    }

    private void UpdateString(string res, TextMeshProUGUI text)
    {
        var settings = LocalizationSettings.StringDatabase.GetLocalizedStringAsync("My text", res);
        if (settings.IsDone)
        {
            text.text = settings.Result;
        }
        else
        {
            settings.Completed += (settings) => text.text = settings.Result;
        }
    }

    public void OnSoundToggle()
    {
        Managers.Audio.SoundMute = !Managers.Audio.SoundMute;
        Managers.Audio.PlaySound(sound);
        UpdateSoundButtonTitle();
    }

    public void OnSoundValue(float volume)
    {
        Managers.Audio.SoundVolume = volume;
    }

    public void OnMusicToggle()
    {
        Managers.Audio.MusicMute = !Managers.Audio.MusicMute;
        Managers.Audio.PlaySound(sound);
        UpdateMusicButtonTitle();
    }

    public void OnMusicValue(float volume)
    {
        Managers.Audio.MusicVolume = volume;
    }
}