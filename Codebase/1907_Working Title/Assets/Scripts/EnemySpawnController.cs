using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemySpawnController : IPausable
{
    //Alex Spawn Effect
    [SerializeField] GameObject SpawnEffect = null;
    //Alex Spawn Effect

    // Whittington door gaurd safety thing
    [SerializeField] private GameObject[] DoorGuards = null;
    // Whittington door gaurd safety thing

    [SerializeField] string roomName = "";
    [SerializeField] GameObject[] DoorLights = null;
    [SerializeField] GameObject[] SpawnPoints = null;
    [SerializeField] GameObject[] Enemies = null;

    [SerializeField] uint[] EnemyWeights = null;
    [SerializeField] float SpawnInterval = 3.0f;
    [SerializeField] private float HealthPickupSpawnInterval = 6.0f;
    [SerializeField] uint RoomWeight = 10;

    List<GameObject> EnemiyList;

    bool ShouldSpawn = false;
    bool RoomClear = false;
    bool RoomClearTriggered = false;

    // Random Number Generation
    System.Random rand = new System.Random();
    // Random Number Generation

    private float timer = 0;
    private uint RoomStartWeight = 0;
    int lastspawn = -1;

    // Enemy spawn timer
    [SerializeField] private float WaitTimebeforeSpawn = 0.0f;

    //private float enemyspawntimer = 0;
    private List<GameObject> enemy = new List<GameObject>();
    private List<Vector3> enemylocation = new List<Vector3>();
    private List<uint> EnemySpawnWeights = new List<uint>();

    private float waittimebeforeroomclearcheck = 5.0f;
    // Enemy spawn timer

    private void OnEnable()
    {
        StartSpawner.PlayerEntered += StartSpawning;
    }

    void Start()
    {
        RoomStartWeight = RoomWeight;
        DisableDoors();

        if (RoomWeight == 0) waittimebeforeroomclearcheck = 0;
    }

    void Update()
    {
        if (!GamePaused)
        {
            if (ShouldSpawn && WaitTimebeforeSpawn > 0)
                WaitTimebeforeSpawn -= Time.deltaTime;

            if (!RoomClear && ShouldSpawn && WaitTimebeforeSpawn <= 0)
            {
                // Timer shit
                timer += Time.deltaTime;

                if (timer >= SpawnInterval)
                {
                    timer = 0;

                    if (!GameState.GamePaused)
                    {
                        SetSpawn();
                        SetSpawn();
                    }
                }
            }

            CheckIfRoomClear();
        }
    }


    void StartSpawning(string RoomName)
    {
        if (RoomName == roomName)
        {
            ShouldSpawn = true;
            DisableDoors();
            InvokeRepeating("SpawnHealthPickup", HealthPickupSpawnInterval, HealthPickupSpawnInterval);

            // For roomclear check
            InvokeRepeating("SubstractWaitTimeBeforeRoomClearCheck", 0, 1.0f);
            // For roomclear check
        }
    }

    void SubstractWaitTimeBeforeRoomClearCheck()
    {
        waittimebeforeroomclearcheck -= 1.0f;

        if(waittimebeforeroomclearcheck <= 0.0f)
            CancelInvoke("SubstractWaitTimeBeforeRoomClearCheck");
    }

    void CheckIfRoomClear()
    {
        if ((RoomWeight <= 0 || RoomWeight > RoomStartWeight) && waittimebeforeroomclearcheck <= 0)
        {
            int NumberOfEnemies = 0;

            if (GameObject.FindGameObjectsWithTag("Enemy").Length > 0)
                NumberOfEnemies +=  GameObject.FindGameObjectsWithTag("Enemy").Length;
            if (GameObject.FindGameObjectsWithTag("EnemyBoom").Length > 0)
                NumberOfEnemies +=  GameObject.FindGameObjectsWithTag("EnemyBoom").Length;
            if (GameObject.FindGameObjectsWithTag("Boomer").Length > 0)
                NumberOfEnemies +=  GameObject.FindGameObjectsWithTag("Boomer").Length;
            if (GameObject.FindGameObjectsWithTag("Sniper").Length > 0)
                NumberOfEnemies += GameObject.FindGameObjectsWithTag("Sniper").Length;
            if (GameObject.FindGameObjectsWithTag("CoinThief").Length > 0)
                NumberOfEnemies += GameObject.FindGameObjectsWithTag("CoinThief").Length;

            if (NumberOfEnemies <= 0)
                RoomClear = true;
        }

        if(RoomClear && !RoomClearTriggered)
        {
            RoomClearTriggered = true;
            // Do things here on room clear
            CancelInvoke("SpawnHealthPickup");
            OpenDoors();
        }
    }
    void OpenDoors()
    {
        if(DoorGuards.Length > 0)
        {
            for (int i = 0; i < DoorGuards.Length; i++)
            {
                DoorGuards[i].SetActive(false);
            }
        }
        else
        {
            GameObject[] door_guards = GameObject.FindGameObjectsWithTag("DoorGuard");

            GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>().PlayDoorOpenSound();

            // Activate the doorguards so the player cant teleport until the room is clear

            for (int i = 0; i < door_guards.Length; i++)
            {
                door_guards[i].SetActive(false);
            }
        }
        

        for (int i = 0; i < DoorLights.Length; i++)
        {
            DoorLights[i].gameObject.SetActive(true);
        }
    }

    void DisableDoors()
    {
        GameObject[] door_guards = GameObject.FindGameObjectsWithTag("DoorGuard");

        // Activate the doorguards so the player cant teleport until the room is clear

        for(int i = 0; i < door_guards.Length; i++)
        {
            door_guards[i].gameObject.SetActive(true);
        }

        for (int i = 0; i < DoorLights.Length; i++)
        {
            DoorLights[i].gameObject.SetActive(false);
        }
    }

    void SpawnHealthPickup()
    {
        System.Random rand = new System.Random();
        int spawnIndex = rand.Next(Enemies.Length);

        Instantiate<GameObject>(Resources.Load<GameObject>("FirstAid"), SpawnPoints[spawnIndex].transform.position, SpawnPoints[spawnIndex].transform.rotation);
    }

    void SetSpawn()
    {
        int spawnIndex = rand.Next(Enemies.Length);
        int count = 0;

        while (EnemyWeights[spawnIndex] > RoomWeight)
        {
            if (count >= 4)
            {
                spawnIndex = 0;
                break;
            }
            spawnIndex = rand.Next(Enemies.Length);
            count++;
        }

        if (RoomWeight > 0 && RoomWeight <= RoomStartWeight)
        {
            // Spawn control
            int spawnpointindex = rand.Next(SpawnPoints.Length);
            lastspawn = spawnpointindex;

            while (SpawnPoints.Length > 1 && spawnpointindex == lastspawn && RoomWeight < RoomStartWeight)
            {
                spawnpointindex = rand.Next(SpawnPoints.Length);
            }

            lastspawn = spawnpointindex;

            //RoomWeight -= EnemyWeights[spawnIndex];
            // Instantiate effect
            Instantiate(SpawnEffect, SpawnPoints[spawnpointindex].transform.position, transform.rotation);
            // Instantiate<GameObject>(Enemies[spawnIndex], SpawnPoints[spawnpointindex].transform.position, transform.rotation);
            // Spawn Enemy
            if (!Enemies[spawnIndex])
            {
#if DEBUG
                Debug.Log("Thing broke");
#endif
            }
            else
            {
                enemy.Add(Enemies[spawnIndex]);
            }

            enemylocation.Add(SpawnPoints[spawnpointindex].transform.position);
            EnemySpawnWeights.Add(EnemyWeights[spawnIndex]);

            Invoke("SpawnEnemy", SpawnInterval);
        }
    }

    private void SpawnEnemy()
    {
        if (enemy.Count > 0 && enemylocation.Count > 0 && enemy.Count == enemylocation.Count && (RoomWeight > 0 && RoomWeight <= RoomStartWeight))
        {
            Instantiate<GameObject>(enemy[enemy.Count - 1], enemylocation[enemy.Count -1], transform.rotation);
            RoomWeight -= EnemySpawnWeights[EnemySpawnWeights.Count - 1];

            enemy.RemoveAt(enemy.Count - 1);
            enemylocation.RemoveAt(enemylocation.Count - 1);
            EnemySpawnWeights.RemoveAt(EnemySpawnWeights.Count - 1);
        }
    }

    private void OnDisable()
    {
        StartSpawner.PlayerEntered -= StartSpawning;
    }
}
