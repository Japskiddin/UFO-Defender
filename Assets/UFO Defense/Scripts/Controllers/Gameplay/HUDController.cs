using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HUDController : MonoBehaviour
{
    [Header("Properties")]
    [SerializeField] private TextMeshProUGUI mobTotal;

    public void UpdateMobTotal(int value)
    {
        mobTotal.text = value.ToString();
    }
}