using UnityEngine;
using UnityEngine.UI;

namespace UFO_Defense.Scripts.UI
{
    public class ProgressBar : MonoBehaviour
    {
        [Header("UI Elements")] [SerializeField]
        private Image image;

        [Header("Properties")] [SerializeField]
        private int value;

        [SerializeField] private int maxValue = 100;
        [SerializeField] private bool isCorrectConfigured;

        private void Awake()
        {
            if (image.type == Image.Type.Filled && image.fillMethod == Image.FillMethod.Horizontal)
            {
                isCorrectConfigured = true;
            }
            else
            {
                if (Debug.isDebugBuild)
                {
                    Debug.Log(
                        "{GameLog} => [ProgressBar] - (<color=red>Error</color>) -> Components Parameters Are Incorrectly Configured! \n " +
                        "Required Type Filled \n" +
                        "Required FillMethod Horizontal");
                }
            }
        }

        private void LateUpdate()
        {
            if (!isCorrectConfigured) return;
            image.fillAmount = (float)value / maxValue;
        }

        public void SetValue(int newValue)
        {
            value = newValue;
        }

        public void SetMaxValue(int newMaxValue)
        {
            maxValue = newMaxValue;
        }
    }
}