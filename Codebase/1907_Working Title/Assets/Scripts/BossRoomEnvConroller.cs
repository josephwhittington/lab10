using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossRoomEnvConroller : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        GetComponent<AudioSource>().volume =
            GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>().SFXVolume;
    }

    public void EnableEnvEffect()
    {
        gameObject.SetActive(true);
    }
}
