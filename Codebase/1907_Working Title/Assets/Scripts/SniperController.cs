using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class SniperController : IPausable
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
    //[SerializeField] Image EnemyHealthUI = null;

    //[SerializeField] uint BulletDamage = 1;

    [SerializeField] uint HitPoints = 3;

    uint MaxHP;
    private float timer = 0;



    [SerializeField] ParticleSystem ParticleSystem = null;
    private bool canShoot = false;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        agent = GetComponentInChildren<NavMeshAgent>();

        // Set shot rate here
        //InvokeRepeating("Shoot", ShotWaitTime, ShotInterval);

        // Set the max hp to the starting hp value for percentage on fill amt for UI update
        MaxHP = HitPoints;
        ParticleSystem.Stop();
    }
    private void Update()
    {
        RaycastHit Hit;
        if (!GamePaused && !PlayerStats.PlayerDead)
        {
            agent.isStopped = false;
            agent.SetDestination(player.transform.position);
            Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out Hit, Mathf.Infinity);
            if(Hit.collider.gameObject.tag == "Player")
            {
                canShoot = true;
                if (ParticleSystem.isStopped || ParticleSystem.isPaused)
                {
                    ParticleSystem.Play();
                    GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>().PlaySniperCharge();
                }
        }
            else
            {
                canShoot = false;
                if (ParticleSystem.isPlaying)
                {
                    GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>().StopSniperCharge();
                    ParticleSystem.Stop();
                }
                timer = 0;
            }

        
            if (canShoot)
            {
                timer += Time.deltaTime;
                if (timer >= ShotInterval)
                {
                    timer = 0;
                    Shoot();
                }
            }
        }
        else
        {
            agent.isStopped = true;
            GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>().StopSniperCharge();
            ParticleSystem.Pause();
        }
        
    }

    private void FixedUpdate()
    {
        LookAtPlayer();
    }

    public void DealDamage(uint p_damage)
    {
        if (HitPoints - p_damage <= 0 || HitPoints - p_damage > MaxHP)
        {
            Suicide();

            //alex kill counter
            KillCounter.instance.IncrementCount();
            KillCounter.instance.UpdateCounter();


            //After destroying enemy make him drop a coin this will follow to the player//
            GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>()?.PlayEnemyDeathSound();
            Instantiate<GameObject>(DeathEffect, transform.position, transform.rotation);
            Instantiate<GameObject>(CoinDrop, transform.position, transform.rotation);
        }
        else HitPoints -= p_damage;
    }

    void Suicide()
    {
        GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>().StopSniperCharge();
        ParticleSystem.Stop();
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
    public void TakeDamage(uint p_damage)
    {
        HitPoints = (HitPoints - p_damage < 0) ? 0 : HitPoints - p_damage;
    }
}