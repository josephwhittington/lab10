using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningWallsController : MonoBehaviour
{
    // Spawn hit
    [SerializeField] private GameObject SpawnEffect = null;
    // Spawn hit

    [SerializeField] private float spawnInterval = 10.0f;

    // Explosion shit
    [SerializeField] private GameObject[] SectorObjs = null;
    [SerializeField] private GameObject[] SectorObjLights = null;
    [SerializeField] private GameObject SectorBasedExplosion = null;
    // Explosion shit

    [SerializeField] private GameObject[] Enemies = null;
    [SerializeField] private GameObject[] EnemySpawnPoints = null;
    private float timer = 0;
    private float phase3timer = 10.0f;

    private bool Phase3 = false;

    private int sector = 0;
    private bool sectorSelected = false;

    void Start()
    {
        // pawn shit
        timer = spawnInterval;
        // pawn shit

        gameObject.SetActive(false);

        // Lights
        for (int i = 0; i < SectorObjLights.Length; i++)
        {
            SectorObjLights[i].GetComponent<Light>().gameObject.SetActive(false);
        }
        // Lights
    }

    void Update()
    {
        if (Phase3)
            phase3timer -= Time.deltaTime;

        if (phase3timer <= 1 && !sectorSelected)
        {
            System.Random rand = new System.Random();
            sector = rand.Next(SectorObjs.Length);

            SectorObjLights[sector].GetComponent<Light>().gameObject.SetActive(true);
            sectorSelected = true;

        } else if (phase3timer < 1)
        {
            SectorObjLights[sector].GetComponent<Light>().intensity += Time.deltaTime * 100;
        }

        if (phase3timer <= 0)
        {
            SectorObjLights[sector].GetComponent<Light>().gameObject.SetActive(false);

            // Play Sound
            GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>().PlaySectorBasedExplosion();
            // Play Sound

            Instantiate(SectorBasedExplosion, SectorObjs[sector].transform.position, SectorObjs[sector].transform.rotation);
            phase3timer = 10;
            sectorSelected = false;
        }

        timer -= Time.deltaTime;

        if (timer <= 0)
        {
            for (int i = 0; i < EnemySpawnPoints.Length; i++)
            {
                Instantiate<GameObject>(SpawnEffect, EnemySpawnPoints[i].transform.position,
                    EnemySpawnPoints[i].transform.rotation);
            }

            Invoke("Spawn", 1.0f);

            timer = spawnInterval;
        }
    }

    private void Spawn()
    {
        for (int i = 0; i < EnemySpawnPoints.Length; i++)
        {
            Instantiate<GameObject>(Enemies[0], EnemySpawnPoints[i].transform.position,
                EnemySpawnPoints[i].transform.rotation);
        }
    }

    public void StartPhase3()
    {
        Phase3 = true;
    }

    public void EnableEnvEffects()
    {
        gameObject.SetActive(true);
    }
}
