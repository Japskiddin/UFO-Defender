using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDController : MonoBehaviour
{
    [Header("Properties")]
    [SerializeField] private Text mobTotal;

    public void UpdateMobTotal(int value)
    {
        mobTotal.text = value.ToString();
    }
}