using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForceField : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if(!other.gameObject.GetComponent<PlayerController>().GodModeEnabled())
                PlayerStats.DealDamageToPlayer(1);
        }
    }

}

