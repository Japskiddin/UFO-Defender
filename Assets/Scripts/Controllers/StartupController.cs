using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartupController : MonoBehaviour
{
    [SerializeField] private ProgressBarController progressBar;

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
        // ��������� �������� ������� � �������� ��������.
        progressBar.SetMaxValue(numModules);
        progressBar.SetValue(numReady);
    }

    private void OnManagersStarted()
    {
        // ����� �������� ����������� ��������� ��������� �����.
        Managers.Mission.OpenMainMenu();
    }
}