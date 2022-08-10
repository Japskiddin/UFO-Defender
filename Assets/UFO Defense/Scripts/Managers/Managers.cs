using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[RequireComponent(typeof(SceneManager))]
[RequireComponent(typeof(AudioManager))]
public class Managers : MonoBehaviour
{
    public static AudioManager Audio { get; private set; }
    public static SceneManager Scene { get; private set; }
    // ������ �����������, ������� ��������������� � ����� �� ����� ��������� ������������������.
    private List<IGameManager> _startSequence;

    private void Awake()
    {
        // ������� Unity ��� ���������� ������� ����� �������.
        DontDestroyOnLoad(gameObject);

        Audio = GetComponent<AudioManager>();
        Scene = GetComponent<SceneManager>();

        if (Audio == null)
        {
            throw new NullReferenceException("Audio manager is null");
        }
        if (Scene == null)
        {
            throw new NullReferenceException("Scene manager is null");
        }

        _startSequence = new List<IGameManager>
        {
            Scene,
            Audio
        };

        // ���������� ��������� ��������� ������������������.
        StartCoroutine(StartupManagers());
    }

    private IEnumerator StartupManagers()
    {
        foreach (IGameManager manager in _startSequence)
        {
            manager.Startup();
        }

        yield return null;

        int numModules = _startSequence.Count;
        int numReady = 0;

        // ���������� ����, ���� �� ������ �������� ��� ����������.
        while (numReady < numModules)
        {
            int lastReady = numReady;
            numReady = 0;

            foreach (IGameManager manager in _startSequence)
            {
                if (manager.Status == ManagerStatus.Started)
                {
                    numReady++;
                }
            }

            if (numReady > lastReady)
            {
                if (Debug.isDebugBuild)
                {
                    Debug.Log("Progress: " + numReady + "/" + numModules);
                }
                // ������� �������� ����������� ������ � �����������.
                Messenger<int, int>.Broadcast(StartupEvent.MANAGERS_PROGRESS, numReady, numModules);
            }

            // ��������� �� ���� ���� ����� ��������� ���������.
            yield return null;
        }

        if (Debug.isDebugBuild)
        {
            Debug.Log("All managers started up");
        }
        // ������� �������� ����������� ��� ����������.
        Messenger.Broadcast(StartupEvent.MANAGERS_STARTED);
    }
}