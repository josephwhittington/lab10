using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FollowingCoinThief : MonoBehaviour
{
    GameObject Enemy = null;
    NavMeshAgent agent = null;
    private float check = 0;


    // Start is called before the first frame update
    void Start()
    {
        Enemy = GameObject.FindGameObjectWithTag("CoinThief");
        agent = GetComponent<NavMeshAgent>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("CoinThief"))
        {
            Destroy(gameObject);
            GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>()?.PlayCoinPickup();
        }

        if (!Enemy && other.gameObject.CompareTag("Player") && check > 0.1f)
        {
            Destroy(gameObject);
            GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>()?.PlayCoinPickup();
            PlayerStats.Coins += 1;
        }
    }

    // Update is called once per frame
    void Update()
    {
        check += Time.deltaTime;
        if(!agent.hasPath)
        agent.SetDestination(Enemy.transform.position);


        if (!Enemy && check > 0.1f)
        {
            agent.SetDestination(GameObject.FindGameObjectWithTag("Player").gameObject.transform.position);
        }
    }
}
