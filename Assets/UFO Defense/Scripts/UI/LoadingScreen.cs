using UnityEngine;

namespace UFO_Defense.Scripts.UI
{
    public class LoadingScreen : MonoBehaviour
    {
        [SerializeField] ProgressBar progress;

        void Start()
        {
            progress.SetValue(0);
        }

        public void SetProgress(int value)
        {
            progress.SetValue(value);
        }
    }
}