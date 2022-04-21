using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    [Header("Properties")]
    [SerializeField] private AudioClip sound;
    [SerializeField] private Text mobTotal;
    [Header("Panels")]
    [SerializeField] private Image gameOver;
    [SerializeField] private Image levelComplete;
    [SerializeField] private Image levelPause;
    [SerializeField] private Image settingsMenu;

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
        if (settingsMenu.gameObject.activeSelf
            || levelComplete.gameObject.activeSelf
            || gameOver.gameObject.activeSelf)
        {
            return;
        }

        if (levelPause.gameObject.activeSelf)
        {
            OnGameResume();
        }
        else
        {
            Managers.Audio.PlaySound(sound);
            levelPause.gameObject.SetActive(true);
            Messenger<bool>.Broadcast(GameEvent.GAME_PAUSE, true);
        }
    }

    public void OnGameResume()
    {
        Managers.Audio.PlaySound(sound);
        levelPause.gameObject.SetActive(false);
        Messenger<bool>.Broadcast(GameEvent.GAME_PAUSE, false);
    }

    public void OnSettingsClick()
    {
        Managers.Audio.PlaySound(sound);
        levelPause.gameObject.SetActive(false);
        settingsMenu.gameObject.SetActive(true);
    }

    public void OnSettingsClose()
    {
        Managers.Audio.PlaySound(sound);
        levelPause.gameObject.SetActive(true);
        settingsMenu.gameObject.SetActive(false);
    }

    public void OnExitClick()
    {
        Managers.Audio.PlaySound(sound);
        Messenger<bool>.Broadcast(GameEvent.GAME_PAUSE, false);
        Managers.Mission.OpenMainMenu();
    }

    public void OnNextLevel()
    {
        Managers.Mission.GoToNext();
    }
}