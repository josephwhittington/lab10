using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyCoinThief : IPausable
{
    GameObject player = null;
    NavMeshAgent agent = null;
    [SerializeField] int HitPoints = 12;
    [SerializeField] GameObject CoinDrop = null;
    private float TimerCoolDown = 0;
    private float CoolDown = 0.01f;
    private float WaitForThis = 0;
    private float WaitForThisCool = 1.0f;
   

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        agent = GetComponent<NavMeshAgent>();
        InvokeRepeating("CoinTimer", 0, .6f);
    }

    void CoinTimer()
    {
        TimerCoolDown = CoolDown;

        if (TimerCoolDown > 0)
        {
            TimerCoolDown -= Time.deltaTime;

            if (PlayerStats.Coins > 1)
            {             
                Instantiate<GameObject>(CoinDrop, player.transform.position, player.transform.rotation);
                PlayerStats.Coins -= 1;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name.Equals("CoinThief"))
        {
            Destroy(gameObject);
        }
    }

    public void DealDamage(uint p_damage)
    {
        if (HitPoints - (int)p_damage <= 0)
        {
            Suicide();
        }

        else
        {
            HitPoints -= (int)p_damage;
        }

    }

    private void FixedUpdate()
    {
        LookAtPlayer();
    }

    void LookAtPlayer()
    {
        // Rotate towards mouse
        Vector3 dir = Camera.main.WorldToScreenPoint(player.transform.position) - Camera.main.WorldToScreenPoint(transform.position);
        float angle = Mathf.Atan2(dir.x, dir.y) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.up);
    }

 
    
    void Suicide()
    {
        GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>().PlayExplosionSound();
        Destroy(gameObject);
        CancelInvoke("CoinTimer");
    }

}
