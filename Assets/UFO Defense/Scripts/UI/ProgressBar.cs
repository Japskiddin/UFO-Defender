using UnityEngine;
using UnityEngine.UI;

namespace UFO_Defense.Scripts.UI
{
    public class ProgressBar : MonoBehaviour
    {
        [Header("UI Elements")]
        [SerializeField] private Image image;
        [SerializeField] private Image thumb;

        [Header("Properties")]
        [SerializeField] private int value;
        [SerializeField] private int maxValue = 100;
        [SerializeField] private bool isCorrectConfigured;
        [SerializeField] private bool hasThumb;

        private const int OriginLeft = 0;
        private const int OriginRight = 1;

        private void Awake()
        {
            var isImageFillConfigured = image.type == Image.Type.Filled && image.fillMethod == Image.FillMethod.Horizontal;
            if (!isImageFillConfigured)
            {
                if (Debug.isDebugBuild)
                {
                    Debug.LogError(
                        "{GameLog} => [ProgressBar] - (<color=red>Error</color>) -> Components Parameters Are Incorrectly Configured! \n " +
                        "Required Type Filled \n" +
                        "Required FillMethod Horizontal");
                }
                return;
            }
            if (hasThumb)
            {
                if (thumb == null)
                {
                    if (Debug.isDebugBuild)
                    {
                        Debug.LogError(
                            "{GameLog} => [ProgressBar] - (<color=red>Error</color>) -> Components Parameters Are Incorrectly Configured! \n " +
                            "Required Image Thumb");
                    }
                    return;
                }

                InitThumbPosition();
            }

            isCorrectConfigured = true;
        }

        private void LateUpdate()
        {
            if (!isCorrectConfigured) return;
            image.fillAmount = (float)value / maxValue;
            UpdateThumbPosition();
        }

        private void InitThumbPosition()
        {
            var thumbRect = thumb.GetComponent<RectTransform>();
            if (image.fillOrigin == OriginRight)
            {
                thumbRect.anchorMin = new Vector2(0, 0.5f);
                thumbRect.anchorMax = new Vector2(0, 0.5f);
            }
            else
            {
                thumbRect.anchorMin = new Vector2(1, 0.5f);
                thumbRect.anchorMax = new Vector2(1, 0.5f);
            }
            thumbRect.anchoredPosition = new Vector2(0, 0);
        }

        private void UpdateThumbPosition()
        {
            if (!isCorrectConfigured || !hasThumb) return;
            var progressWidth = image.GetComponent<RectTransform>().rect.width;
            var thumbX = progressWidth - progressWidth * image.fillAmount;
            var thumbRect = thumb.GetComponent<RectTransform>();
            thumbRect.anchoredPosition = new Vector2(thumbX * (image.fillOrigin == OriginLeft ? -1 : 1), 0);
        }

        public void SetValue(int newValue)
        {
            if (!isCorrectConfigured) return;
            value = newValue;
        }

        public void SetMaxValue(int newMaxValue)
        {
            if (!isCorrectConfigured) return;
            maxValue = newMaxValue;
        }
    }
}