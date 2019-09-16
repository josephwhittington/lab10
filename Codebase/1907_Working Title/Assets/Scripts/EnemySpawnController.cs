using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnController : IPausable
{
    //Alex Spawn Effect
    [SerializeField] GameObject SpawnEffect;
    //float TimerCoolDown = 0.0f;
    //float CoolDown = 2f;
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

    private float timer = 0;
    private uint RoomStartWeight = 0;
    int lastspawn = -1;

    // Enemy spawn timer
    [SerializeField] private float WaitTimebeforeSpawn = 0.0f;

    private float enemyspawntimer = 0;
    private GameObject enemy = null;
    private Vector3 enemylocation = Vector3.zero;
    // Enemy spawn timer

    private void OnEnable()
    {
        StartSpawner.PlayerEntered += StartSpawning;
    }

    void Start()
    {
        RoomStartWeight = RoomWeight;

        DisableDoors();
    }

    void Update()
    {
        if (!GamePaused)
        {
            if (ShouldSpawn && WaitTimebeforeSpawn > 0)
                WaitTimebeforeSpawn -= Time.deltaTime;

            CheckIfRoomClear();

            if (!RoomClear && ShouldSpawn && WaitTimebeforeSpawn <= 0)
            {
                // Timer shit
                timer += Time.deltaTime;

                if (timer >= SpawnInterval)
                {
                    timer = 0;
                    if (!GameState.GamePaused)
                    {
                        enemyspawntimer = SpawnInterval;
                        SetSpawn();
                    }
                }
            }
        }
    }

    void StartSpawning(string RoomName)
    {
        if (RoomName == roomName)
        {
            ShouldSpawn = true;
            DisableDoors();
            InvokeRepeating("SpawnHealthPickup", HealthPickupSpawnInterval, HealthPickupSpawnInterval);
        }
    }

    void CheckIfRoomClear()
    {
        if (RoomWeight <= 0)
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
        System.Random rand = new System.Random();
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

            RoomWeight -= EnemyWeights[spawnIndex];
            // Instantiate effect
            Instantiate(SpawnEffect, SpawnPoints[spawnpointindex].transform.position, transform.rotation);
            //Instantiate<GameObject>(Enemies[spawnIndex], SpawnPoints[spawnpointindex].transform.position, transform.rotation);
            // Spawn Enemy
            enemy = Enemies[spawnIndex];
            enemylocation = SpawnPoints[spawnpointindex].transform.position;

            Invoke("SpawnEnemy", SpawnInterval);
        }
    }

    private void SpawnEnemy()
    {
        if(enemy && enemylocation != Vector3.zero)
            Instantiate<GameObject>(enemy, enemylocation, transform.rotation);
    }

    private void OnDisable()
    {
        StartSpawner.PlayerEntered -= StartSpawning;
    }
}
