using System;
using UnityEngine;
using UnityEngine.UI;

namespace UFO_Defense.Scripts.UI
{
    [RequireComponent(typeof(Image))]
    public class MenuButton : MonoBehaviour
    {
        private Image _image;

        [Header("Properties")] [SerializeField]
        private Sprite[] backgrounds;

        private void Awake()
        {
            _image = GetComponent<Image>();
            if (_image == null)
            {
                throw new NullReferenceException("Component Image is null");
            }
        }

        private void Start()
        {
            _image.sprite = backgrounds[0];
        }

        private void OnMouseUp()
        {
            _image.sprite = backgrounds[0];
        }

        private void OnMouseDown()
        {
            _image.sprite = backgrounds[1];
        }
    }
}