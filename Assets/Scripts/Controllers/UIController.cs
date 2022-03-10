using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    [SerializeField] Text mobTotal;

    public void UpdateMobTotal(int value)
    {
        mobTotal.text = value.ToString();
    }
}