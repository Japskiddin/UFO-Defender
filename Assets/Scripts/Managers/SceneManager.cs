using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManager : MonoBehaviour, IGameManager
{
    public ManagerStatus Status { get; private set; }
    public int CurrentLevel { get; private set; }
    private const int _maxLevel = 2;

    public void Startup()
    {
        Debug.Log("Mission manager starting...");
        UpdateData(0);

        Status = ManagerStatus.Started;
    }

    public void UpdateData(int curLevel)
    {
        CurrentLevel = curLevel;
    }

    public void OpenMainMenu()
    {
        Managers.Audio.PlayMainMenuMusic();
        string name = "MainMenu";
        Debug.Log("Loading " + name);
        UnityEngine.SceneManagement.SceneManager.LoadScene(name);
        UpdateData(0);
    }

    public void GoToNext()
    {
        // ѕровер€ем, достигнут ли последний уровень.
        if (CurrentLevel < _maxLevel)
        {
            Managers.Audio.PlayLevelMusic();
            CurrentLevel++;
            LoadGameLevel();
        } else
        {
            Debug.Log("Last level");
            Messenger.Broadcast(GameEvent.GAME_COMPLETE);
        }
    }

    public void RestartCurrent()
    {
        LoadGameLevel();
    }

    private void LoadGameLevel()
    {
        string name = "GameLevel";
        Debug.Log("Loading " + name);
        UnityEngine.SceneManagement.SceneManager.LoadScene(name);
    }
}