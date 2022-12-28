using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace UFO_Defense.Scripts.Managers
{
    [RequireComponent(typeof(ScenesManager))]
    [RequireComponent(typeof(AudioManager))]
    public class Manager : MonoBehaviour
    {
        public static AudioManager Audio { get; private set; }

        public static ScenesManager Scene { get; private set; }

        private List<IGameManager> _startSequence;

        private void Awake()
        {
            DontDestroyOnLoad(gameObject);

            Audio = GetComponent<AudioManager>();
            Scene = GetComponent<ScenesManager>();

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

            StartCoroutine(StartupManagers());
        }

        private IEnumerator StartupManagers()
        {
            foreach (var manager in _startSequence)
            {
                manager.Startup();
            }

            yield return null;

            var numModules = _startSequence.Count;
            var numReady = 0;

            while (numReady < numModules)
            {
                var lastReady = numReady;
                numReady = _startSequence.Count(manager => manager.Status == ManagerStatus.Started);

                if (numReady > lastReady)
                {
                    if (Debug.isDebugBuild)
                    {
                        Debug.Log($"Progress: {numReady} / {numModules}");
                    }

                    Messenger<int, int>.Broadcast(StartupEvent.ManagersProgress, numReady, numModules);
                }

                yield return null;
            }

            if (Debug.isDebugBuild)
            {
                Debug.Log("All managers started up");
            }

            Messenger.Broadcast(StartupEvent.ManagersStarted);
        }
    }
}