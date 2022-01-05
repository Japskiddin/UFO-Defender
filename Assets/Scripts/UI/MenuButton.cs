using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuButton : MonoBehaviour
{
    [SerializeField] private Sprite[] backgrounds;

    // Start is called before the first frame update
    void Start()
    {
        Image image = GetComponent<Image>();
        if (image != null)
        {
            image.sprite = backgrounds[0];
        }
    }

    private void OnMouseUp()
    {
        Image image = GetComponent<Image>();
        if (image != null)
        {
            image.sprite = backgrounds[0];
        }
    }

    private void OnMouseDown()
    {
        Image image = GetComponent<Image>();
        if (image != null)
        {
            image.sprite = backgrounds[1];
        }
    }
}