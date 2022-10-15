using UnityEngine;

namespace UFO_Defense.Scripts.Controllers.Game
{
    public class GameplayController : MonoBehaviour
    {
        public GameStatus Status { get; private set; }

        private void Awake()
        {
            Status = GameStatus.Running;
            Time.timeScale = 1;
        }

        private void Update()
        {
            if (Status is GameStatus.Completed or GameStatus.GameOver) return;
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (!Controller.Settings.Visible()
                    && !Controller.LevelComplete.Visible()
                    && !Controller.GameOver.Visible())
                {
                    if (Status == GameStatus.Paused)
                    {
                        OnGameResume();
                    }
                    else
                    {
                        OnGamePause();
                    }
                }
            }

            if (!Debug.isDebugBuild) return;
            if (Input.GetKeyDown(KeyCode.N))
            {
                LevelComplete();
            }

            if (Input.GetKeyDown(KeyCode.G))
            {
                GameOver();
            }
        }

        public void OnGameResume()
        {
            Status = GameStatus.Running;
            Time.timeScale = 1;
            Controller.LevelPause.PlaySound();
            Controller.LevelPause.Hide();
        }

        private void OnGamePause()
        {
            Status = GameStatus.Paused;
            Time.timeScale = 0;
            Controller.LevelPause.PlaySound();
            Controller.LevelPause.Show();
        }

        public void GameOver()
        {
            Status = GameStatus.GameOver;
            Time.timeScale = 0;
            Controller.GameOver.Show();
        }

        public void LevelComplete()
        {
            Status = GameStatus.Completed;
            Time.timeScale = 0;
            Controller.LevelComplete.Show();
        }
    }
}