using UFO_Defense.Scripts.Managers;
using UFO_Defense.Scripts.UI;
using UnityEngine;

namespace UFO_Defense.Scripts.Controllers
{
    public class MainMenuController : MonoBehaviour
    {
        [Header("Properties")] [SerializeField]
        private AudioClip sound;

        [Header("Panels")] [SerializeField] private RectTransform mainMenu;
        [SerializeField] private RectTransform settingsMenu;
        [SerializeField] private RectTransform about;
        [SerializeField] private RectTransform homeButton;
        [SerializeField] private RectTransform exitButton;

        private void Awake()
        {
            mainMenu.gameObject.GetComponent<CanvasElementVisibility>().Visible = true;
            settingsMenu.gameObject.GetComponent<CanvasElementVisibility>().Visible = false;
            about.gameObject.GetComponent<CanvasElementVisibility>().Visible = false;
            homeButton.gameObject.SetActive(false);
            exitButton.gameObject.SetActive(true);
            Time.timeScale = 1;
        }

        public void OnExitClick()
        {
            Manager.Audio.PlaySound(sound);
            Application.Quit();
        }

        public void OnStartClick()
        {
            Manager.Audio.PlaySound(sound);
            Manager.Scene.GoToNext();
        }

        public void OnSettingsClick()
        {
            Manager.Audio.PlaySound(sound);
            mainMenu.gameObject.GetComponent<CanvasElementVisibility>().Visible = false;
            settingsMenu.gameObject.GetComponent<CanvasElementVisibility>().Visible = true;
            about.gameObject.GetComponent<CanvasElementVisibility>().Visible = false;
            homeButton.gameObject.SetActive(true);
            exitButton.gameObject.SetActive(false);
        }

        public void OnHomeClick()
        {
            Manager.Audio.PlaySound(sound);
            mainMenu.gameObject.GetComponent<CanvasElementVisibility>().Visible = true;
            settingsMenu.gameObject.GetComponent<CanvasElementVisibility>().Visible = false;
            about.gameObject.GetComponent<CanvasElementVisibility>().Visible = false;
            homeButton.gameObject.SetActive(false);
            exitButton.gameObject.SetActive(true);
        }
    }
}