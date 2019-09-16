using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine;

public class DisableContinueButton : MonoBehaviour
{
    [SerializeField] private Button ContinueButton = null;

    // Start is called before the first frame update
    void Start()
    {
        if (!PlayerPrefs.HasKey("CurrentLevel"))
            ContinueButton.interactable = false;
        else
        {
            if (SceneManager.GetActiveScene().buildIndex == PlayerPrefs.GetInt("CurrentLevel"))
            {
                ContinueButton.interactable = false;
            }
            else ContinueButton.interactable = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
