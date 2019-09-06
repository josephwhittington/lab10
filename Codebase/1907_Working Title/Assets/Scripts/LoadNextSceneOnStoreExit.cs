using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadNextSceneOnStoreExit : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        PlayerStats.PlayerPrefSave();
        if (other.gameObject.CompareTag("Player"))
        {
            PlayerPrefs.SetInt("StoreVisited", 1);
            SceneManager.LoadScene("LoadingScreen");
        }
    }
}
