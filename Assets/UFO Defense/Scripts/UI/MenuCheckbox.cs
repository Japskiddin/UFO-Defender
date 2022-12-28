using System;
using UnityEngine;
using UnityEngine.UI;

namespace UFO_Defense.Scripts.UI
{
    [RequireComponent(typeof(Image))]
    public class MenuCheckbox : MonoBehaviour
    {
        private Image _image;

        [Header("Properties")]
        [SerializeField] private Sprite[] images;

        private void Awake()
        {
            _image = GetComponent<Image>();
            if (_image == null)
            {
                throw new NullReferenceException("Component Image is null");
            }
        }

        public void SetChecked(bool check)
        {
            _image.sprite = check ? images[0] : images[1];
        }
    }
}