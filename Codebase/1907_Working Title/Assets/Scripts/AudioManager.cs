using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public float SFXVolume = 1.0f;
    public float MusicVolume = 1.0f;
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

    public void PlayMenuSelectSound()
    {
        GetComponents<AudioSource>()[8].Play();
    }

    public void PlaySniperCharge()
    {
        GetComponents<AudioSource>()[9].Play();
    }
    public void StopSniperCharge()
    {
        GetComponents<AudioSource>()[9].Stop();
    }

    public void PlayEnemyPew()
    {
        GetComponents<AudioSource>()[10].Play();
    }

    public void PlayBulletRicochetSound()
    {
        GetComponents<AudioSource>()[11].Play();
    }

    public void PlayBossDeflect()
    {
        GetComponents<AudioSource>()[12].volume = GetComponents<AudioSource>()[12].volume * 0.4f;
        GetComponents<AudioSource>()[12].Play();
    }

    public void PlaySectorBasedExplosion()
    {
        GetComponents<AudioSource>()[13].Play();
    }
    public void PlayBigMeech()
    {
        GetComponents<AudioSource>()[14].Play();
    }
    public void StopBigMeech()
    {
        GetComponents<AudioSource>()[14].Stop();
    }
}
