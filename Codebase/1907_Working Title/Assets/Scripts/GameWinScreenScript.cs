using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class GameWinScreenScript : MonoBehaviour
{
    public void Exit()
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene("Main Menu");
    }

    public void LoadCredits()
    {
        SceneManager.LoadScene("Credits");
    }
}
