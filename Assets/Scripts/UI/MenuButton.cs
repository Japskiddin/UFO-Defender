using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

[RequireComponent(typeof(Image))]
public class MenuButton : MonoBehaviour
{
    private Image _image;

    [Header("Properties")]
    [SerializeField] private Sprite[] backgrounds;

    private void Awake()
    {
        _image = GetComponent<Image>();
        if (_image == null)
        {
            throw new NullReferenceException("Component Image is null");
        }
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