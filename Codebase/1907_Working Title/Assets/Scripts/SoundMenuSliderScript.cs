using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class SoundMenuSliderScript : IPausable
{
    [SerializeField] private Slider SFXSlider = null, MusicSlider = null;

    void Start()
    {
        SFXSlider.value = PlayerPrefs.GetFloat(GlobalConfigs.SFXVolume, 1.0f);
        MusicSlider.value = PlayerPrefs.GetFloat(GlobalConfigs.MusicVolume, 1.0f);
    }

    public void UpdateSFX()
    {
        PlayerPrefs.SetFloat(GlobalConfigs.SFXVolume, SFXSlider.value);
        PlayerPrefs.Save();

        if(GamePaused)
            GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>().PlayCoinPickup();
    }

    public void UpdateMusic()
    {
        PlayerPrefs.SetFloat(GlobalConfigs.MusicVolume, MusicSlider.value);
        PlayerPrefs.Save();
    }
}
