using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsController : MonoBehaviour
{
    [Header("Properties")]
    [SerializeField] private AudioClip sound;
    [SerializeField] private Slider soundsSlider;
    [SerializeField] private Slider musicSlider;
    [SerializeField] private MenuCheckbox musicCheckbox;
    [SerializeField] private MenuCheckbox soundCheckbox;

    private void Awake()
    {
        Refresh();
    }

    public void Refresh()
    {
        soundsSlider.value = Managers.Audio.SoundVolume;
        musicSlider.value = Managers.Audio.MusicVolume;
        UpdateCheckbox(musicCheckbox, Managers.Audio.MusicMute);
        UpdateCheckbox(soundCheckbox, Managers.Audio.SoundMute);
    }

    private void UpdateCheckbox(MenuCheckbox checkbox, bool check)
    {
        checkbox.SetChecked(check);
    }

    public void OnSoundToggle()
    {
        Managers.Audio.SoundMute = !Managers.Audio.SoundMute;
        Managers.Audio.PlaySound(sound);
        UpdateCheckbox(soundCheckbox, Managers.Audio.SoundMute);
    }

    public void OnSoundValue(float volume)
    {
        Managers.Audio.SoundVolume = volume;
    }

    public void OnMusicToggle()
    {
        Managers.Audio.MusicMute = !Managers.Audio.MusicMute;
        Managers.Audio.PlaySound(sound);
        UpdateCheckbox(musicCheckbox, Managers.Audio.MusicMute);
    }

    public void OnMusicValue(float volume)
    {
        Managers.Audio.MusicVolume = volume;
    }
}