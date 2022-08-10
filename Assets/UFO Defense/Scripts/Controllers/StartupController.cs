using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartupController : MonoBehaviour
{
    [Header("Properties")]
    [SerializeField] private ProgressBar progressBar;

    private void Awake()
    {
        Messenger<int, int>.AddListener(StartupEvent.MANAGERS_PROGRESS, OnManagersProgress);
        Messenger.AddListener(StartupEvent.MANAGERS_STARTED, OnManagersStarted);
    }

    private void OnDestroy()
    {
        Messenger<int, int>.RemoveListener(StartupEvent.MANAGERS_PROGRESS, OnManagersProgress);
        Messenger.RemoveListener(StartupEvent.MANAGERS_STARTED, OnManagersStarted);
    }

    private void OnManagersProgress(int numReady, int numModules)
    {
        // Обновляем ползунок данными о процессе загрузки.
        progressBar.SetMaxValue(numModules);
        progressBar.SetValue(numReady);
    }

    private void OnManagersStarted()
    {
        // После загрузки диспетчеров загружаем следующую сцену.
        Managers.Scene.OpenMainMenu();
    }
}