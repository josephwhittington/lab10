using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    void Start()
    {
        PlayerPrefs.SetInt("CurrentLevel", SceneManager.GetActiveScene().buildIndex);
        PlayerPrefs.SetInt("StoreVisited", 1);
    }
    public void Scene_Switcher(string menu)
    {
        SceneManager.LoadScene("LoadingScreen");
    }

    public void ExitApp()
    {
#if DEBUG
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}