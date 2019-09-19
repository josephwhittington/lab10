using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    void Start()
    {
        if(!PlayerPrefs.HasKey("CurrentLevel"))
            PlayerPrefs.SetInt("CurrentLevel", SceneManager.GetActiveScene().buildIndex);

        PlayerPrefs.SetInt("StoreVisited", 1);
    }
    public void Scene_Switcher(string menu)
    {
        //PlayerStats.LoadPlayerPrefs();
        GameState.GamePaused = false;

        PlayMenuSelectSound();
        SceneManager.LoadScene("LoadingScreen");
    }

    public void NewGame()
    {
        PlayMenuSelectSound();
        PlayerPrefs.DeleteAll();
        PlayerPrefs.SetInt("StoreVisited", 1);
        GameState.GamePaused = false;

        SceneManager.LoadScene("LoadingScreen");
    }

    void PlayMenuSelectSound()
    {
        GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>().PlayMenuSelectSound();
    }

    public void ExitApp()
    {
        PlayMenuSelectSound();
#if DEBUG
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}