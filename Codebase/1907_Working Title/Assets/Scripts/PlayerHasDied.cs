﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerHasDied : MonoBehaviour
{
    [SerializeField] Canvas DeathScreen = null;
    [SerializeField] Button ExitButton = null, RestartButton = null;
    void Start()
    {
        DeathScreen.enabled = false;
        ExitButton.onClick.AddListener(ExitClicked);
        RestartButton.onClick.AddListener(RestartClicked);
    }
    void Update()
    {
        if(PlayerStats.PlayerDead)
        {
            EnableDeathScreen();
        }
    }
    void EnableDeathScreen()
    {
        DeathScreen.enabled = true;
    }
    public void ExitClicked()
    {
#if DEBUG
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
    public void RestartClicked()
    {
        DeathScreen.enabled = false;
        RestartStats();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    void RestartStats()
    {
        PlayerPrefs.SetInt("CurrentHealth", (int)PlayerStats.MaxHitPoints);

        PlayerStats.CurrentHealth = PlayerStats.MaxHitPoints;
        PlayerStats.Coins = 0;
        PlayerStats.PlayerDead = false;
    }
}