using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManager : MonoBehaviour, IGameManager
{
    public ManagerStatus Status { get; private set; }
    public int CurrentLevel { get; private set; }
    private const int _maxLevel = 10;

    public void Startup()
    {
        if (Debug.isDebugBuild)
        {
            Debug.Log("Mission manager starting...");
        }
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
        if (Debug.isDebugBuild)
        {
            Debug.Log("Loading " + name);
        }
        UnityEngine.SceneManagement.SceneManager.LoadScene(name);
        UpdateData(0);
    }

    public void GoToNext()
    {
        // ���������, ��������� �� ��������� �������.
        if (CurrentLevel < _maxLevel)
        {
            Managers.Audio.PlayLevelMusic();
            CurrentLevel++;
            LoadGameLevel();
        }
        else
        {
            if (Debug.isDebugBuild)
            {
                Debug.Log("Last level");
            }
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
        if (Debug.isDebugBuild)
        {
            Debug.Log("Loading " + name);
        }
        UnityEngine.SceneManagement.SceneManager.LoadScene(name);
        Messenger<int>.Broadcast(GameEvent.LEVEL_UPDATED, CurrentLevel);
    }
}