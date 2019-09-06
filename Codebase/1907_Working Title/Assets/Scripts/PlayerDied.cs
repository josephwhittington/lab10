using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerDied : MonoBehaviour
{
    [SerializeField] Button ExitButton = null, RestartButton = null;
    void Start()
    {
        gameObject.SetActive(false);
        ExitButton.onClick.AddListener(ExitClicked);
        RestartButton.onClick.AddListener(RestartClicked);
    }
    void ExitClicked()
    {
        Application.Quit();
    }
    void RestartClicked()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);

    }

}
