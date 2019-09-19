using System.Collections;
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
        PlayMenuSelectSound();
#if DEBUG
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
    public void RestartClicked()
    {
        PlayMenuSelectSound();
        DeathScreen.enabled = false;
        RestartStats();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    void PlayMenuSelectSound()
    {
        GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>().PlayMenuSelectSound();
    }

    void RestartStats()
    {
        PlayerStats.LoadPlayerPrefs();
        PlayerPrefs.SetInt("CurrentHealth", (int)PlayerStats.MaxHitPoints);
        PlayerStats.PlayerDead = false;

        //alex killcount
        if (SceneManager.GetActiveScene().name != "BossManLevel")
        {
            KillCounter.instance.SetCount();
            KillCounter.instance.UpdateCounter();
        }
        //alex killcount
    }
}
