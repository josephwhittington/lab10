using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemySelfDestruct : IPausable
{

    //float destinationReachedTreshold = 5.0f;
    Animator anim;
    GameObject player = null;
    NavMeshAgent agent = null;
    MeshRenderer meshRenderer = null;
    [SerializeField] Material HitEffect = null;
    [SerializeField] Material EnemyColor = null;
    [SerializeField] int HitPoints = 3;
    //uint MaxHP = 3;

    [SerializeField] GameObject Explosion = null;

    private float HitColorCoolDown = 0.25f;
    private float HitCoolDown = 0.25f;
    private bool HitEffectON = false;


    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        agent = GetComponent<NavMeshAgent>();
        meshRenderer = GetComponent<MeshRenderer>();

        
    }

    // Update is called once per frame
    void Update()
    {
        if (!GamePaused && !PlayerStats.PlayerDead)
        {
            agent.SetDestination(player.transform.position);
         

            if (agent.stoppingDistance == 3)
            {
                if (!HitEffectON)
                {
                    HitEffectON = true;
                    HitColorCoolDown = HitCoolDown;
                }
                BlinkEffect();
            }

            //explode here
            Explode();
            // DealDamage();
        }
    }

  

    void Explode()
    {
        float distanceToTarget = Vector3.Distance(transform.position, player.transform.position);
        //distanceToTarget = Mathf.Abs(distanceToTarget);

        //// Whittington
        //Vector2 player_location = new Vector2(player.transform.position.x, player.transform.position.z);
        //Vector2 gameobj_location = new Vector2(transform.position.x, transform.position.z);
        //float distanceToTarget = Mathf.Abs(Vector2.Distance(player_location, gameobj_location));
        // Whittington
#if DEBUG
        //Debug.Log("Distance:" + distanceToTarget);
#endif

        //if (distanceToTarget <= destinationReachedTreshold)
        //{
        //    Suicide();
        //    Instantiate<GameObject>(Explosion, transform.position, transform.rotation);
        //}

        if (Vector3.Distance(player.transform.position, transform.position) < 3)
        {
            Suicide();
            Instantiate<GameObject>(Explosion, transform.position, transform.rotation);
        }
    }


    void BlinkEffect()
    {
        meshRenderer.material = EnemyColor;

        if (HitEffectON)
        {
            if (HitColorCoolDown > 0)
            {
                meshRenderer.material = HitEffect;
                HitColorCoolDown -= Time.deltaTime;
            }

            else if (HitColorCoolDown < 0)
            {
                HitColorCoolDown = 0;
                HitEffectON = false;
            }

        }

    }


    //player damage connected to enemy health bar
    public void DealDamage(uint p_damage)
    {
        if (HitPoints - (int)p_damage <= 0)
        {
            Suicide();

            //alex kill counter
            KillCounter.instance.IncrementCount();
            KillCounter.instance.UpdateCounter();

            Instantiate<GameObject>(Explosion, transform.position, transform.rotation);
        }
        else
        {
            HitPoints -= (int)p_damage;
        }
    }


    void Suicide()
    {
        GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>().PlayExplosionSound();
        Destroy(gameObject);
    }

}
