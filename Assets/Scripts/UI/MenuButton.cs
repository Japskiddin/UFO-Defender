using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class MenuButton : MonoBehaviour
{
    [SerializeField] private Sprite[] backgrounds;
    private Image image;

    private void Awake()
    {
        image = GetComponent<Image>();
    }

    void Start()
    {
        image.sprite = backgrounds[0];
    }

    private void OnMouseUp()
    {
        image.sprite = backgrounds[0];
    }

    private void OnMouseDown()
    {
        image.sprite = backgrounds[1];
    }
}