using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCameraSoundController : MonoBehaviour
{
    private AudioSource BackgroundMusic = null;
    // Start is called before the first frame update
    void Start()
    {
        BackgroundMusic = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if(BackgroundMusic)
            BackgroundMusic.volume = PlayerPrefs.GetFloat(GlobalConfigs.MusicVolume, 1.0f);
    }
}
