using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FollowingCoin : MonoBehaviour
{
    GameObject player = null;
    NavMeshAgent agent = null;

    float coolDown = 5.0f;
    public float TimerCoolDown = 0.0f;
    //public Transform Target;
    //public float MinModifier = 7;
    //public float MaxModifier = 11;
    //Vector3 _velocity = Vector3.zero;


    //bool _isFollowing = false;


    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        agent = GetComponent<NavMeshAgent>();
        TimerCoolDown = coolDown;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject);
            PlayerStats.Coins += 1;
            GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>()?.PlayCoinPickup();
        }
    }

    public void TimerTillSelfDestruct()
    {
        if (TimerCoolDown > 0)
        {
            TimerCoolDown -= Time.deltaTime;
        }

        if (TimerCoolDown <= 0)
        {
            Destroy(gameObject);
            PlayerStats.Coins += 1;
            GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>()?.PlayCoinPickup();
        }

    }

    void Update()
    {
        agent.SetDestination(player.transform.position);
        TimerTillSelfDestruct();
    }
}
