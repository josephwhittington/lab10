using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class EnemyController : IPausable
{
    GameObject player;
    NavMeshAgent agent;


    //Alex Coin Drop attached prefab coin
    [SerializeField] GameObject CoinDrop = null;
    [SerializeField] GameObject DeathEffect = null;


    [SerializeField] GameObject BulletPrefab = null;
    [SerializeField] GameObject[] SpawnPoints = null;
    [SerializeField] float ShotInterval = 0.5f;
    //[SerializeField] float ShotWaitTime = 2.0f;
    [SerializeField] Image EnemyHealthUI = null;

    //[SerializeField] uint BulletDamage = 1;

    [SerializeField] int HitPoints = 3;

    int MaxHP;
    private float timer = 0;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        agent = GetComponentInChildren<NavMeshAgent>();

        // Set shot rate here
        //InvokeRepeating("Shoot", ShotWaitTime, ShotInterval);

        // Set the max hp to the starting hp value for percentage on fill amt for UI update
        MaxHP = HitPoints;
    }

    void UpdateUi()
    {
        float fillamount = (float)HitPoints / (float)MaxHP;
        EnemyHealthUI.fillAmount = fillamount > 1.0f ? 1.0f : fillamount;
    }

    // Update is called once per frame
    void Update()
    {
        if (!GamePaused && !PlayerStats.PlayerDead)
        {
            UpdateUi();
            agent.isStopped = false;
            agent.SetDestination(player.transform.position);

            timer += Time.deltaTime;

            if (timer >= ShotInterval)
            {
                timer = 0;
                Shoot();
            }
        }
        else
        {
            agent.isStopped = true;
        }
    }

    private void FixedUpdate()
    {
        LookAtPlayer();
    }

    public void DealDamage(int p_damage)
    {
        if (HitPoints - p_damage <= 0 || HitPoints - p_damage > MaxHP)
        {
            Suicide();

            //After destroying enemy make him drop a coin this will follow to the player//
            GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>()?.PlayEnemyDeathSound();
            Instantiate<GameObject>(DeathEffect, transform.position, transform.rotation);
            Instantiate<GameObject>(CoinDrop, transform.position, transform.rotation);

            //alex kill counter
            KillCounter.instance.IncrementCount();
            KillCounter.instance.UpdateCounter();

        }

        else HitPoints -= p_damage;

        UpdateUi();
    }

    void Suicide()
    {
        Destroy(gameObject);
    }

    void LookAtPlayer()
    {
        // Rotate towards mouse
        Vector3 dir = Camera.main.WorldToScreenPoint(player.transform.position) - Camera.main.WorldToScreenPoint(transform.position);
        float angle = Mathf.Atan2(dir.x, dir.y) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.up);
    }

    void Shoot()
    {
        for (uint i = 0; i < SpawnPoints.Length; i++)
            Instantiate<GameObject>(BulletPrefab, SpawnPoints[i].transform.position, SpawnPoints[i].transform.rotation);
    }


    //TODO: delete not being used (wi)
    float AngleBetweenTwoPoints(Vector2 a, Vector2 b)
    {
        return Mathf.Atan2(a.y - b.y, a.x - b.x) * Mathf.Rad2Deg;
    }

    //player damage connected to enemy health bar
    public void TakeDamage(int p_damage)
    {
        if(p_damage < 0)
            HitPoints = (HitPoints - p_damage >= MaxHP) ? MaxHP : HitPoints - p_damage;
        else
            HitPoints = (HitPoints - p_damage < 0) ? 0 : HitPoints - p_damage;
    }
}
