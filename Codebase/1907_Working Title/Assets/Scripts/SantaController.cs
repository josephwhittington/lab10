using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SantaController : MonoBehaviour
{
    [SerializeField] private int Health = 200;

    [SerializeField] private GameObject CoinPrefab = null;
    [SerializeField] private GameObject CoinSpawnLocation = null;

    void Start()
    {
        GetComponent<AudioSource>().volume =
            GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>().SFXVolume;
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Projectile"))
        {
#if DEBUG
            Debug.Log("Hit");
#endif
            Health -= 1;

            Instantiate<GameObject>(CoinPrefab, CoinSpawnLocation.transform.position,
                CoinSpawnLocation.transform.rotation);
        }
    }

    // Update is called once per frame
    void Update()
    {
        GetComponent<AudioSource>().volume =
            GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>().SFXVolume;

        if (Health <= 0)
        {
            GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>().PlayEnemyBoomerDeathSound();
            Destroy(gameObject);
        }
    }
}
