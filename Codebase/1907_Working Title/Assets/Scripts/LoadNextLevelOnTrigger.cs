using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadNextLevelOnTrigger : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        PlayerStats.PlayerPrefSave();
        if (other.gameObject.CompareTag("Player"))
        {
            PlayerPrefs.SetInt("CurrentLevel", SceneManager.GetActiveScene().buildIndex);
            SceneManager.LoadScene("LoadingScreen");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
