using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningWall : MonoBehaviour
{
    [SerializeField] float tickDamageCooldown = 2.0f;
    private float tickDamageTimer = 0.0f;
    public void OnTriggerStay(Collider other)
    {
        tickDamageTimer += Time.deltaTime;
        if(other.CompareTag("Player"))
        {
            if(tickDamageTimer >= tickDamageCooldown)
            {
                tickDamageTimer = 0;
                PlayerStats.DealDamageToPlayer(1);
            }
        }
    }


}
