using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using UFO_Defense.Scripts.UI;

namespace UFO_Defense.Scripts.Managers
{
    public class ScenesManager : MonoBehaviour, IGameManager
    {
        private const int MaxLevel = 10;
        public ManagerStatus Status { get; private set; }
        public int CurrentLevel { get; private set; }

        public void Startup()
        {
            if (Debug.isDebugBuild)
            {
                Debug.Log("Scenes manager starting...");
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
                Debug.Log($"Loading {sceneName}");
            }

            SceneManager.LoadScene(sceneName);
            UpdateData(0);
        }

        public void GoToNext()
        {
            if (CurrentLevel < MaxLevel)
            {
                Manager.Audio.PlayLevelMusic();
                CurrentLevel++;
                StartCoroutine(LoadGameLevelAsync());
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
            StartCoroutine(LoadGameLevelAsync());
        }

        IEnumerator LoadGameLevelAsync()
        {
            // The Application loads the Scene in the background as the current Scene runs.
            // This is particularly good for creating loading screens.
            // You could also load the Scene by using sceneBuildIndex. In this case Scene2 has
            // a sceneBuildIndex of 1 as shown in Build Settings.

            yield return null;

            const string sceneName = "GameLevel";
            if (Debug.isDebugBuild)
            {
                Debug.Log($"Loading {sceneName}");
            }

            var asyncLoad = SceneManager.LoadSceneAsync(sceneName);
            //Don't let the Scene activate until you allow it to
            asyncLoad.allowSceneActivation = false;

            // Wait until the asynchronous scene fully loads
            while (!asyncLoad.isDone)
            {
                var progress = (asyncLoad.progress * 100) + "%";
                if (Debug.isDebugBuild)
                {
                    Debug.Log($"Scene loading progress: {progress}");
                }
                // Check if the load has finished
                if (asyncLoad.progress >= 0.9f)
                {
                    asyncLoad.allowSceneActivation = true;
                }
                yield return null;
            }
        }
    }
}