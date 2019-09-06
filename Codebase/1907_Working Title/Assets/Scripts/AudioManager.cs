using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    void Start()
    {
        
    }

    void Update()
    {
        
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
        GetComponents<AudioSource>()[5].volume = 1.0f;
        GetComponents<AudioSource>()[5].Play();
    }

    public void PlayExplosionSound()
    {
        GetComponents<AudioSource>()[6].Play();
    }
}
