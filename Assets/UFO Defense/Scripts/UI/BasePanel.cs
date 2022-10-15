using UFO_Defense.Scripts.Managers;
using UnityEngine;
using UnityEngine.UI;

namespace UFO_Defense.Scripts.UI
{
    public class BasePanel : MonoBehaviour
    {
        private AudioClip _sound;
        private Image _panel;
        private CanvasElementVisibility _canvas;

        public void Init(Image panel, AudioClip sound)
        {
            _panel = panel;
            _sound = sound;
            _canvas = _panel.gameObject.GetComponent<CanvasElementVisibility>();
            Hide();
        }

        public void Show()
        {
            _canvas.Visible = true;
        }

        public void Hide()
        {
            _canvas.Visible = false;
        }

        public bool Visible()
        {
            return _canvas.Visible;
        }

        public void PlaySound()
        {
            Manager.Audio.PlaySound(_sound);
        }
    }
}