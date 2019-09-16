using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WreckThePlayer : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.gameObject.GetComponent<PlayerController>().DealDamageToPlayer(5);
        }
    }
}
