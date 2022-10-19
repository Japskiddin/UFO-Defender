using UFO_Defense.Scripts.Controllers.Game;
using UnityEngine;

namespace UFO_Defense.Scripts.Managers
{
    public class SceneManager : MonoBehaviour, IGameManager
    {
        private const int MaxLevel = 10;
        public ManagerStatus Status { get; private set; }
        public int CurrentLevel { get; private set; }

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
            Manager.Audio.PlayMainMenuMusic();
            const string sceneName = "MainMenu";
            if (Debug.isDebugBuild)
            {
                Debug.Log("Loading " + sceneName);
            }

            UnityEngine.SceneManagement.SceneManager.LoadScene(sceneName);
            UpdateData(0);
        }

        public void GoToNext()
        {
            if (CurrentLevel < MaxLevel)
            {
                Manager.Audio.PlayLevelMusic();
                CurrentLevel++;
                LoadGameLevel();
            }
            else
            {
                if (Debug.isDebugBuild)
                {
                    Debug.Log("Last level");
                }

                Messenger.Broadcast(GameEvent.GameComplete);
            }
        }

        public void RestartCurrent()
        {
            LoadGameLevel();
        }

        private void LoadGameLevel()
        {
            const string sceneName = "GameLevel";
            if (Debug.isDebugBuild)
            {
                Debug.Log("Loading " + sceneName);
            }

            UnityEngine.SceneManagement.SceneManager.LoadScene(sceneName);
        }
    }
}