using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinThiefSpawner : IPausable
{
    //spawnPoints
    [SerializeField] string roomName = "";
    [SerializeField] float SpawnInterval = 3.0f;
    [SerializeField] GameObject[] SpawnPoints = null;
    [SerializeField] GameObject Enemy = null;
    private float timer = 0;
    private int maxSpawns = 0;
    bool ShouldSpawn = false;
    int lastspawn = -1;

    private void OnEnable()
    {
        StartSpawner.PlayerEntered += StartSpawning;
    }

    void StartSpawning(string RoomName)
    {
        if (RoomName == roomName)
        {
            ShouldSpawn = true;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!GamePaused)
        {

            if (ShouldSpawn)
            {
                timer += Time.deltaTime;
                if (timer >= SpawnInterval)
                {
                    timer = 0;

                    if (!GameState.GamePaused)
                    {
                        Spawn();
                    }
                }

            }
        }
    }

    void Spawn()
    {
        System.Random rand = new System.Random();
        int spawnpointindex = rand.Next(SpawnPoints.Length);
        lastspawn = spawnpointindex;

        while (SpawnPoints.Length > 1 && spawnpointindex == lastspawn)
        {
            spawnpointindex = rand.Next(SpawnPoints.Length);
        }

        lastspawn = spawnpointindex;

        if (maxSpawns  < 1)
        {
            Instantiate<GameObject>(Enemy, SpawnPoints[spawnpointindex].transform.position, transform.rotation);
            maxSpawns += 1;
        }
    }

    private void OnDisable()
    {
        StartSpawner.PlayerEntered -= StartSpawning;
    }

}


