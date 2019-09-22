using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class ObstacleEnemyController : IPausable
{
    NavMeshAgent agent;
    [SerializeField] GameObject HealthDrop = null;
    [SerializeField] GameObject DeathEffect = null;
    //[SerializeField] Image EnemyHealthUI = null;
    [SerializeField] GameObject[] Waypoints = null;
    private int currIndex = 0;
    private GameObject CurrentWaypoint = null;
    private Vector3 CurrentWaypointTrans = Vector3.zero;
    public int HitPoints = 3;
    private GameObject[] EnemiesToHeal = null;
    int MaxHP;
    [SerializeField] ParticleSystem particle = null;

    void Start()
    {
        gameObject.SetActive(false);
        particle.Stop();
        agent = GetComponentInChildren<NavMeshAgent>();
        CurrentWaypoint = Waypoints[0];

        // Set shot rate here
        //InvokeRepeating("Shoot", ShotWaitTime, ShotInterval);

        // Set the max hp to the starting hp value for percentage on fill amt for UI update
        MaxHP = HitPoints;
    }
    // Update is called once per frame
    void Update()
    {
        CurrentWaypointTrans = CurrentWaypoint.transform.position;
        CurrentWaypointTrans.y = transform.position.y;
        if (!GamePaused && !PlayerStats.PlayerDead)
        {
            agent.isStopped = false;
            if (transform.position != CurrentWaypointTrans)
            {   
                agent.SetDestination(CurrentWaypointTrans);
            }
            else
            {
                FindToHeal();
                HealEnemies();
                ++currIndex;
                if (currIndex >= Waypoints.Length)
                {
                    currIndex %= Waypoints.Length;
                }
                CurrentWaypoint = Waypoints[currIndex];
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
            Instantiate<GameObject>(HealthDrop, new Vector3(transform.position.x, 1.0f, transform.position.z), transform.rotation);
        }
        else HitPoints -= p_damage;
    }

    void Suicide()
    {
        Destroy(gameObject);
    }

    void LookAtPlayer()
    {
        // Rotate towards mouse
        Vector3 dir = Camera.main.WorldToScreenPoint(CurrentWaypointTrans) - Camera.main.WorldToScreenPoint(transform.position);
        float angle = Mathf.Atan2(dir.x, dir.y) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.up);
    }


    //TODO: delete not being used (wi)
    float AngleBetweenTwoPoints(Vector2 a, Vector2 b)
    {
        return Mathf.Atan2(a.y - b.y, a.x - b.x) * Mathf.Rad2Deg;
    }

    //player damage connected to enemy health bar
    public void TakeDamage(int p_damage)
    {
        HitPoints = (HitPoints - p_damage < 0) ? 0 : HitPoints - p_damage;
    }

    private void FindToHeal()
    {
        if (GameObject.FindGameObjectsWithTag("Enemy").Length > 0)
            EnemiesToHeal = GameObject.FindGameObjectsWithTag("Enemy");
    }

    private void HealEnemies()
    {
        if (EnemiesToHeal == null)
            return;
        for (int i = 0; i <= EnemiesToHeal.Length; i++)
        {
            EnemiesToHeal[i].GetComponent<EnemyController>().TakeDamage(-1);
        }
        if(EnemiesToHeal.Length != 0)
            particle.Play();
    }


}
