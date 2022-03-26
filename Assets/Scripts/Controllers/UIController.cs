using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    [SerializeField] private Text mobTotal;
    [SerializeField] private Image gameOver;

    private void Awake()
    {
        gameOver.gameObject.SetActive(false);
    }

    public void UpdateMobTotal(int value)
    {
        mobTotal.text = value.ToString();
    }

    public void OnGameOver()
    {
        gameOver.gameObject.SetActive(true);
        Messenger<bool>.Broadcast(GameEvent.GAME_PAUSE, true);
    }

    public void OnExitClick()
    {
        Messenger<bool>.Broadcast(GameEvent.GAME_PAUSE, false);
        Managers.Mission.OpenMainMenu();
    }
}