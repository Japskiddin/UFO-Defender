using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour
{
    [SerializeField] private AudioClip sound;
    [SerializeField] private Image mainMenu;
    [SerializeField] private Image settingsMenu;
    [SerializeField] private Slider soundsSlider;
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Text textMusic;
    [SerializeField] private Text textSounds;

    private void Awake()
    {
        Refresh();
        mainMenu.gameObject.SetActive(true);
        settingsMenu.gameObject.SetActive(false);
    }

    public void OnExitClick()
    {
        Managers.Audio.PlaySound(sound);
        Application.Quit();
    }

    public void OnStartClick()
    {
        Managers.Audio.PlaySound(sound);
        Managers.Mission.GoToNext();
    }

    public void OnSettingsClick()
    {
        Managers.Audio.PlaySound(sound);
        mainMenu.gameObject.SetActive(false);
        settingsMenu.gameObject.SetActive(true);
    }

    public void OnSettingsClose()
    {
        mainMenu.gameObject.SetActive(true);
        settingsMenu.gameObject.SetActive(false);
        Managers.Audio.PlaySound(sound);
    }

    public void Refresh()
    {
        soundsSlider.value = Managers.Audio.SoundVolume;
        musicSlider.value = Managers.Audio.MusicVolume;
        textSounds.text = "Sound " + (Managers.Audio.SoundMute ? "on" : "off");
        textMusic.text = "Music " + (Managers.Audio.MusicMute ? "on" : "off");
    }

    public void OnSoundToggle()
    {
        Managers.Audio.SoundMute = !Managers.Audio.SoundMute;
        Managers.Audio.PlaySound(sound);
        textSounds.text = "Sound " + (Managers.Audio.SoundMute ? "on" : "off");
    }

    public void OnSoundValue(float volume)
    {
        Managers.Audio.SoundVolume = volume;
    }

    public void OnMusicToggle()
    {
        Managers.Audio.MusicMute = !Managers.Audio.MusicMute;
        Managers.Audio.PlaySound(sound);
        textMusic.text = "Music " + (Managers.Audio.MusicMute ? "on" : "off");
    }

    public void OnMusicValue(float volume)
    {
        Managers.Audio.MusicVolume = volume;
    }
}