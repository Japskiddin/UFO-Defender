using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MissionManager : MonoBehaviour, IGameManager
{
    public ManagerStatus Status { get; private set; }
    public int CurrentLevel { get; private set; }
    public int MaxLevel { get; private set; }

    public void Startup()
    {
        Debug.Log("Mission manager starting...");
        UpdateData(0, 1);

        Status = ManagerStatus.Started;
    }

    public void UpdateData(int curLevel, int maxLevel)
    {
        CurrentLevel = curLevel;
        MaxLevel = maxLevel;
    }

    public void OpenMainMenu()
    {
        Managers.Audio.PlayMainMenuMusic();
        string name = "MainMenu";
        Debug.Log("Loading " + name);
        SceneManager.LoadScene(name);
        UpdateData(0, 1);
    }

    public void GoToNext()
    {
        // Проверяем, достигнут ли последний уровень.
        if (CurrentLevel < MaxLevel)
        {
            Managers.Audio.PlayLevelMusic();
            CurrentLevel++;
            string name = "Level" + CurrentLevel;
            Debug.Log("Loading " + name);
            // Команда загрузки сцены.
            SceneManager.LoadScene(name);
        } else
        {
            Debug.Log("Last level");
            Messenger.Broadcast(GameEvent.GAME_COMPLETE);
        }
    }

    public void ReachObjective()
    {
        // Здесь может быть код обработки нескольких целей.
        Messenger.Broadcast(GameEvent.LEVEL_COMPLETE);
    }

    public void RestartCurrent()
    {
        string name = "Level" + CurrentLevel;
        Debug.Log("Loading " + name);
        SceneManager.LoadScene(name);
    }
}