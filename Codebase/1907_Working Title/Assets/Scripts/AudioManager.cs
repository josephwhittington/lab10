﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private float SFXVolume = 1.0f;
    private float MusicVolume = 1.0f;
    void Start()
    {
        SFXVolume = PlayerPrefs.GetFloat(GlobalConfigs.SFXVolume, 1.0f);
        MusicVolume = PlayerPrefs.GetFloat(GlobalConfigs.MusicVolume, 1.0f);
    }

    void Update()
    {
        SFXVolume = PlayerPrefs.GetFloat(GlobalConfigs.SFXVolume, 1.0f);
        MusicVolume = PlayerPrefs.GetFloat(GlobalConfigs.MusicVolume, 1.0f);

        AudioSource[] things = GetComponents<AudioSource>();

        for (int i = 0; i < things.Length; i++)
        {
            if (i == 3) things[i].volume = MusicVolume;
            else things[i].volume = SFXVolume;
        }
    }

    public void PlayEnemyDeathSound()
    {
        GetComponents<AudioSource>()[0]?.Play();
    }

    public void PlayCoinPickup()
    {
        GetComponents<AudioSource>()[1]?.Play();
    }

    public void PlayHealthItemPickup()
    {
        GetComponents<AudioSource>()[2]?.Play();
    }

    public void PlayPauseMenuMusic()
    {
        GetComponents<AudioSource>()[3].loop = true;
        GetComponents<AudioSource>()[3]?.Play();
    }

    public void StopPauseMenuMusic()
    {
        GetComponents<AudioSource>()[3]?.Stop();
    }

    public void PlayDoorOpenSound()
    {
        GetComponents<AudioSource>()[4]?.Play();
    }

    public void PlayEnemyBoomerDeathSound()
    {
        //Debug.Log("Bruh");
        GetComponents<AudioSource>()[5].Play();
    }

    public void PlayExplosionSound()
    {
        GetComponents<AudioSource>()[6].Play();
    }

    public void PlayDoorDestructionSound()
    {
        GetComponents<AudioSource>()[7].Play();
    }
}