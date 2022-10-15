using UnityEngine;

namespace UFO_Defense.Scripts.UI
{
    [RequireComponent(typeof(CanvasGroup))]
    [ExecuteAlways]
    public class CanvasElementVisibility : MonoBehaviour
    {
        [SerializeField] private bool visible;

        private CanvasGroup _canvasGroup;

        public bool Visible
        {
            get => visible;
            set
            {
                visible = value;
                if (visible) ShowElement();
                else HideElement();
            }
        }

        private void OnValidate()
        {
            if (Visible) ShowElement();
            else HideElement();
        }

        private void ShowElement()
        {
            if (!_canvasGroup) _canvasGroup = GetComponent<CanvasGroup>();
            _canvasGroup.alpha = 1;
            _canvasGroup.interactable = true;
            _canvasGroup.blocksRaycasts = true;
        }

        private void HideElement()
        {
            if (!_canvasGroup) _canvasGroup = GetComponent<CanvasGroup>();
            _canvasGroup.alpha = 0;
            _canvasGroup.interactable = false;
            _canvasGroup.blocksRaycasts = false;
        }
    }
}