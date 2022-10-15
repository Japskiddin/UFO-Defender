using UFO_Defense.Scripts.Managers;
using UFO_Defense.Scripts.UI;
using UnityEngine;

namespace UFO_Defense.Scripts.Controllers
{
    public class StartupController : MonoBehaviour
    {
        [Header("Properties")] [SerializeField]
        private ProgressBar progressBar;

        private void Awake()
        {
            Messenger<int, int>.AddListener(StartupEvent.ManagersProgress, OnManagersProgress);
            Messenger.AddListener(StartupEvent.ManagersStarted, OnManagersStarted);
        }

        private void OnDestroy()
        {
            Messenger<int, int>.RemoveListener(StartupEvent.ManagersProgress, OnManagersProgress);
            Messenger.RemoveListener(StartupEvent.ManagersStarted, OnManagersStarted);
        }

        private void OnManagersProgress(int numReady, int numModules)
        {
            progressBar.SetMaxValue(numModules);
            progressBar.SetValue(numReady);
        }

        private static void OnManagersStarted()
        {
            Manager.Scene.OpenMainMenu();
        }
    }
}