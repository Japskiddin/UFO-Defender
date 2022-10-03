using UnityEngine;

public class MainMenuController : MonoBehaviour
{
    [Header("Properties")]
    [SerializeField] private AudioClip sound;
    [Header("Panels")]
    [SerializeField] private RectTransform mainMenu;
    [SerializeField] private RectTransform settingsMenu;
    [SerializeField] private RectTransform about;
    [SerializeField] private RectTransform homeButton;
    [SerializeField] private RectTransform exitButton;

    private void Awake()
    {
        mainMenu.gameObject.SetActive(true);
        settingsMenu.gameObject.SetActive(false);
        about.gameObject.SetActive(false);
        homeButton.gameObject.SetActive(false);
        exitButton.gameObject.SetActive(true);
        Time.timeScale = 1;
    }

    public void OnExitClick()
    {
        Managers.Audio.PlaySound(sound);
        Application.Quit();
    }

    public void OnStartClick()
    {
        Managers.Audio.PlaySound(sound);
        Managers.Scene.GoToNext();
    }

    public void OnSettingsClick()
    {
        Managers.Audio.PlaySound(sound);
        mainMenu.gameObject.SetActive(false);
        settingsMenu.gameObject.SetActive(true);
        about.gameObject.SetActive(false);
        homeButton.gameObject.SetActive(true);
        exitButton.gameObject.SetActive(false);
    }

    public void OnHomeClick()
    {
        Managers.Audio.PlaySound(sound);
        mainMenu.gameObject.SetActive(true);
        settingsMenu.gameObject.SetActive(false);
        about.gameObject.SetActive(false);
        homeButton.gameObject.SetActive(false);
        exitButton.gameObject.SetActive(true);
    }
}