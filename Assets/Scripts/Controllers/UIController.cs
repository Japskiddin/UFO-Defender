using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    [SerializeField] private AudioClip sound;
    [SerializeField] private Text mobTotal;
    [SerializeField] private Image gameOver;
    [SerializeField] private Image levelComplete;
    [SerializeField] private Image levelPause;
    [SerializeField] private Image settingsMenu;

    private float _prevMusicVolume = 100f;

    private void Awake()
    {
        gameOver.gameObject.SetActive(false);
        levelComplete.gameObject.SetActive(false);
        levelPause.gameObject.SetActive(false);
        settingsMenu.gameObject.SetActive(false);
    }

    public void UpdateMobTotal(int value)
    {
        mobTotal.text = value.ToString();
    }

    public void OnGameOver()
    {
        gameOver.gameObject.SetActive(true);
        Messenger<bool>.Broadcast(GameEvent.GAME_PAUSE, true);
    }

    public void OnLevelComplete()
    {
        levelComplete.gameObject.SetActive(true);
        Messenger<bool>.Broadcast(GameEvent.GAME_PAUSE, true);
    }

    public void OnGamePause()
    {
        if (levelPause.gameObject.activeSelf)
        {
            OnGameResume();
        } else
        {
            _prevMusicVolume = Managers.Audio.musicVolume;
            Managers.Audio.musicVolume = 0f;
            Managers.Audio.PlaySound(sound);
            levelPause.gameObject.SetActive(true);
            Messenger<bool>.Broadcast(GameEvent.GAME_PAUSE, true);
        }
    }

    public void OnGameResume()
    {
        Managers.Audio.musicVolume = _prevMusicVolume;
        Managers.Audio.PlaySound(sound);
        levelPause.gameObject.SetActive(false);
        Messenger<bool>.Broadcast(GameEvent.GAME_PAUSE, false);
    }

    public void OnExitClick()
    {
        Managers.Audio.PlaySound(sound);
        Messenger<bool>.Broadcast(GameEvent.GAME_PAUSE, false);
        Managers.Mission.OpenMainMenu();
    }

    public void OnNextLevel()
    {
        // add code for selecting next level
    }
}