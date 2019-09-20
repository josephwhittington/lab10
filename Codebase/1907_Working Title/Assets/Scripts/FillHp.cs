using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FillHp : MonoBehaviour
{
    [SerializeField] Button fillHP = null;

    void Start()
    {
        if(fillHP)
            fillHP.onClick.AddListener(Fill);
    }
    void Fill()
    {
        if(PlayerStats.Coins >= 50)
        {
            PlayerStats.Coins -= 50;
            PlayerStats.CurrentHealth = PlayerStats.MaxHitPoints;
        }
    }
}
