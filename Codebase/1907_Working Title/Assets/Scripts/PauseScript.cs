﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseScript : MonoBehaviour
{
    [SerializeField] private Canvas PauseMenu = null, SoundMenu = null;
    [SerializeField] private Button PlayButton = null, QuitButton = null, OptionsMenu = null, OptionsQuitButton = null;
    private bool IsPaused = false;

    // Start is called before the first frame update
    void Start()
    {
        PauseMenu.gameObject.SetActive(false);
        SoundMenu.gameObject.SetActive(false);

        PlayButton?.onClick.AddListener(PlayButtonOnClick);
        OptionsMenu.onClick.AddListener(OptionsMenuOnClick);
        QuitButton?.onClick.AddListener(QuitButtonClick);
        OptionsQuitButton?.onClick.AddListener(OptionsQuitButtonOnClick);
    }

    private void PlayButtonOnClick()
    {
        //Debug.Log("Play Button Clicked");
        IsPaused = !IsPaused;
        PauseMenu.gameObject.SetActive(IsPaused);

        TriggerEvent();
    }

    private void OptionsMenuOnClick()
    {
        SoundMenu.gameObject.SetActive(true);
        PauseMenu.gameObject.SetActive(false);
    }

    private void OptionsQuitButtonOnClick()
    {
        SoundMenu.gameObject.SetActive(false);
        PauseMenu.gameObject.SetActive(true);
    }

    private void QuitButtonClick()
    {
        //Debug.Log("Closing Application");
        SceneManager.LoadScene(0);
    }

    void TriggerEvent()
    {
        GameObject UI = GameObject.FindGameObjectWithTag("EventManager");

        if (IsPaused)
        {
            // Pause ambient music
            Camera.main.GetComponent<AudioSource>().Pause();
            // Play pause music
            GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>().PlayPauseMenuMusic();

            if (UI)
                UI.GetComponent<EventManger>().SendMessage("HandleGenericEvent", "PAUSE");
        }
        else
        {
            // Pause pause menu music
            GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>().StopPauseMenuMusic();
            // Play ambient music
            Camera.main.GetComponent<AudioSource>().UnPause();

            UI.GetComponent<EventManger>().SendMessage("HandleGenericEvent", "UNPAUSE");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if ((Input.GetKeyDown(KeyCode.P) || Input.GetKeyDown(KeyCode.Escape)) && !PlayerStats.PlayerDead)
        {
            IsPaused = !IsPaused;
            PauseMenu.gameObject.SetActive(IsPaused);

            TriggerEvent();
        }
    }
}
