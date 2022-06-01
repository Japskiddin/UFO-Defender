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
    // Список диспетчеров, который просматривается в цикле во время стартовой последовательности.
    private List<IGameManager> _startSequence;

    private void Awake() {
        // Команда Unity для сохранения объекта между сценами.
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

        // Асинхронно загружаем стартовую последовательность.
        StartCoroutine(StartupManagers());
    }

    private IEnumerator StartupManagers() {
        foreach(IGameManager manager in _startSequence) {
            manager.Startup();
        }

        yield return null;

        int numModules = _startSequence.Count;
        int numReady = 0;

        // Продолжаем цикл, пока не начнут работать все диспетчеры.
        while (numReady < numModules) {
            int lastReady = numReady;
            numReady = 0;

            foreach(IGameManager manager in _startSequence) {
                if (manager.Status == ManagerStatus.Started) {
                    numReady++;
                }
            }

            if (numReady > lastReady) {
                Debug.Log("Progress: " + numReady + "/" + numModules);
                // Событие загрузки рассылается вместе с параметрами.
                Messenger<int, int>.Broadcast(StartupEvent.MANAGERS_PROGRESS, numReady, numModules);
            }

            // Остановка на один кадр перед следующей проверкой.
            yield return null;
        }

        Debug.Log("All managers started up");
        // Событие загрузки рассылается без параметров.
        Messenger.Broadcast(StartupEvent.MANAGERS_STARTED);
    }
}