using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class MenuButton : MonoBehaviour
{
    [SerializeField] private Sprite[] backgrounds;
    private Image _image;

    private void Awake()
    {
        _image = GetComponent<Image>();
    }

    void Start()
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