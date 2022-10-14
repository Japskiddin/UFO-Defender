using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BasePanel : MonoBehaviour
{
    private AudioClip _sound;
    private Image _panel;

    public virtual void Init(Image panel, AudioClip sound)
    {
        _panel = panel;
        _sound = sound;
        Hide();
    }

    public void Show()
    {
        _panel.gameObject.GetComponent<CanvasElementVisibility>().Visible = true;
    }

    public void Hide()
    {
        _panel.gameObject.GetComponent<CanvasElementVisibility>().Visible = false;
    }

    public bool Visible()
    {
        return _panel.gameObject.GetComponent<CanvasElementVisibility>().Visible;
    }

    public void PlaySound()
    {
        Managers.Audio.PlaySound(_sound);
    }
}