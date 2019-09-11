using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningWallsController : MonoBehaviour
{
    [SerializeField] private float spawnInterval = 10.0f;

    // Explosion shit
    [SerializeField] private GameObject[] SectorObjs = null;
    [SerializeField] private GameObject SectorBasedExplosion = null;
    // Explosion shit

    [SerializeField] private GameObject[] Enemies = null;
    [SerializeField] private GameObject[] EnemySpawnPoints = null;
    private float timer = 0;
    private float phase3timer = 10.0f;

    private bool Phase3 = false;

    void Start()
    {
        // pawn shit
        timer = spawnInterval;
        // pawn shit

        gameObject.SetActive(false);
    }

    void Update()
    {
        if (Phase3)
            phase3timer -= Time.deltaTime;

        if (phase3timer <= 0)
        {
            System.Random rand = new System.Random();
            int spawnindex = rand.Next(SectorObjs.Length);

            Instantiate(SectorBasedExplosion, SectorObjs[spawnindex].transform.position, SectorObjs[spawnindex].transform.rotation);
            phase3timer = 10;
        }

        timer -= Time.deltaTime;

        if (timer <= 0)
        {
            for (int i = 0; i < EnemySpawnPoints.Length; i++)
            {
                Instantiate<GameObject>(Enemies[0], EnemySpawnPoints[i].transform.position,
                    EnemySpawnPoints[i].transform.rotation);
            }

            timer = spawnInterval;
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
