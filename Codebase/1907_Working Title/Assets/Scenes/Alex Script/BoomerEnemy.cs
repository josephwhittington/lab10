using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BoomerEnemy : IPausable
{
    GameObject player;
    NavMeshAgent agent;
    Renderer rend;
    Animator anim;

    [SerializeField] GameObject ForceField = null;
    public float scrollSpeed = 0.5f;

    [SerializeField] int HitPoints = 2;
    [SerializeField] GameObject littlefucks = null;

    int MaxHP = 5;

    uint ShieldHealth = 10;


    // Start is called before the first frame update
    void Start()
    {
        MaxHP = HitPoints;

        anim = gameObject.GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player");
        agent = GetComponent<NavMeshAgent>();
        rend = ForceField.GetComponent<Renderer>();

        if (GetComponent<AudioSource>())
            GetComponent<AudioSource>().volume = GameObject.FindGameObjectWithTag("AudioManager")
                .GetComponent<AudioManager>().SFXVolume;
    }

    // Update is called once per frame
    void Update()
    {
        GetComponent<AudioSource>().volume = GameObject.FindGameObjectWithTag("AudioManager")
            .GetComponent<AudioManager>().SFXVolume;
        if (!GamePaused && !PlayerStats.PlayerDead)
        {
            agent.SetDestination(player.transform.position);
            anim.SetBool("Walk_Anim", true);
            float offset = Time.time * scrollSpeed;
            rend.material.SetTextureOffset("_MainTex", new Vector2(0, offset));
        }
    }

    void Suicide()
    {
        GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>().PlayEnemyBoomerDeathSound();
        Destroy(gameObject);
    }

    public void DealDamage(uint p_damage)
    {
        if (HitPoints - p_damage <= 0 || HitPoints > MaxHP)
        {
           Suicide();

            //alex kill counter
            KillCounter.instance.IncrementCount();
            KillCounter.instance.UpdateCounter();

            for (int i = 0; i < 3; i++)
            {
                Instantiate<GameObject>(littlefucks, transform.position, transform.rotation);
            }
        }
        else
        {
            if (ShieldHealth > 0)
            {
                ShieldHealth -= 1;
            }
            else
            {
               HitPoints -= (int)p_damage;
            }

            if (ShieldHealth <= 5)
            {
                rend.material.SetColor("_EmissionColor", Color.yellow);
            }

        }
    }
}
