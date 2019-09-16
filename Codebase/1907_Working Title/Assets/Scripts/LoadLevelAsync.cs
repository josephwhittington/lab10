using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadLevelAsync : MonoBehaviour
{

    [SerializeField] private Image ProgressBar = null;
    private AsyncOperation Level = null;
    private float time = 0.0f;

    void Start()
    {
        StartCoroutine(LoadAsyncOperation());

    }

    IEnumerator LoadAsyncOperation()
    {
        if (PlayerPrefs.GetInt("StoreVisited", 0) == 1)
        {
            if (PlayerPrefs.GetInt("CurrentLevel") == 7)
            {
                PlayerPrefs.DeleteAll();

                Level = SceneManager.LoadSceneAsync("Main Menu");
                Level.allowSceneActivation = false;
            }
            else
            {
                Level = SceneManager.LoadSceneAsync(PlayerPrefs.GetInt("CurrentLevel") + 1);
                Level.allowSceneActivation = false;
            }
        }
        else
        {
            Level = SceneManager.LoadSceneAsync("Store");
            Level.allowSceneActivation = false;
        }
        yield return null;
    }

    // Update is called once per frame
    void Update()
    {
        if (time >= 3)
            Level.allowSceneActivation = true;
        else
        {
            time += Time.deltaTime;
            ProgressBar.fillAmount = (float) (time / 3);
        }
    }
}
